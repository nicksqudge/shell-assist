using ShellAssist.Templates;

namespace ShellAssist.Core.Localisations;

public class EnglishHandler : ILocalisationHandler
{
    public string InvalidCommandName()
    {
        return "Invalid Command Name";
    }

    public string CommandCreated(CommandFile file)
    {
        return $"{file.Name} was created";
    }

    public string CommandExists(CommandFile file)
    {
        return $"Command {file.Name} already exists";
    }
}