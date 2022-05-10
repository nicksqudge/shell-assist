namespace ShellAssist;

public interface IOperatingSystem
{
    ShellConfig GetConfig();
}

public class Windows : IOperatingSystem
{
    public ShellConfig GetConfig()
    {
        return new ShellConfig()
        {
            Exists = false
        };
    }
}