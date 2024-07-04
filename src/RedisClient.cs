using System;
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
public class RedisClient : IRedisClient
{
    private readonly AsyncSingleton<ConnectionMultiplexer> _client;

    private readonly ILogger<RedisClient> _logger;

    public RedisClient(IConfiguration config, ILogger<RedisClient> logger)
    {
        _logger = logger;

        _client = new AsyncSingleton<ConnectionMultiplexer>(async () =>
        {
            var connectionString = config.GetValueStrict<string>("Azure:Redis:ConnectionString"); // TODO: not reliant on Azure namespace

            _logger.LogDebug(">> REDIS: Connecting to {endpoint} ...", connectionString);

            ConfigurationOptions options = ConfigurationOptions.Parse(connectionString);
            options.AllowAdmin = true;

            ConnectionMultiplexer client = await ConnectionMultiplexer.ConnectAsync(options).NoSync();

            return client;
        });
    }

    public ValueTask<ConnectionMultiplexer> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        _logger.LogDebug(">> REDIS: Disposing...");

        return _client.DisposeAsync();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _logger.LogDebug(">> REDIS: Disposing...");

        _client.Dispose();
    }
}