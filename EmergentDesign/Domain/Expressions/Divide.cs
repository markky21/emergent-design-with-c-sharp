using EmergentDesign.Domain;
using EmergentDesign.Domain.Expressions;

internal class Divide : BinaryExpression
{
    public Divide(Expression left, Expression right)
        : base(left, right)
    {
    }

    protected override string OperatorToString => "/";

    protected override int Combine(int left, int right)
    {
        return left / right;
    }
}
