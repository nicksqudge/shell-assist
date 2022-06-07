using ShellAssist.Templates;

namespace ShellAssist.Core;

internal interface ILocalisationHandler
{
    string InvalidCommandName();

    string CommandCreated(CommandFile command);

    string CommandExists(CommandFile command);
}