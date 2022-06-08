using ShellAssist.Templates;

namespace ShellAssist.Core;

public class ShellConfig
{
    public static string CommandDirectoryName = "Commands";
    
    public ShellConfig(DirectoryInfo directory)
    {
        Exists = directory.Exists;
        Directory = directory.ToString();
    }

    public ShellConfig()
    {
        
    }
    
    public bool Exists { get; set; }
    public string Directory { get; set; } = string.Empty;
    
    public string GetCommandDirectory() => Path.Combine(Directory, CommandDirectoryName);

    public CommandFile GetCommandFile(string commandName)
        => new CommandFile(GetCommandDirectory(), commandName);
}