namespace EmergentDesign.Domain.Expressions;

internal abstract class BinaryExpression : Expression
{
    protected BinaryExpression(Expression left, Expression right)
    {
        Left = left;
        Right = right;
    }

    protected Expression Left { get; }
    protected Expression Right { get; }
    protected abstract string OperatorToString { get; }

    public override int Value =>
        Combine(Left.Value, Right.Value);

    protected abstract int Combine(int leftValue, int rightValue);

    public override string ToString()
    {
        return $"{Left} {OperatorToString} {Right}";
    }
}