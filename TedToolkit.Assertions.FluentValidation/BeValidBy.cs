// -----------------------------------------------------------------------
// <copyright file="BeValidBy.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Cysharp.Text;

using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;

namespace TedToolkit.Assertions.FluentValidation;

/// <summary>
/// Be valid by validator.
/// </summary>
/// <param name="validator">validator.</param>
/// <param name="options">option.</param>
/// <typeparam name="TSubject">the type.</typeparam>
internal struct BeValidBy<TSubject>(
    IValidator<TSubject> validator,
    Action<ValidationStrategy<TSubject>>? options = null) :
    IAssertionItem<TSubject>
{
    private ValidationResult _result = null!;

    /// <inheritdoc/>
    public bool IsPassed(TSubject subject)
    {
        _result = options is null
            ? validator.Validate(subject)
            : validator.Validate(subject, options);

        return _result.IsValid;
    }

    /// <inheritdoc/>
    public string GenerateMessage(scoped in ObjectAssertion<TSubject> assertion)
    {
        if (assertion.IsInverted)
        {
            return Localization.BeValidBy.Reversed(
                assertion.Info.Info.SubjectName,
                AssertionHelpers.Translate(assertion.Type));
        }

        return Localization.BeValidBy.Normal(
            assertion.Info.Info.SubjectName,
            AssertionHelpers.Translate(assertion.Type),
            ZString.Join('\n', _result.Errors.Select(ToErrorMessage)));
    }

    private static string ToErrorMessage(ValidationFailure failure)
        => failure.ErrorMessage;
}