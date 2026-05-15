// -----------------------------------------------------------------------
// <copyright file="AssertionStrategyScope.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.Assertions.AssertionData;
using TedToolkit.Scopes;

namespace TedToolkit.Assertions.Strategies;

/// <summary>
/// Decides what happens when an assertion fails. The currently-pushed instance
/// (via <see cref="ScopeBase{TScope}"/>) takes effect; subclasses override the
/// virtual handlers to customize behavior (e.g. logging). When no scope is
/// pushed, the default instance throws <see cref="ArgumentException"/>.
/// </summary>
public class AssertionStrategyScope : ScopeBase<AssertionStrategyScope>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AssertionStrategyScope"/> class
    /// and pushes it as the current strategy. Disposing the instance restores the
    /// previous strategy.
    /// </summary>
    protected AssertionStrategyScope()
    {
    }

    private static AssertionStrategyScope Instance { get; } = new();

    /// <summary>
    /// Gets the currently-pushed strategy, or the default instance when none is active.
    /// </summary>
    internal static AssertionStrategyScope CurrentOrDefault
    {
        get
        {
            return Current ?? Instance;
        }
    }

    /// <summary>
    /// Invoked when a single assertion item fails outside (or immediately within) an <see cref="AssertionScope"/>.
    /// The default implementation throws an <see cref="ArgumentException"/> describing the failure.
    /// </summary>
    /// <param name="info">Metadata about the asserted subject.</param>
    /// <param name="message">Details about the assertion failure.</param>
    /// <exception cref="ArgumentException">Always thrown by the default implementation to surface the failure.</exception>
    public virtual void HandleAssertionFailure(scoped in SubjectInfo info, scoped in AssertionMessage message)
    {
        var assertMessage = AssertionHelpers.CreateAssertMessage(info, message, false);
        throw new ArgumentException(assertMessage, info.SubjectName);
    }

    /// <summary>
    /// Invoked when an <see cref="AssertionScope"/> exits with one or more collected failures.
    /// The default implementation throws an <see cref="ArgumentException"/> with the aggregated message.
    /// </summary>
    /// <param name="scope">The exiting scope containing the collected failures.</param>
    /// <exception cref="ArgumentException">Thrown by the default implementation when <paramref name="scope"/> has collected failures.</exception>
    public virtual void HandleScopeFailures(scoped in AssertionScope scope)
    {
        if (scope.Messages.Count is 0)
        {
            return;
        }

        var assertMessage = AssertionHelpers.CreateAssertMessage(scope, 0, true);
        throw new ArgumentException(assertMessage);
    }
}