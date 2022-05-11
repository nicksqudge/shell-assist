namespace ShellAssist;

public interface IOperatingSystem
{
    ShellConfig GetConfig();
    void CreateDirectory(string directory);
    void CreateFile(string directory, string fileName, string contents);
    bool DoesFileExist(string directory, string fileName);
}

public class Windows : IOperatingSystem
{
    public ShellConfig GetConfig()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "ShellAssist");
        return new ShellConfig(new DirectoryInfo(directory));
    }

    public void CreateDirectory(string directory)
    {
        Directory.CreateDirectory(directory);
    }

    public void CreateFile(string directory, string fileName, string contents)
    {
        File.WriteAllText(fileName, contents);
    }

    public bool DoesFileExist(string directory, string fileName)
    {
        var path = Path.Combine(directory, fileName);
        return File.Exists(path);
    }
}