// -----------------------------------------------------------------------
// <copyright file="AssertionHelpers.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;
using System.Runtime.CompilerServices;

using Cysharp.Text;

using TedToolkit.Assertions.AssertionData;
using TedToolkit.Assertions.Constraints;
using TedToolkit.Assertions.Strategies;
using TedToolkit.Scopes;

namespace TedToolkit.Assertions;

/// <summary>
/// The helpers for the assertion. In the most time, you don't need it.
/// </summary>
public static class AssertionHelpers
{
    /// <summary>
    /// The basic assert action.
    /// </summary>
    /// <param name="assertion">the assertion.</param>
    /// <param name="item">the assertion item.</param>
    /// <param name="reason">reason.</param>
    /// <param name="tag">tag.</param>
    /// <typeparam name="TSubject">subject type.</typeparam>
    /// <typeparam name="TAssertItem">assertion item type.</typeparam>
    public static void Assert<TSubject, TAssertItem>(
        scoped in ObjectAssertion<TSubject> assertion,
        ref TAssertItem item,
        string reason,
        object? tag)
        where TAssertItem : struct, IAssertionItem<TSubject>
    {
        if (item.IsPassed(assertion.Info.Subject) != assertion.IsInverted)
            return;

        var message = new AssertionMessage(
            assertion.Type,
            item.GenerateMessage(assertion),
            reason,
            tag);

        var hasCurrent = ScopeValues.Struct<AssertionScope>.HasCurrent;
        if (hasCurrent)
            ScopeValues.Struct<AssertionScope>.Current.AddAssertion(assertion.Info.Info, message);

        if (!hasCurrent || assertion.IsImmediately)
            AssertionStrategy.ItemStrategy?.Invoke(assertion.Info.Info, message);
    }

    /// <summary>
    /// Create the constraint.
    /// </summary>
    /// <param name="assertion">assertion.</param>
    /// <typeparam name="TSubject">the type of the subject.</typeparam>
    /// <returns>result.</returns>
    public static AndConstraint<TSubject> CreateConstraint<TSubject>(
        scoped in ObjectAssertion<TSubject> assertion)
    {
        return new(
            assertion with { IsInverted = false });
    }

    /// <summary>
    /// Create the constraint.
    /// </summary>
    /// <param name="assertion">assertion.</param>
    /// <param name="item">assertion item.</param>
    /// <typeparam name="TSubject">the type of the subject.</typeparam>
    /// <typeparam name="TItem">the type of the item.</typeparam>
    /// <typeparam name="TAssertItem">assertion item type.</typeparam>
    /// <returns>result.</returns>
    public static AndConstraint<TSubject, TItem> CreateConstraint<TSubject, TItem, TAssertItem>(
        scoped in ObjectAssertion<TSubject> assertion,
        ref TAssertItem item)
        where TAssertItem : struct, IAssertionItem<TSubject, TItem>
    {
        return new(
            assertion with { IsInverted = false },
            item.Item, item.OperatorName);
    }

    /// <summary>
    /// Translate the assertion type.
    /// </summary>
    /// <param name="type">type.</param>
    /// <returns>result.</returns>
    public static string Translate(AssertionType type)
    {
        return type switch
        {
            AssertionType.COULD => Localization.AssertionTypes.Could,
            AssertionType.SHOULD => Localization.AssertionTypes.Should,
            _ => Localization.AssertionTypes.Must,
        };
    }

    /// <summary>
    /// Get the full name of one type.
    /// </summary>
    /// <param name="type">the type.</param>
    /// <returns>fullname.</returns>
    public static string GetFullName(Type? type)
    {
        if (type is null)
            return "<null>";

        if (!type.IsGenericType)
            return GetTypeName(type);

        var typeName = GetTypeName(type.GetGenericTypeDefinition());
        typeName = typeName.Substring(0,
            typeName.IndexOf('`', StringComparison.InvariantCulture));

        return ZString.Concat(typeName, '<',
            ZString.Join(", ", type.GetGenericArguments()
                .Select(GetFullName)), '>');

        static string GetTypeName(Type type) => type.FullName ?? type.Name;
    }

    /// <summary>
    /// To localization.
    /// </summary>
    /// <param name="subject">the subject.</param>
    /// <typeparam name="TSubject">the type of the subject.</typeparam>
    /// <returns>string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetObjectString<TSubject>(TSubject subject)
        => subject?.ToString() ?? "<null>";

    /// <summary>
    /// To localizations.
    /// </summary>
    /// <param name="subjects">the subject.</param>
    /// <typeparam name="TSubject">the type of the subject.</typeparam>
    /// <returns>string.</returns>
    public static string GetObjectsString<TSubject>(IEnumerable<TSubject>? subjects)
    {
        const int maxItems = 10;
        if (subjects is null)
            return "<null>";

        using var builder = ZString.CreateStringBuilder();
        builder.Append('[');
        var isNotStarted = false;
        var count = 0;
        foreach (var subject in subjects)
        {
            if (isNotStarted)
                builder.Append(", ");

            isNotStarted = true;

            if (count < maxItems)
                builder.Append(GetObjectString(subject));

            count++;
        }

        if (count >= maxItems)
        {
            builder.Append(", ...]");
            return ZString.Concat("(", count, ")", builder.ToString());
        }

        builder.Append(']');
        return builder.ToString();
    }

    /// <summary>
    /// The operation code string.
    /// </summary>
    /// <param name="operationName">operation name.</param>
    /// <param name="code">code.</param>
    /// <returns>string.</returns>
    public static string OperationCode(string operationName, string code)
        => ZString.Concat(operationName, '`', code, '`');

    /// <summary>
    /// The operation item.
    /// </summary>
    /// <param name="operationName">operation name.</param>
    /// <param name="item">item.</param>
    /// <typeparam name="TSubject">the type.</typeparam>
    /// <returns>result.</returns>
    public static string OperationItem<TSubject>(string operationName, TSubject item)
        => ZString.Concat(operationName, '[', GetObjectString(item), ']');

    /// <summary>
    /// Gets or sets the time format for <see cref="CreateAssertMessage(in SubjectInfo, in AssertionMessage)"/> and
    /// <see cref="CreateAssertMessage(in AssertionScope)"/>.
    /// </summary>
    public static string TimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss.fff zzz";

    /// <summary>
    /// Create an assert message.
    /// </summary>
    /// <param name="info">info.</param>
    /// <param name="message">message.</param>
    /// <returns>assert message.</returns>
    public static string CreateAssertMessage(scoped in SubjectInfo info, scoped in AssertionMessage message)
    {
        using var builder = ZString.CreateStringBuilder();
        builder.AppendLine(message.Message);
        builder.Append("when [");
        builder.Append(info.CreatedAt, TimeFormat);
        builder.Append("]");
        if (!string.IsNullOrEmpty(message.Reason))
        {
            builder.AppendLine();
            builder.Append("\tReason: ");
            builder.Append(message.Reason);
        }

        return builder.ToString();
    }

    /// <summary>
    /// Create an assert message.
    /// </summary>
    /// <param name="scope">scope.</param>
    /// <returns>assert message.</returns>
    public static string CreateAssertMessage(scoped in AssertionScope scope)
    {
        var messageCount = 0;
        string body;
        using (var bodyBuilder = ZString.CreateStringBuilder())
        {
            foreach (var (subject, messages) in scope.Messages)
            {
                bodyBuilder.AppendLine();
                bodyBuilder.Append("Subject [");
                bodyBuilder.Append(subject.SubjectName);
                bodyBuilder.Append("] when [");
                bodyBuilder.Append(subject.CreatedAt, TimeFormat);
                bodyBuilder.Append("]:");

                foreach (var message in messages)
                {
                    bodyBuilder.AppendLine();
                    bodyBuilder.Append('\t');
                    bodyBuilder.Append(++messageCount, "D2");
                    bodyBuilder.Append(". ");
                    bodyBuilder.Append(message.Message);
                    if (!string.IsNullOrEmpty(message.Reason))
                    {
                        bodyBuilder.AppendLine();
                        bodyBuilder.Append("\t\tReason: ");
                        bodyBuilder.Append(message.Reason);
                    }
                }

                bodyBuilder.AppendLine();
                bodyBuilder.Append("\tat ");
                bodyBuilder.Append(subject.MemberName);
                bodyBuilder.Append(" in ");
                bodyBuilder.Append(subject.FilePath);
                bodyBuilder.Append(":line ");
                bodyBuilder.Append(subject.LineCount);
            }

            body = bodyBuilder.ToString();
        }

        string header;
        using (var headerBuilder = ZString.CreateStringBuilder())
        {
            headerBuilder.Append('[');
            headerBuilder.Append(DateTimeOffset.Now, TimeFormat);
            headerBuilder.Append("] [");
            headerBuilder.Append(messageCount);
            headerBuilder.Append(" message(s)]");
            headerBuilder.Append(scope.Context);
            header = headerBuilder.ToString();
        }

        return ZString.Concat(header, body);
    }
}