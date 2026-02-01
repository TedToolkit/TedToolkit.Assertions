// -----------------------------------------------------------------------
// <copyright file="PronounConstraint{TSubject}.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions.Constraints;

/// <summary>
/// The pronoun constraint.
/// </summary>
/// <typeparam name="TSubject">the type of the subject.</typeparam>
public readonly record struct PronounConstraint<TSubject>
{
    private readonly ObjectAssertion<TSubject> _assertion;

    /// <summary>
    /// Initializes a new instance of the <see cref="PronounConstraint{TSubject}"/> struct.
    /// Constructor.
    /// </summary>
    /// <param name="assertion">the assertion.</param>
    internal PronounConstraint(scoped in ObjectAssertion<TSubject> assertion)
        => _assertion = assertion;

    /// <summary>
    /// Gets the must assertion.
    /// </summary>
    public ObjectAssertion<TSubject> Must
        => _assertion with { Type = AssertionType.MUST, IsInverted = false };

    /// <summary>
    /// Gets the should assertion.
    /// </summary>
    public ObjectAssertion<TSubject> Should
        => _assertion with { Type = AssertionType.SHOULD, IsInverted = false };

    /// <summary>
    /// Gets the could assertion.
    /// </summary>
    public ObjectAssertion<TSubject> Could
        => _assertion with { Type = AssertionType.COULD, IsInverted = false };
}