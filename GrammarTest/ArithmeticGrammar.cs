using System.Text.RegularExpressions;
using EasyParse.Native;
using EasyParse.Native.Annotations;

namespace GrammarTest;

internal class ArithmeticGrammar : NativeGrammar
{
    protected override IEnumerable<Regex> IgnorePatterns => new[] { new Regex(@"\s+") };

    public Unit Unit([R("number", @"\d+")] string value) => new Unit(null, new ConstantExpression(int.Parse(value)));
    public Unit Unit([L("-")] string minus, Unit unit) => new("-", unit);
    public Unit Unit([L("(")] string open, Term term, [L(")")] string close) => new(term.Op, term.Expressions);
    public Factor Factor(Unit unit) => new(unit.Op, unit.Expressions);
    public Factor Factor(Factor factor, [L("*", "/")] string op, Unit unit) => new Factor(op, factor, unit);
    public Term Term(Term term, [L("+", "-")] string op, Factor factor) => new Term(op, term, factor);

    [Start]
    public Term Term(Factor factor) => new Term(factor.Op, factor.Expressions);
}

public record Factor(string Op, params Expression[] Expressions) : ArithmeticOperation(Op, Expressions);

public record Term(string Op, params Expression[] Expressions) : ArithmeticOperation(Op, Expressions);

public record Expression;

public record ArithmeticOperation(string? Op, params Expression[] Expressions) : Expression;

public record Unit(string? Op, params Expression[] Expressions) : ArithmeticOperation(Op, Expressions);

internal record ConstantExpression(int Value) : Expression;