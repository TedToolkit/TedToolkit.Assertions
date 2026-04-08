// -----------------------------------------------------------------------
// <copyright file="AndConstraint{TSubject}.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions.Constraints;

/// <summary>
/// Returned by assertion methods to enable fluent chaining via <see cref="And"/> or <see cref="AndIt"/>.
/// </summary>
/// <typeparam name="TSubject">The type of the subject being asserted.</typeparam>
public readonly record struct AndConstraint<TSubject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AndConstraint{TSubject}"/> struct.
    /// </summary>
    /// <param name="assertion">The assertion to continue chaining from.</param>
    internal AndConstraint(scoped in ObjectAssertion<TSubject> assertion)
    {
        And = assertion;
    }

    /// <summary>
    /// Gets the assertion for chaining additional checks on the same subject.
    /// </summary>
    public ObjectAssertion<TSubject> And { get; }

    /// <summary>
    /// Gets a pronoun constraint that allows switching the assertion level (e.g. <c>.AndIt.Should</c>).
    /// </summary>
    public PronounConstraint<TSubject> AndIt
    {
        get
        {
            return new(And);
        }
    }

    /// <summary>
    /// Gets the subject value.
    /// </summary>
    public TSubject Subject
    {
        get
        {
            return And.Info.Subject;
        }
    }
}