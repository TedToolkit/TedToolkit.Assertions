// -----------------------------------------------------------------------
// <copyright file="BeAssignableTo.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.Assertions;

/// <summary>
/// Be type of.
/// </summary>
/// <param name="expectedType">expected type.</param>
/// <typeparam name="TSubject">the type.</typeparam>
internal readonly struct BeAssignableTo<TSubject>(Type expectedType)
    : IAssertionItem<TSubject>
{
    /// <inheritdoc/>
    public bool IsPassed(TSubject subject)
    {
        var subjectType = subject?.GetType();
        return expectedType.IsGenericTypeDefinition && (subjectType?.IsGenericType ?? false)
            ? expectedType.IsAssignableFrom(subjectType.GetGenericTypeDefinition())
            : expectedType.IsAssignableFrom(subjectType);
    }

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
    {
        return assertion.GetAssertionItemMessage(Localization.ExpectedStatements.BeAssignableTo(
                AssertionHelpers.GetFullName(expectedType)),
            Localization.ActualStatements.ItIs(AssertionHelpers.GetFullName(assertion.Info.Subject?.GetType())));
    }
}