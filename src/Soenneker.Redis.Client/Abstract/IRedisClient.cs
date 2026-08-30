using System;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Soenneker.Redis.Client.Abstract;

/// <summary>
/// Provides lazily created, cached Redis connection multiplexers.
/// </summary>
public interface IRedisClient : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the multiplexer for the configured <c>Azure:Redis:ConnectionString</c> value.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>The shared multiplexer for the configured connection string.</returns>
    ValueTask<ConnectionMultiplexer> Get(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the cached multiplexer for a connection string, connecting it on first use.
    /// </summary>
    /// <param name="connectionString">Connection string used to open the backing service.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>The shared multiplexer for <paramref name="connectionString"/>.</returns>
    ValueTask<ConnectionMultiplexer> Get(string connectionString, CancellationToken cancellationToken = default);
}
