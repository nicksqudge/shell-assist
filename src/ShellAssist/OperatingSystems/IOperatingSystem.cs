using ShellAssist.Templates;

namespace ShellAssist.OperatingSystems;

public interface IOperatingSystem
{
    ShellConfig GetConfig();
    void CreateDirectory(string directory);
    Task CreateFile(string directory, string fileName, string contents);
    bool DoesFileExist(string directory, string fileName);
    bool DoesDirectoryExist(string directory);
    void OpenFile(string directory, string fileName);
    Task<IEnumerable<FileInfo>> GetTemplateFilesFromDirectory(string directory);
}