using System.Text.RegularExpressions;
using EasyParse.Native;
using EasyParse.Native.Annotations;

namespace GrammarTest;

internal class ArithmeticGrammar : NativeGrammar
{
    protected override IEnumerable<Regex> IgnorePatterns => new[] { new Regex(@"\s+") };

    public Unit Unit([R("number", @"\d+")] string value) => new ConstantUnit(int.Parse(value));

    public Unit Unit([L("-")] string minus, Unit unit) => new NegateUnit(unit);

    public Unit Unit([L("(")] string open, Additive additive, [L(")")] string close) => new AdditiveUnit(additive);

    public Multiplicative Multiplicative(Unit unit) => new MultiplicativeUnit(unit);

    public Multiplicative Multiplicative(Multiplicative multiplicative, [L("*", "/")] string op, Unit unit) => new MultiplicativeBinary(multiplicative, op, unit);

    public Additive Additive(Additive additive, [L("+", "-")] string op, Multiplicative multiplicative) => new AdditiveBinary(additive, op, multiplicative);

    [Start]
    public Additive Additive(Multiplicative multiplicative) => new AdditiveMultiplicative(multiplicative);
}

internal record NegateUnit(Unit Unit) : Unit;

internal record AdditiveBinary(Expression Left, string Op, Expression Right) : Additive;

internal record AdditiveMultiplicative(Multiplicative Multiplicative) : Additive;

public abstract record Multiplicative : Expression;

internal record MultiplicativeBinary(Expression Left, string Op, Expression Right) : Multiplicative;

internal record MultiplicativeUnit(Unit Unit) : Multiplicative;

public abstract record Additive : Expression;

public record Expression;

public record Unit : Expression;

internal record AdditiveUnit(Additive Additive) : Unit;

internal record ConstantUnit(int Value) : Unit;