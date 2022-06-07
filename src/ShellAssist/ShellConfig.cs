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

    public string FormatCommandName(string input)
    {
        input = input.Trim();
        input = input.Replace(" ", "-").Replace("!", "").Replace(".", "");
        input = input.ToLower();
        
        foreach (var c in Path.GetInvalidFileNameChars())
        {
            input = input.Replace(c, ' ');
        }

        return $"{input}.json";
    }

    public string GetCommandDirectory() => Path.Combine(Directory, "Commands");
}