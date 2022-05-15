using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using ShellAssist.Templates;
using ShellAssist.Templates.Versions;
using Xunit;

namespace ShellAssist.Tests;

public class TemplateLoaderTests
{
    [Fact]
    public async Task ValidTemplate()
    {
        string content = @"{
            'version': 1,
            'command': 'Something here'
        }";

        var supportedTemplates = new Dictionary<int, Type>();
        supportedTemplates.Add(1, typeof(Version1CommandTemplate));
        
        var templateLoader = new TemplateLoader(supportedTemplates);
        var template = templateLoader.LoadTemplate(content);
        template.Should().BeOfType<Version1CommandTemplate>();
    }

    [Fact]
    public async Task InvalidTemplate()
    {
        string content = @"{
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
        string content = @"{
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
        string content = @"{
            'version': 1,
            'command': {
                'start': 'ping',
                'args': [
                    'www.google.com'
                ]
            }
        }";

        var supportedTemplates = new Dictionary<int, Type>();
        supportedTemplates.Add(1, typeof(Version1CommandTemplate));
        
        var templateLoader = new TemplateLoader(supportedTemplates);
        var template = templateLoader.LoadTemplate(content);
        template.Should().BeOfType<Version1CommandTemplate>();
        template.ToCommand().Should().Be("ping www.google.com");
    }
}