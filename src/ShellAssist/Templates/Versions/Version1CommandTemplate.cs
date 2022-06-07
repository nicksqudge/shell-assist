using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ShellAssist.Templates.Versions;

public class Version1CommandTemplate : ICommandTemplate
{
    public string Start { get; set; } = string.Empty;
    public string[] Args { get; set; } = new string[] { };
    public string ToCommand()
    {
        string result = Start;

        if (Args != null && Args.Any())
            result += " " + string.Join(" ", Args);

        return result;
    }
}