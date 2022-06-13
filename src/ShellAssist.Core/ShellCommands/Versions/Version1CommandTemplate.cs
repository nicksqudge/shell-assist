namespace ShellAssist.Core.ShellCommands.Versions;

public class Version1ShellCommandTemplate : IShellCommandTemplate
{
    public ICollection<SingleCommand> Commands { get; set; }

    public class SingleCommand
    {
        public string Start { get; set; } = string.Empty;
        public string[] Args { get; set; } = new string[] { };
    }
}