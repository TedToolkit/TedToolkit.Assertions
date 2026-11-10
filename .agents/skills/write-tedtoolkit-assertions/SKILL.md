---
name: write-tedtoolkit-assertions
description: >-
  Use TedToolkit.Assertions for fluent checks, custom assertion items, batched failures, and FluentValidation or logging integration.
---

# Write TedToolkit Assertions

Choose the failure policy before writing the fluent call. Determine whether a failing check must stop execution, be logged, or join other failures in an `AssertionScope`.

```csharp
using TedToolkit.Assertions;

age.Must().BeGreaterThan(18);
age.Should().BeInRange(0, 150);
```

## Workflow

1. Reuse a built-in check when it expresses the rule. Keep the same subject and failure point when replacing an existing guard.
2. Use an `AssertionScope` for independent checks. Use `.Immediately` only when a later operation cannot run after a failure.
3. Implement `IAssertionItem<TSubject>` for a reusable rule. Build the consumer to verify the generated fluent extension and its message.
4. Verify one passing value and one failing value through the actual failure strategy. For a scoped batch, confirm which failures are surfaced together.

Use [validation and logging integrations](references/integrations.md) when the project already references either adapter.
