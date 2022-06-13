using Newtonsoft.Json;

namespace ShellAssist.Core.ShellCommands;

public class ShellTemplateLoader
{
    public class LoaderResult
    {
        public int Version { get; set; }
        public object Command { get; set; }
    }
    
    private readonly Dictionary<int, Type> _supportedVersions;

    public ShellTemplateLoader(Dictionary<int, Type> supportedVersions)
    {
        this._supportedVersions = supportedVersions;
    }

    public LoaderResult? LoadTemplate(string content)
    {
        try
        {
            var template = JsonConvert.DeserializeObject<CommandFileJson>(content);
            if (!_supportedVersions.ContainsKey(template.Version))
                return null;

            
            var commandTemplateType = _supportedVersions[template.Version];
            var convertType = typeof(CommandFileJson<>).MakeGenericType(new Type[] { commandTemplateType });
            
            dynamic command = JsonConvert.DeserializeObject(content, convertType);
            if (command == null)
                return null;

            return new LoaderResult
            {
                Version = template.Version,
                Command = command.Command
            };
        }
        catch
        {
            return null;
        }
    }
}