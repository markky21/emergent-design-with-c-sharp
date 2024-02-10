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

// 1. create all possible expressions for plus
// 1. create all possible expressions for minus
// 1. create all possible expressions for multiply
// 1. create all possible expressions for divide
// repeate for minus and devide but with different order of numbers
// define restrictions for divide
// defien restrictions for minus
// combine all expressions