using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Redis.Client.Abstract;

namespace Soenneker.Redis.Client.Registrars;

public static class RedisClientRegistrar
{
    /// <summary>
    /// Adds <see cref="IRedisClient"/> as a singleton service. <para/>
    /// </summary>
    public static void AddRedisClientAsSingleton(this IServiceCollection services)
    {
        services.TryAddSingleton<IRedisClient, RedisClient>();
    }
}