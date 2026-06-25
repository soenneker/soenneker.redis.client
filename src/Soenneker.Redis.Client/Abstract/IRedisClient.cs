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
    /// Gets the value.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<ConnectionMultiplexer> Get(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<ConnectionMultiplexer> Get(string connectionString, CancellationToken cancellationToken = default);
}
