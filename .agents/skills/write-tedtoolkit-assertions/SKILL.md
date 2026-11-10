---
name: write-tedtoolkit-assertions
description: >-
  Write fluent checks and custom assertion items with TedToolkit.Assertions. Use for Must, Should, Could, scoped failures, or missing generated assertion methods.
---

# Write TedToolkit Assertions

- Choose `Must`, `Should`, or `Could` according to the caller’s failure policy. Verify that the configured failure strategy has the expected throw, logging, or collection behavior.
- Use existing assertions first, for example `age.Must().BeGreaterThan(18)`. Use `AssertionScope` when independent failures should be reported together.
- Add a custom `IAssertionItem<TSubject>` when a rule must be reusable. Build the consumer to verify its fluent extensions are generated, then exercise both a successful value and a failure.
- When converting a check that guards a later operation, preserve whether it stops execution immediately or contributes to an aggregate failure.
