---
name: assert-with-fluentvalidation
description: >-
  Use a FluentValidation validator from TedToolkit.Assertions.FluentValidation when an existing AbstractValidator defines the rules for a fluent assertion.
---

# Assert with FluentValidation

- Reference `TedToolkit.Assertions.FluentValidation` and import `TedToolkit.Assertions.FluentValidation` alongside the core assertion namespace.
- Reuse the existing validator and call the package’s `BeValidBy` assertion with the appropriate severity. Check the installed overload before wiring an asynchronous validation path.
- Verify that a failing property is reported with the expected validator message and that a valid value proceeds normally.
