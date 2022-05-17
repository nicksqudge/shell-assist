using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;

namespace ShellAssist.Commands;

public abstract class BaseCommand : ICommand
{
    [CommandOption("debug", 'd', Description = "Run in debug mode")]
    public bool InDebug { get; set; }

    private IConsole _console;
    
    protected abstract ValueTask ExecuteCommandAsync(IConsole console);

    public ValueTask ExecuteAsync(IConsole console)
    {
        this._console = console;
        return ExecuteCommandAsync(console);
    }

    protected void LogError(string message)
    {
        _console.WriteFailure($"[DEBUG] - {message}", ConsoleColor.Cyan);
        _console.NewLine();
    }

    protected void LogInfo(string message)
    {
        _console.WriteInColour($"[DEBUG] - {message}", ConsoleColor.Cyan);
        _console.NewLine();
    }

    protected void LogSuccess(string message)
    {
        _console.WriteSuccess($"[DEBUG] - {message}", ConsoleColor.Cyan);
        _console.NewLine();
    }
}