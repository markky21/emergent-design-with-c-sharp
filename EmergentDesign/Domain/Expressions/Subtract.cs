namespace EmergentDesign.Domain.Expressions;

internal class Subtract : BinaryExpression
{
    public Subtract(Expression left, Expression right) : base(left, right)
    {
    }

    protected override string OperatorToString { get; } = "-";

    protected override int Combine(int leftValue, int rightValue)
    {
        return leftValue - rightValue;
    }
}