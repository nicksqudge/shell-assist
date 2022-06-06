using ShellAssist.Templates.Versions;

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
        var baseCommand = new Version1CommandTemplate()
        {
            Args = new string []
            {
                "www.google.com"
            },
            Start = "ping"
        };

        return new Template<Version1CommandTemplate>()
        {
            Version = 1,
            Command = baseCommand
        };
    }
}