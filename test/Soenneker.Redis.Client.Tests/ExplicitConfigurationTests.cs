using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
namespace Soenneker.Redis.Client.Tests;
public sealed class ExplicitConfigurationTests
{
    [Test]
    public void DefaultConfigurationIsRequiredOnlyByDefaultGet()
    {
        using var client = new RedisClient(new ConfigurationBuilder().Build(), NullLogger<RedisClient>.Instance);
        try { client.Get(); throw new Exception("Missing default was accepted"); }
        catch (InvalidOperationException) { }
    }
}
