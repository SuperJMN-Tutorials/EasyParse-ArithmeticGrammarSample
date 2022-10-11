using EasyParse.Fluent;
using EasyParse.Fluent.Symbols;
using GrammarTest.Model;

namespace GrammarTest.Grammars;

public class ArithmeticGrammarFluent : FluentGrammar
{
    private NonTerminal Unit => () => Rule()
        .Match(Pattern.Int).To((int i) => new Unit(null, new ConstantExpression(i)))
        .Match("-", Unit).To((Unit n) => new Unit("-", n))
        .Match("(", Term, ")").To((Term term) => new Unit(term.Op, term.Expressions));

    private NonTerminal Factor => () => Rule()
        .Match(Unit).To((Unit u) => new Factor(u.Op, u))
        .Match(Factor, "*", Unit).To((Factor f, Unit n) => new Factor("*", f, n))
        .Match(Factor, "/", Unit).To((Factor f, Unit n) => new Factor("/", f, n));

    private NonTerminal Term => () => Rule()
        .Match(Factor).To((Factor u) => new Term(u.Op, u))
        .Match(Term, "+", Factor).To((Term f, Factor n) => new Term("+", f, n))
        .Match(Term, "-", Factor).To((Term f, Factor n) => new Term("-", f, n));

    private IRule Expression() => Rule().Match<Term>(Term);

    protected override IRule Start => Expression();
    protected override IEnumerable<RegexSymbol> Ignore => new List<RegexSymbol>() { Pattern.WhiteSpace };

}