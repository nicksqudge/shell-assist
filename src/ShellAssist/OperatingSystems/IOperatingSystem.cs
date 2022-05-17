namespace ShellAssist.OperatingSystems;

public interface IOperatingSystem
{
    ShellConfig GetConfig();
    void CreateDirectory(string directory);
    void CreateFile(string directory, string fileName, string contents);
    bool DoesFileExist(string directory, string fileName);
    void OpenFile(string directory, string fileName);
}