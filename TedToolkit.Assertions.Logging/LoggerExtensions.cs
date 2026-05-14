// -----------------------------------------------------------------------
// <copyright file="LoggerExtensions.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.Logging;

using TedToolkit.Scopes;

namespace TedToolkit.Assertions.Logging;

/// <summary>
/// Extension methods that push a <see cref="LoggerScope"/> as the ambient scope so assertion failures flow into the supplied <see cref="ILogger"/>.
/// </summary>
public static class LoggerExtensions
{
    /// <summary>
    /// Pushes a <see cref="LoggerScope"/> wrapping <paramref name="logger"/> as the current async-flow scope.
    /// Safe across <see langword="await"/> boundaries.
    /// </summary>
    /// <param name="logger">The logger that will receive assertion failure messages.</param>
    /// <returns>A <see cref="ValueScope{TScope}"/> that restores the previous scope on disposal.</returns>
    public static ValueScope<LoggerScope> Push(this ILogger logger)
    {
        return new LoggerScope(logger).Push();
    }

    /// <summary>
    /// Pushes a <see cref="LoggerScope"/> wrapping <paramref name="logger"/> onto a thread-local stack.
    /// Faster than <see cref="Push(ILogger)"/> but cannot flow across <see langword="await"/> boundaries.
    /// </summary>
    /// <param name="logger">The logger that will receive assertion failure messages.</param>
    /// <returns>A <see cref="FastScope{TScope}"/> that restores the previous scope on disposal.</returns>
    public static FastScope<LoggerScope> FastPush(this ILogger logger)
    {
        return new LoggerScope(logger).FastPush();
    }
}