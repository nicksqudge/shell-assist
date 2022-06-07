using System.Diagnostics;

namespace ShellAssist.Core.OperatingSystems;

public class Windows : IOperatingSystem
{
    public ShellConfig GetConfig()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "ShellAssist");
        return new ShellConfig(new DirectoryInfo(directory));
    }

    public Task CreateDirectory(string directory, CancellationToken cancellationToken)
    {
        return Task.FromResult(Directory.CreateDirectory(directory));
    }

    public async Task CreateFile(string directory, string fileName, string contents, CancellationToken cancellationToken)
    {
        if (Directory.Exists(directory) == false)
            await CreateDirectory(directory, cancellationToken);
        
        var path = Path.Combine(directory, fileName);
        await File.WriteAllTextAsync(path, contents);
    }

    public Task<bool> DoesFileExist(string directory, string fileName, CancellationToken cancellationToken)
    {
        var path = Path.Combine(directory, fileName);
        return Task.FromResult(File.Exists(path));
    }

    public Task<bool> DoesDirectoryExist(string directory, CancellationToken cancellationToken)
    {
        return Task.FromResult(Directory.Exists(directory));
    }

    public Task OpenFile(string directory, string fileName, CancellationToken cancellationToken)
    {
        var path = Path.Combine(directory, fileName);
        new Process()
        {
            StartInfo = new ProcessStartInfo(path)
            {
                UseShellExecute = true
            }
        }.Start();
        return Task.CompletedTask;
    }

    public Task<IEnumerable<FileInfo>> GetTemplateFilesFromDirectory(string directory, CancellationToken cancellationToken)
    {
        var files = Directory.GetFiles(directory, "*.json").Select(f => new FileInfo(f));
        return Task.FromResult(files);
    }
}