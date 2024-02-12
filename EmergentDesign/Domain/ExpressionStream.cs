using EmergentDesign.Common;
using EmergentDesign.Domain.Expressions;

namespace EmergentDesign.Domain;

internal class ExpressionStream
{
    public IEnumerable<Expression> DistinctFor(IEnumerable<int> inputNumbers) =>
        DistinctFor(AsLiterals(inputNumbers), 1, MultiplyAndDivide, CreateAdditive);

    private IEnumerable<Expression> DistinctFor(
        IEnumerable<Expression> elements, int minPartitions,
        Func<Partition<Expression>, IEnumerable<Expression>> partitionExpressionBuilder,
        Func<Expression, IEnumerable<Expression>, IEnumerable<Expression>, IEnumerable<Expression>>
            reduceExpressionBuilder) =>
        elements.IsOnlyOne()
            ? elements
            : Partitionings.Of(elements)
                .All()
                .Where(partitioning => partitioning.Count() >= minPartitions)
                .SelectMany(partitioning => partitioning.Select(partitionExpressionBuilder).CrossProduct())
                .SelectMany(subexpressions =>
                    ThreeWaySplit(subexpressions)
                        .SelectMany(split => reduceExpressionBuilder(split.head, split.direct, split.inverse)));

    private IEnumerable<Expression> AsLiterals(IEnumerable<int> inputNumbers) =>
        inputNumbers.Select(number => new Literal(number));

    private IEnumerable<Expression> AddAndSubtract(IEnumerable<Expression> expressions) =>
        DistinctFor(expressions, 2, MultiplyAndDivide, CreateAdditive);

    private IEnumerable<Expression> MultiplyAndDivide(IEnumerable<Expression> expressions) =>
        DistinctFor(expressions, 2, AddAndSubtract, CreateMultiplicative);

    private IEnumerable<Expression> CreateAdditive(
        Expression head, IEnumerable<Expression> add, IEnumerable<Expression> subtract) =>
        head.Add(add).TrySubtract(subtract);

    private IEnumerable<Expression> CreateMultiplicative(
        Expression head, IEnumerable<Expression> multiply, IEnumerable<Expression> divide) =>
        head.Multiply(multiply).TryDivide(divide);

    private IEnumerable<(Expression head, Partition<Expression> direct, Partition<Expression> inverse)>
        ThreeWaySplit(IEnumerable<Expression> expressions) =>
        expressions
            .AsPartition()
            .Split()
            .Where(split => split.left.Any())
            .Select(split => (split.left.First(), split.left.Skip(1).AsPartition(), split.right));
}
