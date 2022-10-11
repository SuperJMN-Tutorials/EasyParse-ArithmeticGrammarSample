using Xunit.Abstractions;

namespace GrammarTest;

public class ArithmeticGrammarTests
{
    private readonly ITestOutputHelper output;

    public ArithmeticGrammarTests(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public void Test_grammar2()
    {
        var parser = new Other().BuildCompiler<Term>();

        var otherGrammar = string.Join(Environment.NewLine, new Other().ToGrammarFileContent());
        var originalGrammar = string.Join(Environment.NewLine, new ArithmeticGrammar().ToGrammarFileContent());

        var result = parser.Compile("1+2*5");

        Assert.True(result.IsSuccess);
    }
}