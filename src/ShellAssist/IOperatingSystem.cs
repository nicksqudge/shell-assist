namespace ShellAssist;

public interface IOperatingSystem
{
    ShellConfig GetConfig();
}

public class Windows : IOperatingSystem
{
    public ShellConfig GetConfig()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ShellAssist");
        return new ShellConfig(new DirectoryInfo(directory));
    }
}