using EasyParse.Fluent;
using EasyParse.Fluent.Symbols;
using GrammarTest.Model;

namespace GrammarTest;

public class ArithmeticGrammarFluent : FluentGrammar
{
    private NonTerminal Unit => () => Rule()
        .Match(Pattern.Int)
        .To((int i) => new Unit(null, new ConstantExpression(i)));

    private NonTerminal Multiplicative => () => Rule()
        .Match(Unit).To((Unit u) => new Factor(u.Op, u))
        .Match(Multiplicative, "*", Unit).To((Factor f, Unit n) => new Factor("*", f, n))
        .Match(Multiplicative, "/", Unit).To((Factor f, Unit n) => new Factor("/", f, n));

    private NonTerminal Additive => () => Rule()
        .Match(Multiplicative).To((Factor u) => new Term(u.Op, u))
        .Match(Additive, "+", Multiplicative).To((Term f, Factor n) => new Term("+", f, n))
        .Match(Additive, "-", Multiplicative).To((Term f, Factor n) => new Term("-", f, n));


    private IRule Expression() => Rule().Match<Term>(Additive);

    protected override IRule Start => Expression();
    protected override IEnumerable<RegexSymbol> Ignore => new List<RegexSymbol>() { Pattern.WhiteSpace };

}