// -----------------------------------------------------------------------
// <copyright file="WhichAssertionResult{TSubject}.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions;

/// <summary>
/// The which assertion result.
/// </summary>
/// <typeparam name="TSubject">the type.</typeparam>
public readonly record struct WhichAssertionResult<TSubject>
{
    private readonly bool _succeed;

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <exception cref="InvalidOperationException">invalid operation.</exception>
    internal TSubject Value
        => _succeed ? field : throw new InvalidOperationException(Localization.Exceptions.WhichAssertionFailed);

    /// <summary>
    /// Initializes a new instance of the <see cref="WhichAssertionResult{TSubject}"/> struct.
    /// the default creation.
    /// </summary>
    /// <param name="result">the result value.</param>
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