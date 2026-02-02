// -----------------------------------------------------------------------
// <copyright file="AssertionStrategy.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions.Strategies;

/// <summary>
/// The assertion item strategy.
/// </summary>
public static class AssertionStrategy
{
    /// <summary>
    /// Gets or sets the item strategy.
    /// </summary>
    public static AssertionItemHandler ItemStrategy { get; set; } = (scoped in info, scoped in message) =>
    {
        var assertMessage = AssertionHelpers.CreateAssertMessage(message);
        throw new ArgumentException(assertMessage, info.SubjectName);
    };

    /// <summary>
    /// Gets or sets the scope strategy.
    /// </summary>
    public static AssertionScopeHandler ScopeStrategy { get; set; } = (scoped in scope) =>
    {
        if (scope.Messages.Count is 0)
            return;

        var assertMessage = AssertionHelpers.CreateAssertMessage(scope, 0);
        throw new ArgumentException(assertMessage);
    };
}