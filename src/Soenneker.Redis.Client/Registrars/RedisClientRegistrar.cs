using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Redis.Client.Abstract;

namespace Soenneker.Redis.Client.Registrars;

/// <summary>
/// Represents the redis client registrar.
/// </summary>
public static class RedisClientRegistrar
{
    /// <summary>
    /// Adds <see cref="IRedisClient"/> as a singleton service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddRedisClientAsSingleton(this IServiceCollection services)
    {
        services.TryAddSingleton<IRedisClient, RedisClient>();

        return services;
    }

    /// <summary>
    /// Registers Redis Client with a scoped lifetime.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddRedisClientAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<IRedisClient, RedisClient>();

        return services;
    }
}
