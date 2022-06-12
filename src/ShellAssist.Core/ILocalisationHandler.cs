using ShellAssist.Templates;

namespace ShellAssist.Core;

public interface ILocalisationHandler
{
    string InvalidCommandName();

    string CommandCreated(CommandFile command);

    string CommandExists(CommandFile command);

    string CommandDeleted(CommandFile command);

    string CommandFound(CommandFile command);
}