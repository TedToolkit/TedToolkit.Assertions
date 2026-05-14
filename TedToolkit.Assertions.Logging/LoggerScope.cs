// -----------------------------------------------------------------------
// <copyright file="LoggerScope.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.Logging;

using TedToolkit.Assertions.Strategies;
using TedToolkit.Scopes;

namespace TedToolkit.Assertions.Logging;

/// <summary>
/// Replaces the global <see cref="AssertionStrategy"/> handlers with variants that log assertion failures through an <see cref="ILogger"/> before throwing.
/// </summary>
/// <param name="Logger">Logger</param>
public readonly record struct LoggerScope(ILogger Logger) : IScope
{
    static LoggerScope()
    {
        AssertionStrategy.ItemStrategy = (scoped in info, scoped in message) =>
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
        };

        AssertionStrategy.ScopeStrategy = (scoped in scope) =>
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
        };
    }

    private static void Log(LogLevel level, string message)
    {
        if (!ScopeValues.Struct<LoggerScope>.HasCurrent)
        {
            return;
        }

#pragma warning disable CA1848, CA2254
        ScopeValues.Struct<LoggerScope>.Current.Logger.Log(level, message);
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

    /// <inheritdoc/>
    public void OnEntry()
    {
    }

    /// <inheritdoc/>
    public void OnExit()
    {
    }
}