namespace ShellAssist.Core.OperatingSystems;

public interface IOperatingSystem
{
    ShellConfig GetConfig();
    Task CreateDirectory(string directory, CancellationToken cancellationToken);
    Task CreateFile(string directory, string fileName, string contents, CancellationToken cancellationToken);
    Task<bool> DoesFileExist(string directory, string fileName, CancellationToken cancellationToken);
    Task<bool> DoesDirectoryExist(string directory, CancellationToken cancellationToken);
    Task OpenFile(string directory, string fileName, CancellationToken cancellationToken);
    Task<IEnumerable<FileInfo>> GetTemplateFilesFromDirectory(string directory, CancellationToken cancellationToken);
}