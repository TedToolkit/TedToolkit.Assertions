// -----------------------------------------------------------------------
// <copyright file="BeEmptyGuid.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.Assertions.Attributes;

namespace TedToolkit.Assertions.CustomAssertionItems;

/// <summary>
/// Be empty.
/// </summary>
[AssertionMethodName("BeEmpty")]
internal readonly struct BeEmptyGuid : IAssertionItem<Guid>
{
    /// <inheritdoc />
    public bool IsPassed(Guid subject)
        => subject == Guid.Empty;

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<Guid> assertion)
        => assertion.GetAssertionItemMessage(Localization.ExpectedStatements.BeEmpty);
}