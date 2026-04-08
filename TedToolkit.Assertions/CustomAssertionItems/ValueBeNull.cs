// -----------------------------------------------------------------------
// <copyright file="ValueBeNull.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.Assertions.Attributes;

namespace TedToolkit.Assertions;

/// <summary>
/// Asserts that the nullable value-type subject is <see langword="null"/>. When the subject has a value, the inner value is extractable via <c>.Which</c>.
/// </summary>
/// <typeparam name="TSubject">The underlying value type.</typeparam>
[AssertionMethodName("BeNull")]
internal struct ValueBeNull<TSubject> : IAssertionItem<TSubject?, TSubject>
    where TSubject : struct
{
    /// <inheritdoc/>
    public bool IsPassed(TSubject? subject)
    {
        if (subject is not null)
        {
            Item = subject.Value;
        }

        return subject is null;
    }

    /// <inheritdoc/>
    public readonly string GenerateMessage(scoped in ObjectAssertion<TSubject?> assertion)
    {
        return assertion.GetAssertionItemMessage(Localization.ExpectedStatements.BeNull);
    }

    /// <inheritdoc/>
    public WhichAssertionResult<TSubject> Item { get; private set; }

    /// <inheritdoc/>
    public readonly string OperatorName
    {
        get
        {
            return "Value";
        }
    }
}