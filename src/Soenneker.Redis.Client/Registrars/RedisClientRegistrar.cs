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
    public static IServiceCollection AddRedisClientAsSingleton(this IServiceCollection services)
    {
        services.TryAddSingleton<IRedisClient, RedisClient>();

        return services;
    }

    /// <summary>
    /// Adds redis client as scoped.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The result of the operation.</returns>
    public static IServiceCollection AddRedisClientAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<IRedisClient, RedisClient>();

        return services;
    }
}