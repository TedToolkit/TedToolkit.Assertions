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
/// The assert scope.
/// </summary>
public readonly record struct AssertionScope() :
    IScope
{
    /// <summary>
    /// Gets the messages.
    /// </summary>
    public Dictionary<SubjectInfo, List<AssertionMessage>> Messages { get; } = [];

    /// <summary>
    /// Gets the context.
    /// </summary>
    public string Context { get; } = "";

    /// <summary>
    /// Gets the tag.
    /// </summary>
    public object? Tag { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AssertionScope"/> struct.
    /// Create by the context.
    /// </summary>
    /// <param name="context">the context.</param>
    /// <param name="tag">the tag.</param>
    public AssertionScope(string context, object? tag = null)
        : this()
    {
        Context = context;
        Tag = tag;
    }

    /// <summary>
    /// Add the assertion.
    /// </summary>
    /// <param name="info">the info.</param>
    /// <param name="message">the message.</param>
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
        => Messages.Clear();

    /// <inheritdoc/>
    public void OnExit()
        => AssertionStrategy.ScopeStrategy?.Invoke(this);
}