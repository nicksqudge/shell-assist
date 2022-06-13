using Newtonsoft.Json;

namespace ShellAssist.Core.OperatingSystems;

public static class OperatingSystemExtensions
{
    public static Task<bool> DoesCommandFileExist(this IOperatingSystem operatingSystem, 
        CommandFile commandFile, 
        CancellationToken cancellationToken)
        => operatingSystem.DoesFileExist(commandFile.Directory, commandFile.FileName, cancellationToken);

    public static Task CreateCommandFile(this IOperatingSystem operatingSystem, 
        CommandFile commandFile,
        CommandFileJson commandFileJson, 
        CancellationToken cancellationToken)
    {
        var data = JsonConvert.SerializeObject(commandFileJson, Formatting.Indented);
        return operatingSystem.CreateFile(commandFile.Directory, commandFile.FileName, data, cancellationToken);
    }

    public static Task OpenCommandFile(this IOperatingSystem operatingSystem, 
        CommandFile commandFile,
        CancellationToken cancellationToken)
    {
        return operatingSystem.OpenFile(commandFile.Directory, commandFile.FileName, cancellationToken);
    }

    public static Task DeleteCommandFile(this IOperatingSystem operatingSystem,
        CommandFile commandFile,
        CancellationToken cancellationToken)
    {
        return operatingSystem.DeleteFile(commandFile.Directory, commandFile.FileName, cancellationToken);
    }
    
    public static Task<string> ReadCommandFile(this IOperatingSystem operatingSystem,
        CommandFile commandFile,
        CancellationToken cancellationToken)
    {
        return operatingSystem.ReadAllFileContents(commandFile.Directory, commandFile.FileName, cancellationToken);
    }
}