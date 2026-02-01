// -----------------------------------------------------------------------
// <copyright file="ClassBeNull.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.Assertions.Attributes;

namespace TedToolkit.Assertions.CustomAssertionItems;

/// <summary>
/// be null.
/// </summary>
/// <typeparam name="TSubject">the type.</typeparam>
[AssertionMethodName("BeNull")]
internal readonly struct ClassBeNull<TSubject> : IAssertionItem<TSubject>
    where TSubject : class?
{
    /// <inheritdoc/>
    public bool IsPassed(TSubject subject)
        => subject is null;

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
        => assertion.GetAssertionItemMessage(Localization.ExpectedStatements.BeNull);
}