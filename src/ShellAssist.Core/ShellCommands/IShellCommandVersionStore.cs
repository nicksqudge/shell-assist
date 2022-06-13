namespace ShellAssist.Core.ShellCommands;

public interface IShellCommandVersionStore
{
    Dictionary<int, Type> FetchAllVersions();
    CommandFileJson FetchLatest();
}