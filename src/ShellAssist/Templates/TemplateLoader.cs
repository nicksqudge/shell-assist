using Newtonsoft.Json;

namespace ShellAssist.Templates;

public class TemplateLoader
{
    private readonly Dictionary<int, Type> _supportedVersions;

    public TemplateLoader(Dictionary<int, Type> supportedVersions)
    {
        this._supportedVersions = supportedVersions;
    }

    public ICommandTemplate? LoadTemplate(string content)
    {
        try
        {
            var template = JsonConvert.DeserializeObject<Template>(content);
            if (!_supportedVersions.ContainsKey(template.Version))
                return null;

            var commandTemplateType = _supportedVersions[template.Version];
            var convertType = typeof(Template<>).MakeGenericType(new Type[] { commandTemplateType });
            
            dynamic result = JsonConvert.DeserializeObject(content, convertType);
            if (result == null)
                return null;
            
            return result.Command;
        }
        catch
        {
            return null;
        }
    }
}