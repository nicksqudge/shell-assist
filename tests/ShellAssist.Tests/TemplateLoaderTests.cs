using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ShellAssist.Core.Versions;
using ShellAssist.Templates;
using Xunit;

namespace ShellAssist.Tests;

public class TemplateLoaderTests
{
    [Fact]
    public async Task InvalidTemplate()
    {
        var content = @"{
            'version': 2,
            'command': 'Something here'
        }";

        var supportedTemplates = new Dictionary<int, Type>();
        supportedTemplates.Add(1, typeof(Version1CommandTemplate));

        var templateLoader = new TemplateLoader(supportedTemplates);
        var template = templateLoader.LoadTemplate(content);
        template.Should().BeNull();
    }

    [Fact]
    public async Task UnsupportedTemplate()
    {
        var content = @"{
            'version': 1,
            'command': 'Something here'
        }";

        var supportedTemplates = new Dictionary<int, Type>();

        var templateLoader = new TemplateLoader(supportedTemplates);
        var template = templateLoader.LoadTemplate(content);
        template.Should().BeNull();
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
        supportedTemplates.Add(1, typeof(Version1CommandTemplate));

        var templateLoader = new TemplateLoader(supportedTemplates);
        var template = templateLoader.LoadTemplate(content);
        template.Should().BeOfType<Version1CommandTemplate>();
        ((Version1CommandTemplate) template).Commands.First().Should().BeEquivalentTo(
            new Version1CommandTemplate.SingleCommand()
            {
                Args = new[] {"www.google.com"},
                Start = "ping"
            });
    }
}