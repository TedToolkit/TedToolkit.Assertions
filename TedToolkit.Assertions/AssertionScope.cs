// -----------------------------------------------------------------------
// <copyright file="AssertionScope.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.Assertions.AssertionData;
using TedToolkit.Assertions.Strategies;
using TedToolkit.Scopes;

namespace TedToolkit.Assertions;

/// <summary>
/// Collects assertion failures within a scope and reports them together when the scope exits. Use with <c>using</c> and <c>.Enter()</c>.
/// </summary>
public readonly record struct AssertionScope() :
    IScope
{
    private readonly AssertionScopeHandler? _custom;

    /// <summary>
    /// Gets the collected assertion failure messages, grouped by subject.
    /// </summary>
    public Dictionary<SubjectInfo, List<AssertionMessage>> Messages { get; } = [];

    /// <summary>
    /// Gets the descriptive label for this scope, included in the aggregated failure message header.
    /// </summary>
    public string Context { get; } = "";

    /// <summary>
    /// Gets an optional user-supplied object for categorizing or filtering scope results.
    /// </summary>
    public object? Tag { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AssertionScope"/> struct with a descriptive context label.
    /// </summary>
    /// <param name="context">A label describing the scope (e.g. "validating order").</param>
    /// <param name="tag">An optional object for categorizing or filtering scope results.</param>
    /// <param name="customHandler">An optional handler to replace the default scope-exit behavior.</param>
    public AssertionScope(string context, object? tag = null, AssertionScopeHandler? customHandler = null)
        : this()
    {
        _custom = customHandler;
        Context = context;
        Tag = tag;
    }

    /// <summary>
    /// Records a failed assertion into this scope's message collection.
    /// </summary>
    /// <param name="info">The subject metadata.</param>
    /// <param name="message">The assertion failure details.</param>
    internal void AddAssertion(scoped in SubjectInfo info, scoped in AssertionMessage message)
    {
        if (!Messages.TryGetValue(info, out var messages))
        {
            messages = [];
            Messages[info] = messages;
        }

        messages.Add(message);
    }

    /// <inheritdoc/>
    public void OnEntry()
    {
        Messages.Clear();
    }

    /// <inheritdoc/>
    public void OnExit()
    {
        (_custom ?? AssertionStrategy.ScopeStrategy)(this);
    }
}