// -----------------------------------------------------------------------
// <copyright file="PronounConstraint{TSubject}.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions.Constraints;

/// <summary>
/// Allows re-entering the assertion chain with a different severity level on the same subject (e.g. <c>.AndIt.Should</c>).
/// </summary>
/// <typeparam name="TSubject">The type of the subject being asserted.</typeparam>
public readonly record struct PronounConstraint<TSubject>
{
    private readonly ObjectAssertion<TSubject> _assertion;

    /// <summary>
    /// Initializes a new instance of the <see cref="PronounConstraint{TSubject}"/> struct.
    /// </summary>
    /// <param name="assertion">The preceding assertion to carry forward.</param>
    internal PronounConstraint(scoped in ObjectAssertion<TSubject> assertion)
    {
        _assertion = assertion;
    }

    /// <summary>
    /// Gets a <see cref="AssertionType.MUST"/>-level assertion on the same subject.
    /// </summary>
    public ObjectAssertion<TSubject> Must
    {
        get
        {
            return _assertion with { Type = AssertionType.MUST, IsInverted = false };
        }
    }

    /// <summary>
    /// Gets a <see cref="AssertionType.SHOULD"/>-level assertion on the same subject.
    /// </summary>
    public ObjectAssertion<TSubject> Should
    {
        get
        {
            return _assertion with { Type = AssertionType.SHOULD, IsInverted = false };
        }
    }

    /// <summary>
    /// Gets a <see cref="AssertionType.COULD"/>-level assertion on the same subject.
    /// </summary>
    public ObjectAssertion<TSubject> Could
    {
        get
        {
            return _assertion with { Type = AssertionType.COULD, IsInverted = false };
        }
    }
}