// -----------------------------------------------------------------------
// <copyright file="AssertItemExtensionGenerator.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.CompilerServices;

using Cysharp.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using TedToolkit.RoslynHelper.Extensions;
using TedToolkit.RoslynHelper.Generators;
using TedToolkit.RoslynHelper.Generators.Syntaxes;

using static TedToolkit.RoslynHelper.Generators.SourceComposer;
using static TedToolkit.RoslynHelper.Generators.SourceComposer<
    TedToolkit.Assertions.Analyzer.AssertItemExtensionGenerator>;

namespace TedToolkit.Assertions.Analyzer;

#pragma warning disable CS8620

/// <summary>
/// The generator for the assert items.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class AssertItemExtensionGenerator : IIncrementalGenerator
{
    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var typeDeclaration = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => s is TypeDeclarationSyntax,
                transform: static (ctx, token) =>
                    ctx.SemanticModel.GetDeclaredSymbol((TypeDeclarationSyntax)ctx.Node,
                        cancellationToken: token));

        context.RegisterSourceOutput(typeDeclaration, Generate);
    }

    private static void Generate(SourceProductionContext context, INamedTypeSymbol? symbol)
    {
        if (symbol is null)
            return;

        if (string.IsNullOrEmpty(symbol.Name))
            return;

        if (symbol.TypeKind is TypeKind.Interface)
            return;

        if (symbol.AllInterfaces
                .FirstOrDefault(i => i.IsGenericType
                                     && i.ConstructUnboundGenericType().FullName is
                                         "TedToolkit.Assertions.IAssertionItem<,>") is
            { } enumerableSymbol)
        {
            GenerateEnumerable(context, symbol, enumerableSymbol);
        }
        else if (symbol.AllInterfaces
                     .FirstOrDefault(i => i.IsGenericType
                                          && i.ConstructUnboundGenericType().FullName is
                                              "TedToolkit.Assertions.IAssertionItem<>") is
                 { } itemSymbol)
        {
            GenerateItem(context, symbol, itemSymbol);
        }
    }

    private static void GenerateEnumerable(in SourceProductionContext context, INamedTypeSymbol declaration,
        INamedTypeSymbol interfaceSymbol)
    {
        GenerateBase(context, declaration, interfaceSymbol, method =>
        {
            method.AddStatement("TedToolkit.Assertions.AssertionHelpers.CreateConstraint".ToSimpleName()
                .Generic(
                    DataType.FromSymbol(interfaceSymbol.TypeArguments[0]),
                    DataType.FromSymbol(interfaceSymbol.TypeArguments[1]),
                    DataType.FromSymbol(declaration))
                .Invoke()
                .AddArgument(Argument("assertion".ToSimpleName()))
                .AddArgument(Argument("assertionItem".ToSimpleName()).Ref)
                .Return);
        });
    }

    private static void GenerateItem(in SourceProductionContext context, INamedTypeSymbol declaration,
        INamedTypeSymbol interfaceSymbol)
    {
        GenerateBase(context, declaration, interfaceSymbol, method =>
        {
            method.AddStatement("TedToolkit.Assertions.AssertionHelpers.CreateConstraint".ToSimpleName()
                .Generic(DataType.FromSymbol(interfaceSymbol.TypeArguments[0]))
                .Invoke()
                .AddArgument(Argument("assertion".ToSimpleName()))
                .Return);
        });
    }

    private static void GenerateBase(in SourceProductionContext context, INamedTypeSymbol declaration,
        INamedTypeSymbol interfaceSymbol, Action<Method> addReturn)
    {
        var extensionName = CreateExtensionName(declaration);

        try
        {
            var typeDeclaration = new TypeDeclaration("GeneratedAssertionExtensions", TypeDeclarationType.CLASS)
                .Public.Static.Partial;

            foreach (var methodName in GetMethodName(declaration))
                typeDeclaration.AddMember(GenerateMethod(declaration, interfaceSymbol, addReturn, methodName));

            File()
                .AddNameSpace(NameSpace(declaration.ContainingNamespace.ToDisplayString())
                    .AddMember(typeDeclaration))
                .Generate(context, extensionName);
        }
#pragma warning disable CA1031
        catch (Exception ex)
#pragma warning restore CA1031
        {
            using var builder = ZString.CreateStringBuilder();
            builder.AppendLine(ex.Message);
            if (!string.IsNullOrEmpty(ex.StackTrace))
                builder.Append(ex.StackTrace);

            context.AddSource(extensionName, builder.ToString());
        }
    }

    private static void AppendPriority(Method method, INamedTypeSymbol declaration)
    {
        if (declaration.GetAttributes().FirstOrDefault(a => a.AttributeClass?.FullName is
                    "TedToolkit.Assertions.Attributes.AssertionMethodPriorityAttribute") is not
                {
                    ConstructorArguments.Length: > 0,
                }

                priorityAttribute || priorityAttribute.ConstructorArguments[0].Value is not int priority)
        {
            return;
        }

        method.AddAttribute(
            Attribute(new DataType("System.Runtime.CompilerServices.OverloadResolutionPriority".ToSimpleName()))
                .AddArgument(Argument(priority.ToLiteral())));
    }

    private static Method GenerateMethod(INamedTypeSymbol declaration,
        INamedTypeSymbol interfaceSymbol, Action<Method> addReturn, string methodName)
    {
        var returnType = new DataType("TedToolkit.Assertions.Constraints.AndConstraint")
            .Generic(interfaceSymbol.TypeArguments.Select(t => DataType.FromSymbol(t)).ToArray());

        var method = Method(methodName, new(returnType)).Public.Static
            .AddAttribute(Attribute<MethodImplAttribute>()
                .AddArgument(Argument(MethodImplOptions.AggressiveInlining.ToExpression())))
            .AddParameter(Parameter(new DataType("TedToolkit.Assertions.ObjectAssertion")
                .Generic(DataType.FromSymbol(interfaceSymbol.TypeArguments[0])), "assertion").This)
            .AddRootDescription(new DescriptionInheritDoc(DataType.FromSymbol(declaration).Type));

        AppendPriority(method, declaration);

        foreach (var declarationTypeParameter in declaration.TypeParameters)
            method.AddTypeParameter(TypeParameter(declarationTypeParameter));

        var constructor = declaration.InstanceConstructors.OrderByDescending(i => i.Parameters.Length).FirstOrDefault();
        var assertItemCreation = new ObjectCreationExpression(DataType.FromSymbol(declaration));

        var lateAddedParameters = new List<Parameter>();

        if (constructor is not null)
        {
            foreach (var constructorParameter in constructor.Parameters)
            {
                var parameter = Parameter(constructorParameter);
                if (constructorParameter.GetAttributes().FirstOrDefault(a => a.AttributeClass?.FullName is
                            "TedToolkit.Assertions.Attributes.AssertionParameterNameAttribute") is
                        {
                            ConstructorArguments.Length: > 0,
                        }

                        attributeData && attributeData.ConstructorArguments[0].Value?.ToString() is { } parameterName)
                {
                    lateAddedParameters.Add(parameter
                        .AddAttribute(
                            Attribute(new DataType("System.Runtime.CompilerServices.CallerArgumentExpression"
                                    .ToSimpleName()))
                                .AddArgument(Argument(parameterName.ToLiteral()))));
                }
                else
                {
                    method.AddParameter(parameter);
                }

                assertItemCreation.AddArgument(Argument(parameter.Name));
            }
        }

        method
            .AddStatement(new VariableExpression(DataType.Var, "assertionItem").Operator("=", assertItemCreation))
            .AddStatement("TedToolkit.Assertions.AssertionHelpers.Assert".ToSimpleName()
                .Generic(DataType.FromSymbol(interfaceSymbol.TypeArguments[0]), DataType.FromSymbol(declaration))
                .Invoke()
                .AddArgument(Argument("assertion".ToSimpleName()))
                .AddArgument(Argument("assertionItem".ToSimpleName()).Ref)
                .AddArgument(Argument("reason".ToSimpleName()))
                .AddArgument(Argument("tag".ToSimpleName())))
            .AddParameter(Parameter(DataType.String, "reason").AddDefault("".ToLiteral()))
            .AddParameter(Parameter(DataType.Object.Null, "tag").AddNull());

        foreach (var lateAddedParameter in lateAddedParameters)
            method.AddParameter(lateAddedParameter);

        addReturn(method);

        return method;
    }

    private static string CreateExtensionName(INamedTypeSymbol symbol)
    {
        using var builder = ZString.CreateStringBuilder();
        builder.Append(symbol.Name);

        if (symbol.IsGenericType)
        {
            foreach (var symbolTypeParameter in symbol.TypeParameters)
                builder.Append(symbolTypeParameter.Name);
        }

        builder.Append("Extension");
        return builder.ToString();
    }

    private static IEnumerable<string> GetMethodName(INamedTypeSymbol symbol)
    {
        var returned = false;
        foreach (var attributeData in symbol.GetAttributes())
        {
            if (attributeData.AttributeClass?.FullName is not
                "TedToolkit.Assertions.Attributes.AssertionMethodNameAttribute")
            {
                continue;
            }

            if (attributeData.ConstructorArguments.Length is 0)
                continue;

            if (attributeData.ConstructorArguments[0].Value?.ToString() is not { } str)
                continue;

            if (string.IsNullOrEmpty(str))
                continue;

            returned = true;
            yield return str;
        }

        if (!returned)
            yield return symbol.Name;
    }
}