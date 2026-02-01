// -----------------------------------------------------------------------
// <copyright file="HaveValue.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions.CustomAssertionItems;
#pragma warning disable CA1815

/// <summary>
/// Have value.
/// </summary>
/// <typeparam name="TSubject">the type.</typeparam>
internal struct HaveValue<TSubject> : IAssertionItem<TSubject?, TSubject>
    where TSubject : struct
{
    /// <inheritdoc/>
    public bool IsPassed(TSubject? subject)
    {
        if (subject is not null)
            Item = subject.Value;

        return subject is not null;
    }

    /// <inheritdoc/>
    public readonly string GenerateMessage(scoped in ObjectAssertion<TSubject?> assertion)
        => assertion.GetAssertionItemMessage(Localization.ExpectedStatements.HaveValue);

    /// <inheritdoc/>
    public WhichAssertionResult<TSubject> Item { get; private set; }

    /// <inheritdoc/>
    public readonly string OperatorName
        => "Value";
}