---
name: log-tedtoolkit-assertion-failures
description: >-
  Route assertion failures through Microsoft.Extensions.Logging with TedToolkit.Assertions.Logging. Use when an application needs scoped assertion logging.
---

# Log TedToolkit Assertion Failures

- Import `TedToolkit.Assertions.Logging` and use `using (logger.Push())` to install logging for the intended assertion scope.
- Check the desired failure strategy before using `FastPush()`. Keep the logging scope around the checks whose failures should reach that logger.
- Test the emitted log and whether a failing assertion still throws or continues under the chosen strategy.
