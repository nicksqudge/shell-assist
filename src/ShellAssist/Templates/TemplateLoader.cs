using Newtonsoft.Json;

namespace ShellAssist.Templates;

public class TemplateLoader
{
    private readonly Dictionary<int, Type> supportedVersions;

    public TemplateLoader(Dictionary<int, Type> supportedVersions)
    {
        this.supportedVersions = supportedVersions;
    }

    public Template Load(string content)
    {
        try
        {
            var template = JsonConvert.DeserializeObject<Template>(content);
            if (!supportedVersions.ContainsKey(template.Version))
                return null;

            return template;
        }
        catch
        {
            return null;
        }
    }
}