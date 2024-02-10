using EmergentDesign.Common;

namespace EmergentDesign.Domain;

internal class ConsoleProblemsReader
{
    private ConsoleInputReader InputReader { get; } = new();

    public IEnumerable<int> DesiredResult =>
        Console.In.IncomingLines(PromptMessageDesiredInput).NonNegativeInt();

    private IEnumerable<(IEnumerable<int> inputNumbers, int desiredResult)> RawNumbersSequence =>
        InputNumberSequence.Zip(DesiredResult, (inputNumbers, desiredResult) => (inputNumbers, desiredResult));

    private IEnumerable<IEnumerable<int>> InputNumberSequence =>
        InputReader.ReadAll;

    public IEnumerable<ProblemStatement> ReadAll()
    {
        return RawNumbersSequence.Select(tuple => new ProblemStatement(tuple.inputNumbers, tuple.desiredResult));
    }

    private void PromptMessageDesiredInput()
    {
        Console.WriteLine("Enter desired result:");
    }
}