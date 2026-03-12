using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.Task;
using Soenneker.Redis.Client.Abstract;
using Soenneker.Utils.AsyncSingleton;
using StackExchange.Redis;

namespace Soenneker.Redis.Client;

/// <inheritdoc cref="IRedisClient"/>
public sealed class RedisClient : IRedisClient
{
    private readonly ILogger<RedisClient> _logger;
    private readonly string _connectionString;

    private readonly AsyncSingleton<ConnectionMultiplexer> _client;

    public RedisClient(IConfiguration config, ILogger<RedisClient> logger)
    {
        _logger = logger;
        _connectionString = config.GetValueStrict<string>("Azure:Redis:ConnectionString"); // TODO: not reliant on Azure namespace

        // No closure: method group
        _client = new AsyncSingleton<ConnectionMultiplexer>(ConnectAsync);
    }

    private async ValueTask<ConnectionMultiplexer> ConnectAsync()
    {
        _logger.LogDebug(">> REDIS: Connecting to {endpoint} ...", _connectionString);

        ConfigurationOptions options = ConfigurationOptions.Parse(_connectionString);
        options.AllowAdmin = true;

        return await ConnectionMultiplexer.ConnectAsync(options)
                                          .NoSync();
    }

    public ValueTask<ConnectionMultiplexer> Get(CancellationToken cancellationToken = default) =>
        _client.Get(cancellationToken);

    public ValueTask DisposeAsync()
    {
        _logger.LogDebug(">> REDIS: Disposing...");
        return _client.DisposeAsync();
    }

    public void Dispose()
    {
        _logger.LogDebug(">> REDIS: Disposing...");
        _client.Dispose();
    }
}