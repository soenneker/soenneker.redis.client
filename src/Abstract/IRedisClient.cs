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
    ValueTask<ConnectionMultiplexer> Get(CancellationToken cancellationToken = default);
}