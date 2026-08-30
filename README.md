[![](https://img.shields.io/nuget/v/Soenneker.Redis.Client.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Redis.Client/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.redis.client/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.redis.client/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/Soenneker.Redis.Client.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Redis.Client/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.redis.client/build-and-test.yml?label=build%20and%20test&style=for-the-badge)](https://github.com/soenneker/soenneker.redis.client/actions/workflows/build-and-test.yml)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.redis.client/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.redis.client/actions/workflows/codeql.yml)

# Soenneker.Redis.Client

Provides shared, lazily connected StackExchange.Redis `ConnectionMultiplexer` instances through dependency injection.

## Installation

```bash
dotnet add package Soenneker.Redis.Client
```

## Configuration

The parameterless `Get()` overload reads the connection string from configuration:

```json
{
  "Azure": {
    "Redis": {
      "ConnectionString": "localhost:6379,abortConnect=false"
    }
  }
}
```

Alternatively, pass a StackExchange.Redis connection string directly to `Get(connectionString)`.

## Registration and use

```csharp
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Redis.Client.Abstract;
using Soenneker.Redis.Client.Registrars;
using StackExchange.Redis;

services.AddRedisClientAsSingleton();

IRedisClient redisClient = serviceProvider.GetRequiredService<IRedisClient>();
ConnectionMultiplexer multiplexer = await redisClient.Get(cancellationToken);
IDatabase database = multiplexer.GetDatabase();

await database.StringSetAsync("orders:42:status", "ready");
RedisValue status = await database.StringGetAsync("orders:42:status");
```

The singleton registration is the normal choice: `ConnectionMultiplexer` is designed to be shared. Each distinct connection string is connected once and cached for the lifetime of `IRedisClient`. The scoped registrar is available when a scope must own and dispose its own connection cache.

The client enables StackExchange.Redis administrative commands for compatibility with the Soenneker server utilities. Restrict the configured Redis credentials and network access accordingly.
