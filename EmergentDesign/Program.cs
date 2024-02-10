// See https://aka.ms/new-console-template for more information

using EmergentDesign.Common;
using EmergentDesign.Domain;

namespace EmergentDesign;

static class Program
{
    static void Main() =>
        ProductionBehavior();

    static void ExpressionStreamDemo() =>
        InputNumberSequence
            .Select(new ExpressionStream().DistinctFor)
            .SelectMany(expressions => Raport(expressions, "No solution found."))
            .WriteLinesTo(Console.Out);

    private static IEnumerable<IEnumerable<int>> InputNumberSequence =>
        new ConsoleInputReader().ReadAll;

    private static void ProductionBehavior() =>
        ProblemStatements
            .Select(problem => new ExactSolver().DistinctExpressionsFor(problem))
            .SelectMany(expressions => Raport(expressions, "No solution found."))
            .WriteLinesTo(Console.Out);

    private static IEnumerable<string> Raport(IEnumerable<Expression> expressions, string onEmpty) =>
        expressions.Select(
                (expression, index) => $"{index + 1,3}. {expression} = {expression.Value}")
            .DefaultIfEmpty(onEmpty);

    private static IEnumerable<ProblemStatement> ProblemStatements => new ConsoleProblemsReader().ReadAll();
}
