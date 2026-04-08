// -----------------------------------------------------------------------
// <copyright file="AssertionExtensions.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.CompilerServices;

namespace TedToolkit.Assertions;

/// <summary>
/// Provides the fluent entry-point extension methods (<see cref="Must{TSubject}"/>, <see cref="Should{TSubject}"/>, <see cref="Could{TSubject}"/>) for starting an assertion chain on any value.
/// </summary>
public static class AssertionExtensions
{
    /// <summary>
    ///     Begins a <see cref="AssertionType.MUST"/>-level (required) assertion on the <paramref name="subject"/>.
    /// </summary>
    /// <inheritdoc cref="CreateInfo"/>
    /// <returns>assertion object.</returns>
    public static ObjectAssertion<TSubject> Must<TSubject>(this TSubject subject,
        [CallerArgumentExpression(nameof(subject))]
        string subjectName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
    {
        return new(AssertionType.MUST,
            CreateInfo(subject, subjectName, memberName, filePath, lineNumber),
            false, false);
    }

    /// <summary>
    ///     Begins a <see cref="AssertionType.SHOULD"/>-level (recommended) assertion on the <paramref name="subject"/>.
    /// </summary>
    /// <inheritdoc cref="CreateInfo"/>
    /// <returns>assertion object.</returns>
    public static ObjectAssertion<TSubject> Should<TSubject>(this TSubject subject,
        [CallerArgumentExpression(nameof(subject))]
        string subjectName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
    {
        return new(AssertionType.SHOULD,
            CreateInfo(subject, subjectName, memberName, filePath, lineNumber),
            false, false);
    }

    /// <summary>
    ///     Begins a <see cref="AssertionType.COULD"/>-level (advisory) assertion on the <paramref name="subject"/>.
    /// </summary>
    /// <inheritdoc cref="CreateInfo"/>
    /// <returns>assertion object.</returns>
    public static ObjectAssertion<TSubject> Could<TSubject>(this TSubject subject,
        [CallerArgumentExpression(nameof(subject))]
        string subjectName = "",
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
    {
        return new(AssertionType.COULD,
            CreateInfo(subject, subjectName, memberName, filePath, lineNumber),
            false, false);
    }

    /// <summary>
    /// Creates a <see cref="SubjectInfo{TSubject}"/> that pairs the subject value with its caller metadata.
    /// </summary>
    /// <param name="subject">The subject value.</param>
    /// <param name="subjectName">The expression name of the subject.</param>
    /// <param name="memberName">The caller's member name.</param>
    /// <param name="filePath">The caller's source file path.</param>
    /// <param name="lineNumber">The caller's source line number.</param>
    /// <typeparam name="TSubject">The type of the subject.</typeparam>
    /// <returns>A new <see cref="SubjectInfo{TSubject}"/>.</returns>
    private static SubjectInfo<TSubject> CreateInfo<TSubject>(TSubject subject,
        string subjectName,
        string memberName,
        string filePath,
        int lineNumber)
    {
        return new(
            subject,
            new(
                subjectName,
                new(memberName, filePath, lineNumber),
                DateTimeOffset.Now));
    }
}