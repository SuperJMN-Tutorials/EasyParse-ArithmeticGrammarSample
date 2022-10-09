namespace GrammarTest;

public class ArithmeticGrammarTests
{
    [Fact]
    public void Test_grammar()
    {
        var parser = new ArithmeticGrammar().BuildCompiler<Additive>();
        var result = parser.Compile("(-1+2)*5");
        Assert.True(result.IsSuccess);
    }
}