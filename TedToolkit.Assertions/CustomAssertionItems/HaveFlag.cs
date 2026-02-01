// -----------------------------------------------------------------------
// <copyright file="HaveFlag.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions.CustomAssertionItems;

/// <summary>
/// Have flag.
/// </summary>
/// <param name="flag">the flag.</param>
/// <typeparam name="TEnum">the type of the enum.</typeparam>
internal readonly struct HaveFlag<TEnum>(TEnum flag) : IAssertionItem<TEnum>
    where TEnum : Enum
{
    /// <inheritdoc/>
    public bool IsPassed(TEnum subject)
        => subject.HasFlag(flag);

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TEnum> assertion)
    {
        return assertion.GetAssertionItemMessage(Localization.ExpectedStatements.HaveFlag(
            AssertionHelpers.GetObjectString(flag)));
    }
}