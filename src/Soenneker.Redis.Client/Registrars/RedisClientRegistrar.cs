using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Redis.Client.Abstract;

namespace Soenneker.Redis.Client.Registrars;

/// <summary>
/// Registers the Redis connection cache.
/// </summary>
public static class RedisClientRegistrar
{
    /// <summary>
    /// Adds <see cref="IRedisClient"/> as a singleton service.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddRedisClientAsSingleton(this IServiceCollection services)
    {
        services.TryAddSingleton<IRedisClient, RedisClient>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IRedisClient"/> with a scoped lifetime, giving each scope its own connection cache.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddRedisClientAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<IRedisClient, RedisClient>();

        return services;
    }
}
