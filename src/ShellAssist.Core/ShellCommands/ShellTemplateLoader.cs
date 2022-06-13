using Newtonsoft.Json;

namespace ShellAssist.Core.ShellCommands;

public class ShellTemplateLoader
{
    private readonly Dictionary<int, Type> _supportedVersions;

    public ShellTemplateLoader(Dictionary<int, Type> supportedVersions)
    {
        this._supportedVersions = supportedVersions;
    }

    public Type? LoadTemplate(string content)
    {
        try
        {
            var template = JsonConvert.DeserializeObject<CommandFileJson>(content);
            return !_supportedVersions.ContainsKey(template.Version) ? 
                null : 
                _supportedVersions[template.Version];
        }
        catch
        {
            return null;
        }
    }
}