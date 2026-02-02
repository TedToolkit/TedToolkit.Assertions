// -----------------------------------------------------------------------
// <copyright file="BeNullOrEmpty.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions;

/// <summary>
/// Be empty.
/// </summary>
internal readonly struct BeNullOrEmpty : IAssertionItem<string>
{
    /// <inheritdoc />
    public bool IsPassed(string subject)
        => string.IsNullOrEmpty(subject);

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<string> assertion)
        => assertion.GetAssertionItemMessage(Localization.ExpectedStatements.BeNullOrEmpty);
}