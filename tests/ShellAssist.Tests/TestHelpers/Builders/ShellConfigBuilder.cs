using System;

namespace ShellAssist.Tests.TestHelpers.Builders;

public class ShellConfigBuilder
{
    private bool _exists;
    private string _directory;
    
    public static string BaseDir = "/test";

    public static ShellConfigBuilder Typical()
        => new ShellConfigBuilder()
            .SetDirectory("/test");

    public ShellConfigBuilder SetDirectory(string dir)
    {
        _directory = dir;
        return this;
    }

    public ShellConfig Build()
    {
        return new ShellConfig()
        {
            Directory = _directory,
            Exists = _exists
        };
    }
}