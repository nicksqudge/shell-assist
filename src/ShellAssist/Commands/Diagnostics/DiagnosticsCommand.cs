using CliFx;
using CliFx.Attributes;
using CliFx.Exceptions;
using CliFx.Infrastructure;
using ShellAssist.OperatingSystems;

namespace ShellAssist.Commands.Diagnostics;

[Command("diagnostics", Description = "Run diagnostics on your installation")]
public class DiagnosticsCommand : ICommand
{
    private readonly IDiagnosticsCommandOutput _output;
    private readonly IOperatingSystem _os;

    public DiagnosticsCommand(IDiagnosticsCommandOutput output, IOperatingSystem os)
    {
        _output = output;
        _os = os;
    }

    public async ValueTask ExecuteAsync(IConsole console)
    {
        bool failed = false;
        
        var shellConfig = _os.GetConfig();
        if (shellConfig.Exists)
            _output.ConfigDirExists();
        else
        {
            _output.ConfigDirDoesNotExist(true, shellConfig.Directory);
            failed = true;
        }
        
        if (failed == true)
            throw new CommandException("Diagnostics failed");
    }
}