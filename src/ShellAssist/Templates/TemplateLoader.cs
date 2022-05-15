using Newtonsoft.Json;

namespace ShellAssist.Templates;

public class TemplateLoader
{
    private readonly Dictionary<int, Type> supportedVersions;

    public TemplateLoader(Dictionary<int, Type> supportedVersions)
    {
        this.supportedVersions = supportedVersions;
    }

    public ICommandTemplate LoadTemplate(string content)
    {
        try
        {
            var template = JsonConvert.DeserializeObject<Template>(content);
            if (!supportedVersions.ContainsKey(template.Version))
                return null;

            var commandTemplate = supportedVersions[template.Version];
            return JsonConvert.DeserializeObject(content, commandTemplate) as ICommandTemplate;
        }
        catch
        {
            return null;
        }
    }
}