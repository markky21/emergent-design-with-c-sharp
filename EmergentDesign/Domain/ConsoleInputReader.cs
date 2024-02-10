using EmergentDesign.Common;

namespace EmergentDesign.Domain;

internal class ConsoleInputReader(string prompt = "Input numbers: ")
{
    private readonly string _prompt = prompt;

    public IEnumerable<IEnumerable<int>> ReadAll =>
        Console.In.IncomingLines(Prompt).NonNegativeIntSequences();

    private void Prompt()
    {
        Console.Write(_prompt);
    }
}