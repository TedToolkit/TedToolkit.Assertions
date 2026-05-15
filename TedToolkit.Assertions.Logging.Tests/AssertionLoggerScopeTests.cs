// -----------------------------------------------------------------------
// <copyright file="AssertionLoggerScopeTests.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.Logging;

using TedToolkit.Assertions.Strategies;
using TedToolkit.Scopes;

namespace TedToolkit.Assertions.Logging.Tests;

/// <summary>
/// Behavioral tests for <see cref="AssertionLoggerScope"/> and the <see cref="LoggerExtensions"/> entry points.
/// </summary>
internal sealed class AssertionLoggerScopeTests
{
    [Test]
    public async Task Push_should_set_current_scope_and_restore_on_dispose()
    {
        await Assert.That(AssertionStrategyScope.Current is AssertionLoggerScope).IsFalse();

        using (new ListLogger().Push())
        {
            await Assert.That(AssertionStrategyScope.Current is AssertionLoggerScope).IsTrue();
        }

        await Assert.That(AssertionStrategyScope.Current is AssertionLoggerScope).IsFalse();
    }

    [Test]
    public async Task Must_failure_should_log_error_and_throw()
    {
        var logger = new ListLogger();

        Exception? captured = null;
        using (logger.Push())
        {
            try
            {
                42.Must().Be(43);
            }
            catch (ArgumentException ex)
            {
                captured = ex;
            }
        }

        await Assert.That(captured).IsNotNull();
        await Assert.That(logger.Entries.Count).IsEqualTo(1);
        await Assert.That(logger.Entries[0].Level).IsEqualTo(LogLevel.Error);
    }

    [Test]
    public async Task Should_failure_should_log_warning_without_throwing()
    {
        var logger = new ListLogger();

        using (logger.Push())
        {
            42.Should().Be(43);
        }

        await Assert.That(logger.Entries.Count).IsEqualTo(1);
        await Assert.That(logger.Entries[0].Level).IsEqualTo(LogLevel.Warning);
    }

    [Test]
    public async Task Could_failure_should_log_information_without_throwing()
    {
        var logger = new ListLogger();

        using (logger.Push())
        {
            42.Could().Be(43);
        }

        await Assert.That(logger.Entries.Count).IsEqualTo(1);
        await Assert.That(logger.Entries[0].Level).IsEqualTo(LogLevel.Information);
    }

    [Test]
    public async Task Passing_assertion_should_not_log()
    {
        var logger = new ListLogger();

        using (logger.Push())
        {
            42.Must().Be(42);
            42.Should().Be(42);
            42.Could().Be(42);
        }

        await Assert.That(logger.Entries.Count).IsEqualTo(0);
    }

    [Test]
    public async Task AssertionScope_inside_LoggerScope_should_aggregate_and_log_once_at_max_severity()
    {
        var logger = new ListLogger();

        Exception? captured = null;
        using (logger.Push())
        {
            try
            {
                using (new AssertionScope("validating").Push())
                {
                    42.Must().Be(43);
                    "x".Should().Be("y");
                    1.Could().Be(2);
                }
            }
            catch (ArgumentException ex)
            {
                captured = ex;
            }
        }

        await Assert.That(captured).IsNotNull();
        await Assert.That(logger.Entries.Count).IsEqualTo(1);
        await Assert.That(logger.Entries[0].Level).IsEqualTo(LogLevel.Error);
    }

    [Test]
    public async Task AssertionScope_with_only_should_failures_should_log_warning_without_throwing()
    {
        var logger = new ListLogger();

        using (logger.Push())
        using (new AssertionScope("validating").Push())
        {
            "x".Should().Be("y");
            1.Could().Be(2);
        }

        await Assert.That(logger.Entries.Count).IsEqualTo(1);
        await Assert.That(logger.Entries[0].Level).IsEqualTo(LogLevel.Warning);
    }

    [Test]
    public async Task Empty_AssertionScope_should_not_log_anything()
    {
        var logger = new ListLogger();

        using (logger.Push())
        using (new AssertionScope("nothing-to-check").Push())
        {
            42.Must().Be(42);
        }

        await Assert.That(logger.Entries.Count).IsEqualTo(0);
    }

    [Test]
    public async Task Without_LoggerScope_Must_should_still_throw()
    {
        Exception? captured = null;
        try
        {
            42.Must().Be(43);
        }
        catch (ArgumentException ex)
        {
            captured = ex;
        }

        await Assert.That(captured).IsNotNull();
    }
}