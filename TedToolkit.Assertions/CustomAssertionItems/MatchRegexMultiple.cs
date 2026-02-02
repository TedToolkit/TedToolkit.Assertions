// -----------------------------------------------------------------------
// <copyright file="MatchRegexMultiple.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

using TedToolkit.Assertions.Attributes;

namespace TedToolkit.Assertions;

/// <summary>
/// Match Regex.
/// </summary>
/// <param name="regularExpression">regex.</param>
/// <param name="count">count.</param>
[AssertionMethodName("MatchRegex")]
internal struct MatchRegexMultiple([StringSyntax(StringSyntaxAttribute.Regex)] string regularExpression, int count)
    : IAssertionItem<string, string[]>
{
    /// <inheritdoc/>
    public bool IsPassed(string subject)
    {
        var result = new Regex(regularExpression).Matches(subject);
#if NET6_0_OR_GREATER
        Item = result.Select(i => i.Value).ToArray();
#else
        Item = result.Cast<Match>().Select(i => i.Value).ToArray();
#endif
        return result.Count == count;
    }

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<string> assertion)
        => assertion.GetAssertionItemMessage(Localization.ExpectedStatements.Match(regularExpression));

    /// <inheritdoc/>
    public WhichAssertionResult<string[]> Item { get; private set; }

    /// <inheritdoc/>
    public string OperatorName
        => AssertionHelpers.OperationCode("Regex", regularExpression);
}