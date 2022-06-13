using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ShellAssist.Core;
using ShellAssist.Core.ShellCommands;
using ShellAssist.Core.ShellCommands.Versions;
using Xunit;

namespace ShellAssist.Tests;

public class ShellTemplateLoaderTests
{
    [Fact]
    public async Task InvalidTemplate()
    {
        var content = @"{
            'version': 2,
            'command': 'Something here'
        }";

        var supportedTemplates = new Dictionary<int, Type>();
        supportedTemplates.Add(1, typeof(Version1ShellCommandTemplate));

        var templateLoader = new ShellTemplateLoader(supportedTemplates);
        var result = templateLoader.LoadTemplate(content);
        result.Should().BeNull();
    }

    [Fact]
    public async Task UnsupportedTemplate()
    {
        var content = @"{
            'version': 1,
            'command': 'Something here'
        }";

        var supportedTemplates = new Dictionary<int, Type>();

        var templateLoader = new ShellTemplateLoader(supportedTemplates);
        var result = templateLoader.LoadTemplate(content);
        result.Should().BeNull();
    }

    [Fact]
    public async Task Version1Template()
    {
        var content = @"{
            'version': 1,
            'command': {
                'commands': [{
                    'start': 'ping',
                    'args': [
                        'www.google.com'
                    ]
                }]
            }
        }";

        var supportedTemplates = new Dictionary<int, Type>();
        supportedTemplates.Add(1, typeof(Version1ShellCommandTemplate));

        var templateLoader = new ShellTemplateLoader(supportedTemplates);
        var result = templateLoader.LoadTemplate(content);
        result.Should().NotBeNull();
        result.Version.Should().Be(1);
        result.Command.Should().NotBeNull();
        var commands = ((Version1ShellCommandTemplate) result.Command).Commands;
        commands.First().Should().BeEquivalentTo(
            new Version1ShellCommandTemplate.SingleCommand()
            {
                Args = new[] {"www.google.com"},
                Start = "ping"
            });
    }
}