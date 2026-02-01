// -----------------------------------------------------------------------
// <copyright file="BeDefault.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions.CustomAssertionItems;

/// <summary>
/// Be empty.
/// </summary>
/// <typeparam name="TSubject">the type of the subject.</typeparam>
internal readonly struct BeDefault<TSubject> : IAssertionItem<TSubject>
    where TSubject : struct
{
    /// <inheritdoc />
    public bool IsPassed(TSubject subject)
        => EqualityComparer<object>.Default.Equals(subject, default(TSubject));

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
        => assertion.GetAssertionItemMessage(Localization.ExpectedStatements.BeDefault);
}