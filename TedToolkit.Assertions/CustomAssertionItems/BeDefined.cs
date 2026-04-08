// -----------------------------------------------------------------------
// <copyright file="BeDefined.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions;

/// <summary>
/// Asserts that the enum subject has a defined value.
/// </summary>
/// <typeparam name="TEnum">The enum type.</typeparam>
internal readonly struct BeDefined<TEnum> : IAssertionItem<TEnum>
    where TEnum : Enum
{
    /// <inheritdoc/>
    public bool IsPassed(TEnum subject)
    {
        return Enum.IsDefined(typeof(TEnum), subject);
    }

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TEnum> assertion)
    {
        return assertion.GetAssertionItemMessage(Localization.ExpectedStatements.BeDefined);
    }
}