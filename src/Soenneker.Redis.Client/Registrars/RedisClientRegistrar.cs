using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Redis.Client.Abstract;

namespace Soenneker.Redis.Client.Registrars;

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

    public static IServiceCollection AddRedisClientAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<IRedisClient, RedisClient>();

        return services;
    }
}