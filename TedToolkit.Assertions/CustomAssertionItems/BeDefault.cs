// -----------------------------------------------------------------------
// <copyright file="BeDefault.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions;

/// <summary>
/// Asserts that the subject equals its type's default value.
/// </summary>
/// <typeparam name="TSubject">The value type of the subject.</typeparam>
internal readonly struct BeDefault<TSubject> : IAssertionItem<TSubject>
    where TSubject : struct
{
    /// <inheritdoc />
    public bool IsPassed(TSubject subject)
    {
        return EqualityComparer<object>.Default.Equals(subject, default(TSubject));
    }

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
    {
        return assertion.GetAssertionItemMessage(Localization.ExpectedStatements.BeDefault);
    }
}