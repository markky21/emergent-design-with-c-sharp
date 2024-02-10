using EmergentDesign.Domain;
using EmergentDesign.Domain.Expressions;

internal class Multiply : BinaryExpression
{
    public Multiply(Expression left, Expression right)
        : base(left, right)
    {
    }

    protected override string OperatorToString => "*";

    protected override int Combine(int left, int right)
    {
        return left * right;
    }
}