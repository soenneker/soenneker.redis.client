[![](https://img.shields.io/nuget/v/Soenneker.Redis.Client.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Redis.Client/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.redis.client/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.redis.client/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/Soenneker.Redis.Client.svg?style=for-the-badge)](https://www.nuget.org/packages/Soenneker.Redis.Client/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.redis.client/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.redis.client/actions/workflows/codeql.yml)

# Soenneker.Redis.Client

A utility library for Redis client accessibility Implements double checked locking during connect Singleton IoC.

## Install

```bash
dotnet add package Soenneker.Redis.Client
```

## Quick start

```csharp
using Soenneker.Redis.Client.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddRedisClientAsSingleton();
```

Adds `IRedisClient` as a singleton service.

## What you get

- `IRedisClient` — A utility library for Redis client accessibility Implements double checked locking during connect Singleton IoC.
- `RedisClientRegistrar` — Represents the redis client registrar.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `RedisClientRegistrar.AddRedisClientAsSingleton(services)` | Adds `IRedisClient` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `RedisClientRegistrar.AddRedisClientAsScoped(services)` | Registers Redis Client with a scoped lifetime. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Reuse the registered client instead of constructing one per operation.
- Calls that return a cached or singleton value reuse the same instance until the owning service is disposed.
- Dispose instances you own when their scope ends so held resources can be released.
