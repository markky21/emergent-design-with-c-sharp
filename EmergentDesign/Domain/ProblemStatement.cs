using EmergentDesign.Common;

namespace EmergentDesign.Domain;

internal class ProblemStatement
{
    public ProblemStatement(IEnumerable<int> inputNumbers, int desiredResult)
    {
        InputNumbers = inputNumbers;
        DesiredResult = desiredResult;
    }

    public IEnumerable<int> InputNumbers { get; }
    public int DesiredResult { get; }

    public override string ToString()
    {
        return $"Problem statement: [{InputNumbers.JoinToText(" ")}] and desired result: {DesiredResult}" +
               Environment.NewLine;
    }
}