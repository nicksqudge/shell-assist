using ShellAssist.Templates;

namespace ShellAssist.Core.Versions;

public class Version1CommandTemplate : ICommandTemplate
{
    public ICollection<SingleCommand> Commands { get; set; }

    public class SingleCommand
    {
        public string Start { get; set; } = string.Empty;
        public string[] Args { get; set; } = new string[] { };
    }
}