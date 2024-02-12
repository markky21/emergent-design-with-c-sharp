namespace EmergentDesign.Domain;

internal class ExactSolver
{
    public IEnumerable<Expression> DistinctExpressionsFor(ProblemStatement problem)
    {
        return new ExpressionStream()
            .DistinctFor(problem.InputNumbers)
            .Where(expression => expression.Value == problem.DesiredResult);
    }
}
