# Assertion integrations

## Assert with FluentValidation

- Reference `TedToolkit.Assertions.FluentValidation` and import `TedToolkit.Assertions.FluentValidation` alongside the core assertion namespace.
- Reuse the existing validator and call the package’s `BeValidBy` assertion with the appropriate severity. Check the installed overload before wiring an asynchronous validation path.
- Verify that a failing property is reported with the expected validator message and that a valid value proceeds normally.

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

## Log TedToolkit Assertion Failures

- Import `TedToolkit.Assertions.Logging` and use `using (logger.Push())` to install logging for the intended assertion scope.
- Check the desired failure strategy before using `FastPush()`. Keep the logging scope around the checks whose failures should reach that logger.
- Test the emitted log and whether a failing assertion still throws or continues under the chosen strategy.

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

Keep logging behavior separate from severity. Verify the emitted log and whether the call throws under the configured scope.
