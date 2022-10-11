namespace GrammarTest.Model;

public record ArithmeticOperation(string? Op, params Expression[] Expressions) : Expression;