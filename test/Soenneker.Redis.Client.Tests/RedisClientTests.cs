using System.Threading.Tasks;
using FluentAssertions;
using Soenneker.Redis.Client.Abstract;
using Soenneker.Tests.FixturedUnit;
using StackExchange.Redis;
using Xunit;


namespace Soenneker.Redis.Client.Tests;

[Collection("Collection")]
public class RedisClientTests : FixturedUnitTest
{
    public RedisClientTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public async ValueTask Get_should_return_client()
    {
        var redisClient = Resolve<IRedisClient>();

        ConnectionMultiplexer client = await redisClient.Get(CancellationToken);

        client.Should().NotBeNull();
    }
}