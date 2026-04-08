# TedToolkit.Assertions

A fluent, extensible assertion library for .NET with source-generated extension methods, multi-level assertion severity, and localization support.

## Installation

```
dotnet add package TedToolkit.Assertions
```

## Supported Frameworks

.NET 6 / 7 / 8 / 9 / 10, .NET Framework 4.7.2 / 4.8, .NET Standard 2.0 / 2.1

## Usage

### Assertion Levels

Three entry points express different levels of severity:

```csharp
using TedToolkit.Assertions;

value.Must().Be(expected);    // Strongest -- a hard requirement
value.Should().Be(expected);  // Recommended
value.Could().Be(expected);   // Optional / advisory
```

### Modifiers

```csharp
// Negate an assertion
value.Must().Not.BeNull();

// Force immediate evaluation (skip scoped collection)
value.Must().Immediately.Be(expected);
```

### Chaining

```csharp
value.Must().Not.BeNull()
     .And.Not.BeNullOrEmpty();

// Use .AndIt to start a new assertion chain on the same subject
value.Must().Not.BeNull()
     .AndIt.Should.Not.BeNullOrEmpty();
```

### Built-in Assertions

| Category | Assertions |
| -------- | ---------- |
| Equality | `Be`, `BeEqualTo` |
| Null | `BeNull`, `BeNullOrEmpty` |
| Default | `BeDefault` |
| Comparison | `BeGreaterThan`, `BeGreaterThanOrEqualTo`, `BeLessThan`, `BeLessThanOrEqualTo`, `BeInRange` |
| Type | `BeTypeOf`, `BeAssignableTo` |
| Collection | `Contain`, `ContainSingle`, `AllSatisfy` |
| Membership | `BeOneOf` |
| Enum | `HaveFlag`, `BeDefined` |
| Predicate | `Match` |
| Regex | `MatchRegex` |
| Floating-point | `BeNaN` (double and float) |
| GUID | `BeEmpty` |
| Nullable | `HaveValue` |

### Assertion Scoping

Collect multiple assertion failures and report them together:

```csharp
using TedToolkit.Assertions;

using (new AssertionScope("validating order").Enter())
{
    order.Name.Must().Not.BeNullOrEmpty();
    order.Amount.Must().BeGreaterThan(0);
    order.Items.Must().Not.BeNull();
}
// All failures are reported when the scope exits
```

You can also provide a custom handler for scope exit:

```csharp
using (new AssertionScope("my scope", tag: null, customHandler: scope =>
{
    // Handle collected failures your way
}).Enter())
{
    // ...
}
```

### Configuring Failure Strategy

Replace the default throw-on-failure behavior globally:

```csharp
using TedToolkit.Assertions.Strategies;

AssertionStrategy.ItemStrategy = (in info, in message) =>
{
    Console.WriteLine(AssertionHelpers.CreateAssertMessage(info, message, false));
};
```

### Creating Custom Assertions

Implement `IAssertionItem<TSubject>` as an `internal readonly struct`. Decorate it with `[AssertionMethodName]` to control the generated extension method name. The bundled Roslyn incremental source generator creates the fluent extension method automatically.

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

// Now you can write: value.Must().BePositive();
```

For assertions that extract a value (e.g. `ContainSingle` returning the matched item), implement `IAssertionItem<TSubject, TItem>` instead. This enables `.Which` chaining on the result.

### Attributes

| Attribute | Purpose |
| --------- | ------- |
| `[AssertionMethodName]` | Specifies the generated extension method name (can be applied multiple times for aliases) |
| `[AssertionMethodPriority]` | Sets `OverloadResolutionPriority` on the generated method for disambiguation |
| `[AssertionParameterName]` | Marks a parameter for automatic `CallerArgumentExpression` capture |

## Localization

Assertion messages support localization through [Crowdin](https://crowdin.com/project/tedtoolkitassertions). Currently available in English and Simplified Chinese.

## License

[LGPL-3.0](../COPYING.LESSER)
