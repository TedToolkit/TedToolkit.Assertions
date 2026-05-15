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
/// Low-level helpers used by the assertion infrastructure and source-generated code. Most users will not need to call these directly.
/// </summary>
public static class AssertionHelpers
{
    /// <summary>
    /// Evaluates a single assertion item against the subject and triggers the failure strategy when the assertion does not pass.
    /// </summary>
    /// <param name="assertion">The assertion context.</param>
    /// <param name="item">The assertion item to evaluate.</param>
    /// <param name="reason">An optional user-supplied reason.</param>
    /// <param name="tag">An optional user-supplied tag.</param>
    /// <typeparam name="TSubject">The type of the subject.</typeparam>
    /// <typeparam name="TAssertItem">The concrete assertion item type.</typeparam>
    public static void Assert<TSubject, TAssertItem>(
        scoped in ObjectAssertion<TSubject> assertion,
        ref TAssertItem item,
        string reason,
        object? tag)
        where TAssertItem : struct, IAssertionItem<TSubject>
    {
        if (item.IsPassed(assertion.Info.Subject) != assertion.IsInverted)
        {
            return;
        }

        var message = new AssertionMessage(
            assertion.Type,
            item.GenerateMessage(assertion),
            reason,
            tag);

        var hasCurrent = ScopeValues.Struct<AssertionScope>.HasCurrent;
        if (hasCurrent)
        {
            ScopeValues.Struct<AssertionScope>.Current.AddAssertion(assertion.Info.Info, message);
        }

        if (hasCurrent && !assertion.IsImmediately)
        {
            return;
        }

        AssertionStrategyScope.CurrentOrDefault.HandleAssertionFailure(assertion.Info.Info, message);
    }

    /// <summary>
    /// Creates an <see cref="AndConstraint{TSubject}"/> for fluent chaining after a non-item-extracting assertion.
    /// </summary>
    /// <param name="assertion">The assertion to chain from.</param>
    /// <typeparam name="TSubject">The type of the subject.</typeparam>
    /// <returns>An <see cref="AndConstraint{TSubject}"/> for further chaining.</returns>
    public static AndConstraint<TSubject> CreateConstraint<TSubject>(
        scoped in ObjectAssertion<TSubject> assertion)
    {
        return new(
            assertion with { IsInverted = false });
    }

    /// <summary>
    /// Creates an <see cref="AndConstraint{TSubject, TItem}"/> for fluent chaining after an item-extracting assertion, enabling <c>.Which</c> access.
    /// </summary>
    /// <param name="assertion">The assertion to chain from.</param>
    /// <param name="item">The assertion item that extracted a value.</param>
    /// <typeparam name="TSubject">The type of the subject.</typeparam>
    /// <typeparam name="TItem">The type of the extracted item.</typeparam>
    /// <typeparam name="TAssertItem">The concrete assertion item type.</typeparam>
    /// <returns>An <see cref="AndConstraint{TSubject, TItem}"/> for further chaining.</returns>
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
    /// Translates an <see cref="AssertionType"/> enum value to its localized display string (e.g. "MUST").
    /// </summary>
    /// <param name="type">The assertion type to translate.</param>
    /// <returns>The localized string representation.</returns>
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
    /// Returns the fully-qualified name of a type, including generic arguments (e.g. <c>System.Collections.Generic.List&lt;System.String&gt;</c>).
    /// </summary>
    /// <param name="type">The type to format, or <see langword="null"/>.</param>
    /// <returns>The formatted type name, or <c>"&lt;null&gt;"</c> if <paramref name="type"/> is <see langword="null"/>.</returns>
    public static string GetFullName(Type? type)
    {
        if (type is null)
        {
            return "<null>";
        }

        if (!type.IsGenericType)
        {
            return GetTypeName(type);
        }

        var typeName = GetTypeName(type.GetGenericTypeDefinition());
        typeName = typeName.Substring(0,
            typeName.IndexOf('`', StringComparison.InvariantCulture));

        return ZString.Concat(typeName, '<',
            ZString.Join(", ", type.GetGenericArguments()
                .Select(GetFullName)), '>');

        static string GetTypeName(Type type)
        {
            return type.FullName ?? type.Name;
        }
    }

    /// <summary>
    /// Converts a subject value to its string representation, returning <c>"&lt;null&gt;"</c> for <see langword="null"/>.
    /// </summary>
    /// <param name="subject">The value to convert.</param>
    /// <typeparam name="TSubject">The type of the value.</typeparam>
    /// <returns>The string representation of the value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetObjectString<TSubject>(TSubject subject)
    {
        return subject?.ToString() ?? "<null>";
    }

    /// <summary>
    /// Converts a sequence of values to a bracketed, comma-separated string representation (truncated to 10 items).
    /// </summary>
    /// <param name="subjects">The values to convert.</param>
    /// <typeparam name="TSubject">The element type.</typeparam>
    /// <returns>A formatted string such as <c>[1, 2, 3]</c> or <c>(15)[1, 2, ..., ...]</c> when truncated.</returns>
    public static string GetObjectsString<TSubject>(IEnumerable<TSubject>? subjects)
    {
        const int maxItems = 10;
        if (subjects is null)
        {
            return "<null>";
        }

        using var builder = ZString.CreateStringBuilder();
        builder.Append('[');
        var isNotStarted = false;
        var count = 0;
        foreach (var subject in subjects)
        {
            if (isNotStarted)
            {
                builder.Append(", ");
            }

            isNotStarted = true;

            if (count < maxItems)
            {
                builder.Append(GetObjectString(subject));
            }

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
    /// Builds a sub-operation label from an operation name and a code expression (e.g. <c>SingleBy`x =&gt; x &gt; 0`</c>).
    /// </summary>
    /// <param name="operationName">The operation name prefix.</param>
    /// <param name="code">The code expression to embed.</param>
    /// <returns>The formatted label.</returns>
    public static string OperationCode(string operationName, string code)
    {
        return ZString.Concat(operationName, '`', code, '`');
    }

    /// <summary>
    /// Builds a sub-operation label from an operation name and an item value (e.g. <c>SingleBy[42]</c>).
    /// </summary>
    /// <param name="operationName">The operation name prefix.</param>
    /// <param name="item">The item value to embed.</param>
    /// <typeparam name="TSubject">The type of the item.</typeparam>
    /// <returns>The formatted label.</returns>
    public static string OperationItem<TSubject>(string operationName, TSubject item)
    {
        return ZString.Concat(operationName, '[', GetObjectString(item), ']');
    }

    /// <summary>
    /// Gets or sets the <see cref="DateTimeOffset"/> format string used when rendering timestamps in scoped assertion messages.
    /// </summary>
    public static string TimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss.fff zzz";

    /// <summary>
    /// Formats a single assertion failure into a human-readable string.
    /// </summary>
    /// <param name="info">The subject metadata.</param>
    /// <param name="message">The assertion failure details.</param>
    /// <param name="addCallerInfo">Whether to append the caller's source location.</param>
    /// <returns>The formatted failure message.</returns>
    public static string CreateAssertMessage(scoped in SubjectInfo info, scoped in AssertionMessage message, bool addCallerInfo)
    {
        using var builder = ZString.CreateStringBuilder();
        builder.AppendLine(message.Message);
        if (!string.IsNullOrEmpty(message.Reason))
        {
            builder.AppendLine();
            builder.Append("\tReason: ");
            builder.Append(message.Reason);
        }

        if (addCallerInfo)
        {
            builder.AppendLine();
            builder.Append(info.CallerInfo);
        }

        return builder.ToString();
    }

    /// <summary>
    /// Formats all collected assertion failures from a scope into a single human-readable string, filtering by minimum severity.
    /// </summary>
    /// <param name="scope">The scope containing collected failures.</param>
    /// <param name="minAssertType">The minimum <see cref="AssertionType"/> to include; failures below this level are omitted.</param>
    /// <param name="addCallerInfo">Whether to append each subject's caller source location.</param>
    /// <returns>The formatted aggregated failure message.</returns>
    public static string CreateAssertMessage(scoped in AssertionScope scope, AssertionType minAssertType, bool addCallerInfo)
    {
        var messageCount = 0;
        string body;
        using (var bodyBuilder = ZString.CreateStringBuilder())
        {
            foreach (var (subject, messages) in scope.Messages)
            {
                var isFirst = true;

                foreach (var message in messages.Where(m => m.Type >= minAssertType))
                {
                    if (isFirst)
                    {
                        bodyBuilder.AppendLine();
                        bodyBuilder.Append("Subject [");
                        bodyBuilder.Append(subject.SubjectName);
                        bodyBuilder.Append("] when [");
                        bodyBuilder.Append(subject.CreatedAt, TimeFormat);
                        bodyBuilder.Append("]:");
                        isFirst = false;
                    }

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

                if (isFirst || !addCallerInfo)
                {
                    continue;
                }

                bodyBuilder.AppendLine();
                bodyBuilder.Append('\t');
                bodyBuilder.Append(subject.CallerInfo);
            }

            body = bodyBuilder.ToString();
        }

        string header;
        using (var headerBuilder = ZString.CreateStringBuilder())
        {
            headerBuilder.Append('[');
            headerBuilder.Append(messageCount);
            headerBuilder.Append(" message(s)]");
            headerBuilder.Append(scope.Context);
            header = headerBuilder.ToString();
        }

        return ZString.Concat(header, body);
    }
}