using CliFx.Infrastructure;

namespace ShellAssist;

public class ConsoleHelper
{
    private readonly IConsole _console;

    public void ShowSuccess(string message)
    {
        _console.Output.WriteLine($"GOOD - {message}");
    }

    public void ShowFailure(string message)
    {
        _console.Output.WriteLine($"FAIL - {message}");
    }
}