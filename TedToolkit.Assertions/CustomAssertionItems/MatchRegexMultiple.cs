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
/// Asserts that the string subject produces exactly <paramref name="count"/> matches for the regular expression. The matched values are extractable via <c>.Which</c>.
/// </summary>
/// <param name="regularExpression">The regular expression pattern.</param>
/// <param name="count">The expected number of matches.</param>
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
    {
        return assertion.GetAssertionItemMessage(Localization.ExpectedStatements.Match(regularExpression));
    }

    /// <inheritdoc/>
    public WhichAssertionResult<string[]> Item { get; private set; }

    /// <inheritdoc/>
    public string OperatorName
    {
        get
        {
            return AssertionHelpers.OperationCode("Regex", regularExpression);
        }
    }
}