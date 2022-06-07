using System.Diagnostics;
using ShellAssist.Templates;

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

    public Task CreateFile(string directory, string fileName, string contents)
    {
        if (Directory.Exists(directory) == false)
            CreateDirectory(directory);
        
        var path = Path.Combine(directory, fileName);
        return File.WriteAllTextAsync(path, contents);
    }

    public bool DoesFileExist(string directory, string fileName)
    {
        var path = Path.Combine(directory, fileName);
        return File.Exists(path);
    }

    public bool DoesDirectoryExist(string directory)
    {
        return Directory.Exists(directory);
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

    public Task<IEnumerable<FileInfo>> GetTemplateFilesFromDirectory(string directory)
    {
        var files = Directory.GetFiles(directory, "*.json").Select(f => new FileInfo(f));
        return Task.FromResult(files);
    }
}