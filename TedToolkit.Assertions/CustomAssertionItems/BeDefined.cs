// -----------------------------------------------------------------------
// <copyright file="BeDefined.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions;

/// <summary>
/// Be defined.
/// </summary>
/// <typeparam name="TEnum">the type of the enum.</typeparam>
internal readonly struct BeDefined<TEnum> : IAssertionItem<TEnum>
    where TEnum : Enum
{
    /// <inheritdoc/>
    public bool IsPassed(TEnum subject)
        => Enum.IsDefined(typeof(TEnum), subject);

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TEnum> assertion)
        => assertion.GetAssertionItemMessage(Localization.ExpectedStatements.BeDefined);
}