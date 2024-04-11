using FluentAssertions;
using Soenneker.Redis.Client.Abstract;
using Soenneker.Tests.FixturedUnit;
using StackExchange.Redis;
using Xunit;
using Xunit.Abstractions;

namespace Soenneker.Redis.Client.Tests;

[Collection("Collection")]
public class RedisClientTests : FixturedUnitTest
{
    public RedisClientTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public async void Get_should_return_client()
    {
        var redisClient = Resolve<IRedisClient>();

        ConnectionMultiplexer client = await redisClient.Get();

        client.Should().NotBeNull();
    }
}