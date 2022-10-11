namespace GrammarTest.Model;

public record Factor(string Op, params Expression[] Expressions) : ArithmeticOperation(Op, Expressions);