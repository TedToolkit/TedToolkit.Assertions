# TedToolkit.Assertions.Logging

An extension for [TedToolkit.Assertions](https://www.nuget.org/packages/TedToolkit.Assertions) that routes assertion failures through [Microsoft.Extensions.Logging](https://learn.microsoft.com/dotnet/core/extensions/logging) instead of (or in addition to) throwing exceptions.

## Installation

```
dotnet add package TedToolkit.Assertions.Logging
```

This package requires [TedToolkit.Assertions](https://www.nuget.org/packages/TedToolkit.Assertions) (installed automatically as a dependency).

## Supported Frameworks

.NET 6 / 7 / 8 / 9 / 10, .NET Framework 4.7.2 / 4.8, .NET Standard 2.0 / 2.1

## Usage

Push a `LoggerScope` around the code whose assertion failures you want logged. The most ergonomic entry points are the `Push` / `FastPush` extension methods on `ILogger`:

```csharp
using TedToolkit.Assertions;
using TedToolkit.Assertions.Logging;

// async-safe (flows across await boundaries)
using (logger.Push())
{
    name.Must().Not.BeNullOrEmpty();
    age.Should().BeGreaterThan(0);
    await DoWorkAsync();
}

// synchronous fast path (thread-local, ref struct — cannot cross await)
using (logger.FastPush())
{
    name.Must().Not.BeNullOrEmpty();
}
```

### Severity → log level

| Assertion | Log level     | Throws?         |
| --------- | ------------- | --------------- |
| `Could`   | `Information` | no              |
| `Should`  | `Warning`     | no              |
| `Must`    | `Error`       | `ArgumentException` |

### Aggregating with `AssertionScope`

Failures inside an `AssertionScope` are collected and reported once on scope exit. The log level reflects the highest severity in the batch:

```csharp
using (logger.Push())
using (new AssertionScope("validating order").Push())
{
    order.Name.Must().Not.BeNullOrEmpty();
    order.Amount.Must().BeGreaterThan(0);
    order.Discount.Should().BeInRange(0, 100);
}
// One log entry with all failures; throws ArgumentException because the batch
// contains MUST failures.
```

## Behavior notes

- The first reference to `LoggerScope` permanently replaces `AssertionStrategy.ItemStrategy` and `AssertionStrategy.ScopeStrategy` for the AppDomain. Don't combine this package with another strategy customization.
- **Outside** a `LoggerScope`, `Could` / `Should` failures are silently dropped (no log, no throw). `Must` still throws an `ArgumentException`. Push a `LoggerScope` before running assertions whose failures you want surfaced.

## License

[LGPL-3.0](../COPYING.LESSER)