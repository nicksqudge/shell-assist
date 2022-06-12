using ShellAssist.Core.Versions;

namespace ShellAssist.Templates;

public interface ITemplateVersionStore
{
    Dictionary<int, Type> FetchAllVersions();
    Template FetchLatest();
}

public class TemplateVersionStore : ITemplateVersionStore
{
    public Dictionary<int, Type> FetchAllVersions()
        => new Dictionary<int, Type>()
        {
            {1, typeof(Version1CommandTemplate)}
        };

    public Template FetchLatest()
    {
        var baseCommand = new Version1CommandTemplate();

        return new Template<Version1CommandTemplate>()
        {
            Version = 1,
            Command = new Version1CommandTemplate()
            {
                Commands = new List<Version1CommandTemplate.SingleCommand>()
                {
                    new Version1CommandTemplate.SingleCommand()
                    {
                        Start = "ping",
                        Args = new []{ "www.google.com" }
                    }
                }
            }
        };
    }
}