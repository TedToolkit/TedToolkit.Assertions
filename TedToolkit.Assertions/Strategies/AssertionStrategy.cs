// -----------------------------------------------------------------------
// <copyright file="AssertionStrategy.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions.Strategies;

/// <summary>
/// Global configuration point for assertion failure behavior. Replace <see cref="ItemStrategy"/> or <see cref="ScopeStrategy"/> to customize how failures are handled.
/// </summary>
public static class AssertionStrategy
{
    /// <summary>
    /// Gets or sets the handler invoked for individual assertion failures. Defaults to throwing an <see cref="ArgumentException"/>.
    /// </summary>
    public static AssertionItemHandler ItemStrategy { get; set; } = (scoped in info, scoped in message) =>
    {
        var assertMessage = AssertionHelpers.CreateAssertMessage(info, message, false);
        throw new ArgumentException(assertMessage, info.SubjectName);
    };

    /// <summary>
    /// Gets or sets the handler invoked when an assertion scope exits with collected failures. Defaults to throwing an <see cref="ArgumentException"/>.
    /// </summary>
    public static AssertionScopeHandler ScopeStrategy { get; set; } = (scoped in scope) =>
    {
        if (scope.Messages.Count is 0)
        {
            return;
        }

        var assertMessage = AssertionHelpers.CreateAssertMessage(scope, 0, true);
        throw new ArgumentException(assertMessage);
    };
}