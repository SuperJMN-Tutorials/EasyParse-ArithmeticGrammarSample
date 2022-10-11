using EasyParse.Parsing;
using Xunit.Abstractions;
using Zafiro.Core.Mixins;
using Zafiro.Core.Trees;

namespace GrammarTest;

public class ArithmeticGrammarTests
{
    private readonly ITestOutputHelper output;

    public ArithmeticGrammarTests(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public void Test_grammar()
    {
        var parser = new ArithmeticGrammar().BuildCompiler<Term>();
        var result = parser.Compile("(-1+2)*5");
        PrintResult(result);

        Assert.True(result.IsSuccess);
    }

    private void PrintResult(CompilationResult<Term> result)
    {
        if (!result.IsSuccess)
        {
            return;
        }

        var treeNodes = result.Result.ToTreeNodes<ArithmeticOperation>(x => x.Expressions.OfType<ArithmeticOperation>());
        var format = treeNodes
            .Flatten(x => x.Children)
            .Select(x => new string('\t', x.Path.Count() - 1) + Format(x.Item));

        var str = string.Join(Environment.NewLine, format);
        output.WriteLine(str);
    }

    private string Format(ArithmeticOperation argItem)
    {
        return "[" + argItem.Op + "]" + " " + string.Join(",", argItem.Expressions.Select(x => x.ToString()));
    }
}