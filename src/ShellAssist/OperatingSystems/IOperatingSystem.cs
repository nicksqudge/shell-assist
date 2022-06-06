using ShellAssist.Templates;

namespace ShellAssist.OperatingSystems;

public interface IOperatingSystem
{
    ShellConfig GetConfig();
    void CreateDirectory(string directory);
    void CreateFile(string directory, string fileName, string contents);
    bool DoesFileExist(string directory, string fileName);
    bool DoesDirectoryExist(string directory);
    void OpenFile(string directory, string fileName);
    IEnumerable<FileInfo> GetTemplateFilesFromDirectory(string directory);
}