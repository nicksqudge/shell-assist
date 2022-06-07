using ShellAssist.Core.OperatingSystems;
using ShellAssist.Templates;

namespace ShellAssist;

public static class Extensions
{
    public static Task<bool> DoesCommandFileExist(this IOperatingSystem operatingSystem, CommandFile commandFile, CancellationToken cancellationToken)
        => operatingSystem.DoesFileExist(commandFile.Directory, commandFile.FileName, cancellationToken);
}