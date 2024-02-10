namespace EmergentDesign.Domain.Expressions;

internal class Add : BinaryExpression
{
    public Add(Expression left, Expression right) : base(left, right)
    {
    }

    protected override string OperatorToString { get; } = "+";

    protected override int Combine(int leftValue, int rightValue)
    {
        return leftValue + rightValue;
    }
}