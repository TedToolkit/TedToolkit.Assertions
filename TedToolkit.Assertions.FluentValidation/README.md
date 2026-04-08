# TedToolkit.Assertions.FluentValidation

An extension for [TedToolkit.Assertions](https://www.nuget.org/packages/TedToolkit.Assertions) that integrates with [FluentValidation](https://docs.fluentvalidation.net/en/latest/), allowing you to assert that an object passes a FluentValidation validator.

## Installation

```
dotnet add package TedToolkit.Assertions.FluentValidation
```

This package requires [TedToolkit.Assertions](https://www.nuget.org/packages/TedToolkit.Assertions) (installed automatically as a dependency).

## Supported Frameworks

.NET 6 / 7 / 8 / 9 / 10, .NET Framework 4.7.2 / 4.8, .NET Standard 2.0 / 2.1

## Usage

Use the `BeValidBy` assertion to validate an object against a FluentValidation validator:

```csharp
using TedToolkit.Assertions;
using TedToolkit.Assertions.FluentValidation;

// Define a validator
public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Amount).GreaterThan(0);
    }
}

// Assert using the validator
var order = new Order { Name = "Widget", Amount = 5 };
order.Must().BeValidBy(new OrderValidator());
```

You can also pass a validation strategy to control which rules are evaluated:

```csharp
order.Must().BeValidBy(new OrderValidator(), strategy =>
    strategy.IncludeRuleSets("QuickCheck"));
```

When the assertion fails, the error message includes all validation errors returned by FluentValidation.

## License

[LGPL-3.0](../COPYING.LESSER)
