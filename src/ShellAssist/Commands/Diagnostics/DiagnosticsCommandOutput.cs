using CliFx.Infrastructure;

namespace ShellAssist.Commands.Diagnostics;

public class DiagnosticsCommandOutput : IDiagnosticsCommandOutput
{
    private readonly IConsole _console;

    public DiagnosticsCommandOutput(IConsole console)
    {
        _console = console;
    }

    public void ConfigDirExists()
    {
        _console.ShowSuccess("Config directory exists");
        _console.NewLine();
    }

    public void ConfigDirDoesNotExist(bool canBeFixed, string directoryPath)
    {
        _console.ShowFailure("Config directory does not exist");
        
        if (canBeFixed)
            _console.WriteInColour(" - [Fixable]", ConsoleColor.Yellow);
        
        _console.NewLine();
        _console.WriteInColour($"\t{directoryPath}", ConsoleColor.DarkGray);
        _console.NewLine();
    }
}