using EmergentDesign.Domain.Expressions;

namespace EmergentDesign.Domain;

internal static class ExpressionExtensions
{
    public static Expression Add(this Expression head, IEnumerable<Expression> others)
    {
        return others.Aggregate(head, (current, other) => new Add(current, other));
    }

    public static IEnumerable<Expression> TrySubtract(this Expression head, IEnumerable<Expression> others)
    {
        var current = head;
        foreach (var other in others)
        {
            if (other.Value > current.Value)
                yield break;
            current = new Subtract(current, other);
        }

        yield return current;
    }

    public static Expression Multiply(this Expression head, IEnumerable<Expression> others)
    {
        return others.Aggregate(head, (current, other) => new Multiply(current, other));
    }

    public static IEnumerable<Expression> TryDivide(this Expression head, IEnumerable<Expression> others)
    {
        var current = head;
        foreach (var other in others)
        {
            if (other.Value == 0 || current.Value % other.Value != 0)
                yield break;
            current = new Divide(current, other);
        }

        yield return current;
    }
}
