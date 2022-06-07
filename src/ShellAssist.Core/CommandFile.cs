namespace ShellAssist.Templates;

public class CommandFile
{
    public string Directory { get; private set; }
    public string FileName { get; private set; }
    public string Name { get; private set; }
    
    private string _filePath = string.Empty;

    public CommandFile(string directory, string commandName)
    {
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
        Directory = file.Directory.ToString();
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