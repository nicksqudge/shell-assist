using ShellAssist.Core.ShellCommands.Versions;

namespace ShellAssist.Core.ShellCommands;

public class ShellCommandVersionStore : IShellCommandVersionStore
{
    public Dictionary<int, Type> FetchAllVersions()
        => new Dictionary<int, Type>()
        {
            {1, typeof(Version1ShellCommandTemplate)}
        };

    public CommandFileJson FetchLatest()
    {
        var baseCommand = new Version1ShellCommandTemplate();

        return new CommandFileJson<Version1ShellCommandTemplate>()
        {
            Version = 1,
            Command = new Version1ShellCommandTemplate()
            {
                Commands = new List<Version1ShellCommandTemplate.SingleCommand>()
                {
                    new Version1ShellCommandTemplate.SingleCommand()
                    {
                        Start = "ping",
                        Args = new []{ "www.google.com" }
                    }
                }
            }
        };
    }
}