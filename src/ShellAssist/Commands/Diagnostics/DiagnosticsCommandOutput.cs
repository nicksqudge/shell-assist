namespace ShellAssist.Commands.Diagnostics;

public class DiagnosticsCommandOutput : IDiagnosticsCommandOutput
{
    private readonly ConsoleHelper _console;

    public DiagnosticsCommandOutput(ConsoleHelper console)
    {
        _console = console;
    }

    public void ConfigExists()
    {
        _console.ShowSuccess("Config file exists");
    }

    public void ConfigDoesNotExist()
    {
        _console.ShowFailure("Config does not exist");
    }
}