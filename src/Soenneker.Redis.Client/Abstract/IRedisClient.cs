using System;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Soenneker.Redis.Client.Abstract;

/// <summary>
/// A utility library for Redis client accessibility <para/>
/// Implements double checked locking during connect <para/> 
/// Singleton IoC
/// </summary>
public interface IRedisClient : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Returns the configured connection Multiplexer used by the Redis Client.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested connection Multiplexer.</returns>
    ValueTask<ConnectionMultiplexer> Get(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the configured connection Multiplexer used by the Redis Client.
    /// </summary>
    /// <param name="connectionString">Connection string used to open the backing service.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested connection Multiplexer.</returns>
    ValueTask<ConnectionMultiplexer> Get(string connectionString, CancellationToken cancellationToken = default);
}
