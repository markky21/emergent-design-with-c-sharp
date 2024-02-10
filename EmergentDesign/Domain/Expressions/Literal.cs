namespace EmergentDesign.Domain.Expressions;

internal class Literal : Expression
{
    internal Literal(int value)
    {
        Value = value;
    }

    public override int Value { get; }

    public override string ToString()
    {
        return $"{Value}";
    }
}