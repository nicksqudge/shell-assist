namespace ShellAssist.Core.Localisations;

internal class EnglishHandler : ILocalisationHandler
{
    public string InvalidCommandName()
    {
        return "Invalid Command Name";
    }

    public string CommandCreated(CommandFile command)
    {
        return $"{command.Name} was created";
    }

    public string CommandExists(CommandFile command)
    {
        return $"Command {command.Name} already exists";
    }

    public string CommandDeleted(CommandFile command)
    {
        return $"Command {command.Name} was deleted";
    }

    public string CommandFound(CommandFile command)
    {
        return $"Command {command.Name} was found... opening file...";
    }

    public string CommandFound(CommandFile command, int version)
    {
        return $"Command {command.Name} v{version} loaded";
    }

    public string InvalidCommandFile()
    {
        return "Invalid command file";
    }

    public string InvalidCommandTemplateVersion()
    {
        return "Invalid command template version";
    }

    public string ExecutingCommand(string command, string[] args)
    {
        return $"Executing command: {command} {string.Join(' ', args)}";
    }
}