using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Soenneker.Dictionaries.Singletons;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.Task;
using Soenneker.Redis.Client.Abstract;
using StackExchange.Redis;

namespace Soenneker.Redis.Client;

/// <inheritdoc cref="IRedisClient"/>
public sealed class RedisClient : IRedisClient
{
    private readonly ILogger<RedisClient> _logger;
    private readonly string _connectionString;

    private readonly SingletonDictionary<ConnectionMultiplexer, string> _clients;

    public RedisClient(IConfiguration config, ILogger<RedisClient> logger)
    {
        _logger = logger;
        _connectionString = config.GetValueStrict<string>("Azure:Redis:ConnectionString"); // TODO: not reliant on Azure namespace

        _clients = new SingletonDictionary<ConnectionMultiplexer, string>(Connect);
    }

    private async ValueTask<ConnectionMultiplexer> Connect(string _, string connectionString, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        ConfigurationOptions options = ConfigurationOptions.Parse(connectionString);
        _logger.LogDebug(">> REDIS: Connecting to {endpoint} ...", options.ToString(false));

        options.AllowAdmin = true;

        return await ConnectionMultiplexer.ConnectAsync(options)
                                          .NoSync();
    }

    public ValueTask<ConnectionMultiplexer> Get(CancellationToken cancellationToken = default) =>
        _clients.Get(_connectionString, _connectionString, cancellationToken);

    public ValueTask<ConnectionMultiplexer> Get(string connectionString, CancellationToken cancellationToken = default) =>
        _clients.Get(connectionString, connectionString, cancellationToken);

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask DisposeAsync()
    {
        _logger.LogDebug(">> REDIS: Disposing...");
        return _clients.DisposeAsync();
    }

    /// <summary>
    /// Releases resources used by the current instance.
    /// </summary>
    public void Dispose()
    {
        _logger.LogDebug(">> REDIS: Disposing...");
        _clients.Dispose();
    }
}
