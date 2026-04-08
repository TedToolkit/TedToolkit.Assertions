// -----------------------------------------------------------------------
// <copyright file="WhichAssertionResult{TSubject}.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions;

/// <summary>
/// Wraps a value extracted by an <see cref="IAssertionItem{TSubject, TItem}"/> assertion, guarding access so that it throws when the assertion failed.
/// </summary>
/// <typeparam name="TSubject">The type of the extracted value.</typeparam>
public readonly record struct WhichAssertionResult<TSubject>
{
    private readonly bool _succeed;

    /// <summary>
    /// Gets the extracted value. Throws if the assertion that produced this result did not pass.
    /// </summary>
    /// <exception cref="InvalidOperationException">The assertion did not succeed, so no value is available.</exception>
    internal TSubject Value
    {
        get
        {
            return _succeed ? field : throw new InvalidOperationException(Localization.Exceptions.WhichAssertionFailed);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WhichAssertionResult{TSubject}"/> struct with a successfully extracted value.
    /// </summary>
    /// <param name="result">The extracted value.</param>
    private WhichAssertionResult(TSubject result)
    {
        _succeed = true;
        Value = result;
    }

#pragma warning disable CA2225, CS1591
    public static implicit operator WhichAssertionResult<TSubject>(TSubject value)
#pragma warning restore CA2225,CS1591
        => new(value);
}