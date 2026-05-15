// -----------------------------------------------------------------------
// <copyright file="AssertionLoggerScope.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.Logging;

using TedToolkit.Assertions.AssertionData;
using TedToolkit.Assertions.Strategies;

namespace TedToolkit.Assertions.Logging;

/// <summary>
/// An <see cref="AssertionStrategyScope"/> that, while pushed, routes assertion failures through an <see cref="ILogger"/>.
/// MUST failures are logged at error level and rethrown; SHOULD and COULD failures are logged at warning/information levels without throwing.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="AssertionLoggerScope"/> class and pushes it as the current strategy.
/// </remarks>
/// <param name="logger">The logger that will receive assertion failure messages.</param>
public sealed class AssertionLoggerScope(ILogger logger) : AssertionStrategyScope
{
    /// <inheritdoc/>
    public override void HandleAssertionFailure(scoped in SubjectInfo info, scoped in AssertionMessage message)
    {
        var assertMessage = AssertionHelpers.CreateAssertMessage(info, message, false);
        var level = MapLogLevel(message.Type);
        Log(level, assertMessage);

        if (level is not LogLevel.Error)
        {
            return;
        }
#pragma warning disable S3877
        throw new ArgumentException(assertMessage, info.SubjectName);
#pragma warning restore S3877
    }

    /// <inheritdoc/>
    public override void HandleScopeFailures(scoped in AssertionScope scope)
    {
        if (scope.Messages.Count is 0)
        {
            return;
        }

        var assertMessage = AssertionHelpers.CreateAssertMessage(scope, 0, true);
        var level = MapLogLevel(MaxSeverity(scope));
        Log(level, assertMessage);
        if (level is not LogLevel.Error)
        {
            return;
        }

        throw new ArgumentException(assertMessage);
    }

    private void Log(LogLevel level, string message)
    {
#pragma warning disable CA1848, CA2254
        logger.Log(level, message);
#pragma warning restore CA1848, CA2254
    }

    private static LogLevel MapLogLevel(AssertionType type)
    {
        return type switch
        {
            AssertionType.COULD => LogLevel.Information,
            AssertionType.SHOULD => LogLevel.Warning,
            _ => LogLevel.Error,
        };
    }

    private static AssertionType MaxSeverity(scoped in AssertionScope scope)
    {
        return scope.Messages
            .SelectMany(i => i.Value)
            .Max(i => i.Type);
    }
}