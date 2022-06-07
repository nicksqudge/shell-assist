using ShellAssist.Templates;

namespace ShellAssist.Core;

public interface ILocalisationHandler
{
    string InvalidCommandName();

    string CommandCreated(CommandFile file);

    string CommandExists(CommandFile file);
}