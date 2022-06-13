namespace ShellAssist.Core;

public interface ILocalisationHandler
{
    string InvalidCommandName();

    string CommandCreated(CommandFile command);

    string CommandExists(CommandFile command);

    string CommandDeleted(CommandFile command);

    string CommandFound(CommandFile command);

    string CommandFound(CommandFile command, int version);

    string InvalidCommandFile();

    string InvalidCommandTemplateVersion();

    string ExecutingCommand(string command, string[] args);
}