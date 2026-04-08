# TedToolkit.Assertions

A fluent, extensible assertion library for .NET with source-generated extension methods, multi-level assertion severity, and localization support.

[![Crowdin](https://badges.crowdin.net/tedtoolkitassertions/localized.svg)](https://crowdin.com/project/tedtoolkitassertions)

## Packages

| Package | Description |
| ------- | ----------- |
| [TedToolkit.Assertions](https://www.nuget.org/packages/TedToolkit.Assertions) | Core assertion library |
| [TedToolkit.Assertions.FluentValidation](https://www.nuget.org/packages/TedToolkit.Assertions.FluentValidation) | FluentValidation integration |

## Features

- **Fluent API** with three assertion levels: `Must`, `Should`, `Could`
- **Source-generated extension methods** -- define an assertion item struct and the Roslyn incremental generator creates the fluent API automatically
- **Assertion scoping** -- collect multiple assertion failures and report them together
- **Negation and immediate evaluation** -- chain `.Not` and `.Immediately` modifiers
- **Fluent chaining** -- continue asserting with `.And`, `.AndIt`, and `.Which`
- **Built-in assertions** for equality, null checks, comparisons, collections, types, regex, enums, and more
- **Custom assertion items** -- implement `IAssertionItem<TSubject>` to add your own assertions
- **Configurable failure strategy** -- swap the default throw behavior for logging, collecting, or anything else
- **Localization** -- assertion messages are localizable via [Crowdin](https://crowdin.com/project/tedtoolkitassertions)

## Supported Frameworks

.NET 6 / 7 / 8 / 9 / 10, .NET Framework 4.7.2 / 4.8, .NET Standard 2.0 / 2.1

## Quick Start

```
dotnet add package TedToolkit.Assertions
```

```csharp
using TedToolkit.Assertions;

// Basic assertions
var name = "hello";
name.Must().Be("hello");
name.Should().Not.BeNull();

// Comparison
var age = 25;
age.Must().BeGreaterThan(18);
age.Should().BeInRange(0, 150);

// Collections
var items = new[] { 1, 2, 3 };
items.Must().Contain(2);
items.Should().AllSatisfy<int[], int>(x => x > 0);
items.Must().ContainSingle<int[], int>(x => x == 2);

// Chaining
name.Must().Not.BeNull()
    .And.Not.BeNullOrEmpty();

// Assertion scoping -- collect all failures
using (new AssertionScope("validating user").Enter())
{
    name.Must().Not.BeNullOrEmpty();
    age.Must().BeGreaterThan(0);
}
```

## Creating Custom Assertions

Implement `IAssertionItem<TSubject>` as an `internal readonly struct` and decorate it with `[AssertionMethodName]`. The bundled source generator will create the extension method for you.

```csharp
using TedToolkit.Assertions;
using TedToolkit.Assertions.Attributes;

[AssertionMethodName("BePositive")]
internal readonly struct BePositive<TSubject> : IAssertionItem<TSubject>
    where TSubject : INumber<TSubject>
{
    public bool IsPassed(TSubject subject) => subject > TSubject.Zero;

    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
        => assertion.GetAssertionItemMessage("be positive");
}

// The generator produces an extension method so you can write:
// value.Must().BePositive();
```

## Configuring Failure Strategy

```csharp
using TedToolkit.Assertions.Strategies;

// Replace the default throw-on-failure behavior
AssertionStrategy.ItemStrategy = (scoped in info, scoped in message) =>
{
    // Log, collect, or handle assertion failures your way
    Console.WriteLine(AssertionHelpers.CreateAssertMessage(info, message, false));
};
```

## License

[LGPL-3.0](COPYING.LESSER)
