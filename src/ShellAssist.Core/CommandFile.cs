namespace ShellAssist.Core;

public class CommandFile
{
    public readonly string Directory = string.Empty;
    public readonly string FileName = string.Empty;
    public readonly string Name = string.Empty;
    private readonly string _filePath = string.Empty;

    public CommandFile(string directory, string commandName)
    {
        if (string.IsNullOrWhiteSpace(commandName))
            return;
        
        if (commandName.EndsWith(".json"))
            commandName = commandName.Replace(".json", "");
        
        commandName = TidyName(commandName);
        
        _filePath = Path.Combine(directory, $"{commandName}.json");
        Directory = directory;
        Name = commandName;
        FileName = Path.GetFileName(_filePath);
    }

    public CommandFile(FileInfo file)
    {
        _filePath = file.ToString();
        
        FileName = file.Name;
        Directory = file.Directory?.ToString() ?? "";
        Name = Path.GetFileNameWithoutExtension(file.Name);
    }

    public override string ToString()
    {
        return _filePath;
    }
    
    public static string TidyName(string input)
    {
        input = input.Trim();
        input = input.Replace(" ", "-").Replace("!", "").Replace(".", "");
        input = input.ToLower();
        
        foreach (var c in Path.GetInvalidFileNameChars())
        {
            input = input.Replace(c, ' ');
        }

        return input;
    }
}