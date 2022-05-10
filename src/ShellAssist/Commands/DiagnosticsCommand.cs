using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;

namespace ShellAssist.Commands;

[Command("diagnostics", Description = "Run diagnostics on your installation")]
public class DiagnosticsCommand : ICommand
{
    private readonly IDiagnosticsCommandOutput _output;
    private readonly IOperatingSystem _os;

    public DiagnosticsCommand(IDiagnosticsCommandOutput output, IOperatingSystem os)
    {
        this._output = output;
        this._os = os;
    }

    public ValueTask ExecuteAsync(IConsole console)
    {
        var shellConfig = _os.GetConfig();
        if (shellConfig.Exists)
            _output.ConfigExists();
        else
            _output.ConfigDoesNotExist();
        
        return ValueTask.CompletedTask;
    }
}

public interface IDiagnosticsCommandOutput
{
    void ConfigExists();
    void ConfigDoesNotExist();
}