// -----------------------------------------------------------------------
// <copyright file="AndConstraint{TSubject}.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions.Constraints;

/// <summary>
/// The 'and' constraint.
/// </summary>
/// <typeparam name="TSubject">the type of the object.</typeparam>
public readonly record struct AndConstraint<TSubject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AndConstraint{TSubject}"/> struct.
    /// Constructor.
    /// </summary>
    /// <param name="assertion">the assertion.</param>
    internal AndConstraint(scoped in ObjectAssertion<TSubject> assertion)
        => And = assertion;

    /// <summary>
    /// Gets 'And' assertion.
    /// </summary>
    public ObjectAssertion<TSubject> And { get; }

    /// <summary>
    /// Gets 'And It' assertion.
    /// </summary>
    public PronounConstraint<TSubject> AndIt
        => new(And);

    /// <summary>
    /// Gets get the Value.
    /// </summary>
    public TSubject Value
        => And.Info.Subject;
}