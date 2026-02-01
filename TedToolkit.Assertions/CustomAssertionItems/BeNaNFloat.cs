// -----------------------------------------------------------------------
// <copyright file="BeNaNFloat.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.Assertions.Attributes;

namespace TedToolkit.Assertions.CustomAssertionItems;

/// <summary>
/// Be NaN.
/// </summary>
[AssertionMethodName("BeNaN")]
internal readonly struct BeNaNFloat : IAssertionItem<float>
{
    /// <inheritdoc/>
    public bool IsPassed(float subject)
        => float.IsNaN(subject);

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<float> assertion)
        => assertion.GetAssertionItemMessage(Localization.ExpectedStatements.BeNaN);
}