using ShellAssist.Templates;

namespace ShellAssist;

public class ShellConfig
{
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

    

    public string GetCommandDirectory() => Path.Combine(Directory, "Commands");

    public CommandFile GetCommandFile(string commandName)
        => new CommandFile(Directory, commandName);
}