using System.Text.Json.Serialization;

namespace ShellAssist.Templates;

public class Template
{
    public int Version { get; set; }
    
    public string Command { get; set; }
}