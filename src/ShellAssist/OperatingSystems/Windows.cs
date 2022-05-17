using System.Diagnostics;

namespace ShellAssist.OperatingSystems;

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
        if (Directory.Exists(directory) == false)
            CreateDirectory(directory);
        
        var path = Path.Combine(directory, fileName);
        File.WriteAllText(path, contents);
    }

    public bool DoesFileExist(string directory, string fileName)
    {
        var path = Path.Combine(directory, fileName);
        return File.Exists(path);
    }

    public void OpenFile(string directory, string fileName)
    {
        var path = Path.Combine(directory, fileName);
        new Process()
        {
            StartInfo = new ProcessStartInfo(path)
            {
                UseShellExecute = true
            }
        }.Start();
    }
}