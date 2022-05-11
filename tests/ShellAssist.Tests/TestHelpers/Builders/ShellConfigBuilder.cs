using System;

namespace ShellAssist.Tests.TestHelpers.Builders;

public class ShellConfigBuilder
{
    private bool _exists;
    private string _directory;

    public static ShellConfigBuilder Typical()
        => new ShellConfigBuilder()
            .Exists()
            .SetDirectory("/dir");

    public ShellConfigBuilder Exists()
    {
        _exists = true;
        return this;
    }

    public ShellConfigBuilder DoesNotExist()
    {
        _exists = false;
        return this;
    }

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