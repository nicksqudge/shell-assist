using CliFx;
using CliFx.Attributes;
using CliFx.Exceptions;
using CliFx.Infrastructure;

namespace ShellAssist.Commands.AddCommand;

[Command("add", Description = "Add a command")]
public class AddCommand : BaseCommand
{
    private readonly IOperatingSystem _os;

    public AddCommand(IOperatingSystem os)
    {
        _os = os;
    }

    [CommandParameter(0, Description = "The name of the command")]
    public string Name { get; set; }

    protected override async ValueTask ExecuteCommandAsync(IConsole console)
    {
        var config = GetShellConfig();
        string commandFileName = GetCommandFileName(config);
        ValidateName(commandFileName);
        CreateFile(commandFileName, config);
    }

    private void ValidateName(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            LogError($"{fileName} is invalid");
            throw new CommandException("Invalid command name");
        }

        LogSuccess("Name validated");
    }

    private string GetCommandFileName(ShellConfig config)
    {
        var result = config.FormatCommandName(Name);
        LogInfo($"Name of file set to: {result}");
        return result;
    }

    private ShellConfig GetShellConfig()
    {
        var config = _os.GetConfig();
        if (config.Exists == false)
        {
            LogInfo("Config directory did not exist, creating it");
            _os.CreateDirectory(config.Directory);
        }

        return config;
    }

    private void CreateFile(string file, ShellConfig config)
    {
        var directory = config.GetCommandDirectory();
        if (_os.DoesFileExist(directory, file))
        {
            string error = $"Command already exists at: {directory}/{file}";
            LogError(error);
            throw new CommandException(error);
        }
        
        _os.CreateFile(directory, file, "Some content");
    }
}