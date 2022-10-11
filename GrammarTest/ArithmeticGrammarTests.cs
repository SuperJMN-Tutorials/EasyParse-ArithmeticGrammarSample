using System.Text.RegularExpressions;
using FluentAssertions;
using GrammarTest.Grammars;

namespace GrammarTest;

public class ArithmeticGrammarTests
{
    [Fact]
    public void Both_grammars_should_be_equivalent()
    {
        var fluentGrammarContent = new ArithmeticGrammarFluent().ToGrammarFileContent()
            .Select(Cleanup)
            .ToList();

        var nativeGrammarContent = new ArithmeticGrammarNative().ToGrammarFileContent()
            .Select(Cleanup)
            .ToList();

        fluentGrammarContent.Should().BeEquivalentTo(nativeGrammarContent);
    }

    private static string Cleanup(string s)
    {
        return Regex.Replace(s, @";\s*#\d*", "");
    }
}