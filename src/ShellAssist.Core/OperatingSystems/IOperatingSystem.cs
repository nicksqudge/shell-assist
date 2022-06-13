namespace ShellAssist.Core.OperatingSystems;

public interface IOperatingSystem
{
    ShellConfig GetConfig();
    Task CreateDirectory(string directory, CancellationToken cancellationToken);
    Task CreateFile(string directory, string fileName, string contents, CancellationToken cancellationToken);
    Task<bool> DoesFileExist(string directory, string fileName, CancellationToken cancellationToken);
    Task<bool> DoesDirectoryExist(string directory, CancellationToken cancellationToken);
    Task OpenFile(string directory, string fileName, CancellationToken cancellationToken);
    Task DeleteFile(string directory, string fileName, CancellationToken cancellationToken);
    Task<string> ReadAllFileContents(string directory, string fileName, CancellationToken cancellationToken);
    Task<string> ExecutingCommand(string command, string[] args);
    Task<IEnumerable<FileInfo>> GetTemplateFilesFromDirectory(string directory, CancellationToken cancellationToken);
}