using System.Threading.Tasks;
using AwesomeAssertions;
using Soenneker.Redis.Client.Abstract;
using Soenneker.Tests.HostedUnit;
using StackExchange.Redis;


namespace Soenneker.Redis.Client.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class RedisClientTests : HostedUnitTest
{
    public RedisClientTests(Host host) : base(host)
    {
    }

    [Test]
    public async ValueTask Get_should_return_client()
    {
        var redisClient = Resolve<IRedisClient>();

        ConnectionMultiplexer client = await redisClient.Get(System.Threading.CancellationToken.None);

        client.Should().NotBeNull();
    }
}
