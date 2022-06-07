using CliFx.Attributes;
using CliFx.Infrastructure;
using ShellAssist.OperatingSystems;

namespace ShellAssist.Commands.ListCommand;

[Command("list", Description = "Add a command")]
public class ListCommand : BaseCommand
{
    private readonly IOperatingSystem _os;

    public ListCommand(IOperatingSystem os)
    {
        _os = os;
    }

    protected override async ValueTask ExecuteCommandAsync(IConsole console)
    {
        var config = _os.GetConfig();
        if (!config.Exists)
        {
            NoCommandsFoundMessage(console);
            return;
        }

        var commandDirectory = config.GetCommandDirectory();
        if (!_os.DoesDirectoryExist(commandDirectory))
        {
            NoCommandsFoundMessage(console);
            return;
        }

        var files = await _os.GetTemplateFilesFromDirectory(commandDirectory);
        if (!files.Any())
        {
            NoCommandsFoundMessage(console);
            return;
        }

        foreach (var file in files)
            console.WriteListItem(file.Name.Replace(".json", ""));
    }

    private void NoCommandsFoundMessage(IConsole console)
    {
        LogInfo("No commands found");
        console.Output.WriteLine("No commands found");
    }
}