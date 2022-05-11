namespace ShellAssist;

public class ShellConfig
{
    public ShellConfig(DirectoryInfo directory)
    {
        Exists = directory.Exists;
        DisplayDirectory = directory.ToString();
    }

    public ShellConfig()
    {
        
    }
    
    public bool Exists { get; set; }
    public string DisplayDirectory { get; set; }
}