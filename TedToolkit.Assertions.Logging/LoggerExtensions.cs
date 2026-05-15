// -----------------------------------------------------------------------
// <copyright file="LoggerExtensions.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.Logging;

namespace TedToolkit.Assertions.Logging;

/// <summary>
/// Extension methods that push a <see cref="AssertionLoggerScope"/> as the ambient assertion strategy so failures flow into the supplied <see cref="ILogger"/>.
/// </summary>
public static class LoggerExtensions
{
    /// <summary>
    /// Pushes a <see cref="AssertionLoggerScope"/> wrapping <paramref name="logger"/> as the current async-flow strategy.
    /// Dispose the returned scope to restore the previous strategy. Safe across <see langword="await"/> boundaries.
    /// </summary>
    /// <param name="logger">The logger that will receive assertion failure messages.</param>
    /// <returns>The pushed <see cref="AssertionLoggerScope"/>, which restores the previous strategy on disposal.</returns>
    public static AssertionLoggerScope Push(this ILogger logger)
    {
        return new(logger);
    }
}