using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200023E RID: 574
	internal class CSharpDeclarationComputer : DeclarationComputer
	{
		// Token: 0x06002F19 RID: 12057 RVA: 0x000F1F24 File Offset: 0x000F0124
		public static void ComputeDeclarationsInSpan(SemanticModel model, TextSpan span, bool getSymbol, ArrayBuilder<DeclarationInfo> builder, CancellationToken cancellationToken)
		{
			CSharpDeclarationComputer.ComputeDeclarations(model, null, model.SyntaxTree.GetRoot(cancellationToken), (SyntaxNode node, int? level) => !node.Span.OverlapsWith(span) || CSharpDeclarationComputer.InvalidLevel(level), getSymbol, builder, default(int?), cancellationToken);
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x000F1F6B File Offset: 0x000F016B
		public static void ComputeDeclarationsInNode(SemanticModel model, ISymbol associatedSymbol, SyntaxNode node, bool getSymbol, ArrayBuilder<DeclarationInfo> builder, CancellationToken cancellationToken, int? levelsToCompute = null)
		{
			CSharpDeclarationComputer.ComputeDeclarations(model, associatedSymbol, node, (SyntaxNode n, int? level) => CSharpDeclarationComputer.InvalidLevel(level), getSymbol, builder, levelsToCompute, cancellationToken);
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x000F1F9B File Offset: 0x000F019B
		private static bool InvalidLevel(int? level)
		{
			return level != null && level.Value <= 0;
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x000F1FB8 File Offset: 0x000F01B8
		private static int? DecrementLevel(int? level)
		{
			if (level == null)
			{
				return level;
			}
			int? num = level;
			if (num == null)
			{
				return default(int?);
			}
			return new int?(num.GetValueOrDefault() - 1);
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x000F1FF4 File Offset: 0x000F01F4
		private static void ComputeDeclarations(SemanticModel model, ISymbol associatedSymbol, SyntaxNode node, Func<SyntaxNode, int?, bool> shouldSkip, bool getSymbol, ArrayBuilder<DeclarationInfo> builder, int? levelsToCompute, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (shouldSkip.Invoke(node, levelsToCompute))
			{
				return;
			}
			int? num = CSharpDeclarationComputer.DecrementLevel(levelsToCompute);
			SyntaxKind syntaxKind = node.Kind();
			if (syntaxKind <= SyntaxKind.RemoveAccessorDeclaration)
			{
				if (syntaxKind <= SyntaxKind.NamespaceDeclaration)
				{
					if (syntaxKind != SyntaxKind.CompilationUnit)
					{
						if (syntaxKind != SyntaxKind.NamespaceDeclaration)
						{
							return;
						}
						NamespaceDeclarationSyntax namespaceDeclarationSyntax = (NamespaceDeclarationSyntax)node;
						foreach (MemberDeclarationSyntax memberDeclarationSyntax in namespaceDeclarationSyntax.Members)
						{
							CSharpDeclarationComputer.ComputeDeclarations(model, null, memberDeclarationSyntax, shouldSkip, getSymbol, builder, num, cancellationToken);
						}
						DeclarationInfo declarationInfo = DeclarationComputer.GetDeclarationInfo(model, node, getSymbol, cancellationToken);
						builder.Add(declarationInfo);
						NameSyntax nameSyntax = namespaceDeclarationSyntax.Name;
						INamespaceSymbol namespaceSymbol = declarationInfo.DeclaredSymbol as INamespaceSymbol;
						while (nameSyntax.Kind() == SyntaxKind.QualifiedName)
						{
							nameSyntax = ((QualifiedNameSyntax)nameSyntax).Left;
							INamespaceSymbol namespaceSymbol2 = (getSymbol ? ((namespaceSymbol != null) ? namespaceSymbol.ContainingNamespace : null) : null);
							builder.Add(new DeclarationInfo(nameSyntax, ImmutableArray<SyntaxNode>.Empty, namespaceSymbol2));
							namespaceSymbol = namespaceSymbol2;
						}
						return;
					}
					else
					{
						CompilationUnitSyntax compilationUnitSyntax = (CompilationUnitSyntax)node;
						if (associatedSymbol is IMethodSymbol)
						{
							builder.Add(DeclarationComputer.GetDeclarationInfo(model, node, getSymbol, new CompilationUnitSyntax[] { compilationUnitSyntax }, cancellationToken));
							return;
						}
						foreach (MemberDeclarationSyntax memberDeclarationSyntax2 in compilationUnitSyntax.Members)
						{
							CSharpDeclarationComputer.ComputeDeclarations(model, null, memberDeclarationSyntax2, shouldSkip, getSymbol, builder, num, cancellationToken);
						}
						if (compilationUnitSyntax.AttributeLists.Any())
						{
							IEnumerable<SyntaxNode> attributes = CSharpDeclarationComputer.GetAttributes(compilationUnitSyntax.AttributeLists);
							builder.Add(DeclarationComputer.GetDeclarationInfo(model, node, false, attributes, cancellationToken));
						}
						return;
					}
				}
				else
				{
					switch (syntaxKind)
					{
					case SyntaxKind.ClassDeclaration:
					case SyntaxKind.StructDeclaration:
					case SyntaxKind.InterfaceDeclaration:
						break;
					case SyntaxKind.EnumDeclaration:
					{
						EnumDeclarationSyntax enumDeclarationSyntax = (EnumDeclarationSyntax)node;
						foreach (EnumMemberDeclarationSyntax enumMemberDeclarationSyntax in enumDeclarationSyntax.Members)
						{
							CSharpDeclarationComputer.ComputeDeclarations(model, null, enumMemberDeclarationSyntax, shouldSkip, getSymbol, builder, num, cancellationToken);
						}
						IEnumerable<SyntaxNode> attributes2 = CSharpDeclarationComputer.GetAttributes(enumDeclarationSyntax.AttributeLists);
						builder.Add(DeclarationComputer.GetDeclarationInfo(model, node, getSymbol, attributes2, cancellationToken));
						return;
					}
					case SyntaxKind.DelegateDeclaration:
					{
						DelegateDeclarationSyntax delegateDeclarationSyntax = (DelegateDeclarationSyntax)node;
						IEnumerable<SyntaxNode> enumerable = Enumerable.Concat<SyntaxNode>(Enumerable.Concat<SyntaxNode>(CSharpDeclarationComputer.GetAttributes(delegateDeclarationSyntax.AttributeLists), CSharpDeclarationComputer.GetParameterListInitializersAndAttributes(delegateDeclarationSyntax.ParameterList)), CSharpDeclarationComputer.GetTypeParameterListAttributes(delegateDeclarationSyntax.TypeParameterList));
						builder.Add(DeclarationComputer.GetDeclarationInfo(model, node, getSymbol, enumerable, cancellationToken));
						return;
					}
					default:
						switch (syntaxKind)
						{
						case SyntaxKind.EnumMemberDeclaration:
						{
							EnumMemberDeclarationSyntax enumMemberDeclarationSyntax2 = (EnumMemberDeclarationSyntax)node;
							IEnumerable<SyntaxNode> attributes3 = CSharpDeclarationComputer.GetAttributes(enumMemberDeclarationSyntax2.AttributeLists);
							IEnumerable<SyntaxNode> enumerable2 = Enumerable.Concat<SyntaxNode>(SpecializedCollections.SingletonEnumerable<EqualsValueClauseSyntax>(enumMemberDeclarationSyntax2.EqualsValue), attributes3);
							builder.Add(DeclarationComputer.GetDeclarationInfo(model, node, getSymbol, enumerable2, cancellationToken));
							return;
						}
						case SyntaxKind.FieldDeclaration:
						case SyntaxKind.EventFieldDeclaration:
						{
							BaseFieldDeclarationSyntax baseFieldDeclarationSyntax = (BaseFieldDeclarationSyntax)node;
							IEnumerable<SyntaxNode> attributes4 = CSharpDeclarationComputer.GetAttributes(baseFieldDeclarationSyntax.AttributeLists);
							foreach (VariableDeclaratorSyntax variableDeclaratorSyntax in baseFieldDeclarationSyntax.Declaration.Variables)
							{
								IEnumerable<SyntaxNode> enumerable3 = Enumerable.Concat<SyntaxNode>(SpecializedCollections.SingletonEnumerable<EqualsValueClauseSyntax>(variableDeclaratorSyntax.Initializer), attributes4);
								builder.Add(DeclarationComputer.GetDeclarationInfo(model, variableDeclaratorSyntax, getSymbol, enumerable3, cancellationToken));
							}
							return;
						}
						case SyntaxKind.MethodDeclaration:
						case SyntaxKind.OperatorDeclaration:
						case SyntaxKind.ConversionOperatorDeclaration:
						case SyntaxKind.ConstructorDeclaration:
						case SyntaxKind.DestructorDeclaration:
						{
							BaseMethodDeclarationSyntax baseMethodDeclarationSyntax = (BaseMethodDeclarationSyntax)node;
							IEnumerable<SyntaxNode> enumerable4 = CSharpDeclarationComputer.GetParameterListInitializersAndAttributes(baseMethodDeclarationSyntax.ParameterList);
							enumerable4 = enumerable4.Concat(baseMethodDeclarationSyntax.Body);
							ConstructorDeclarationSyntax constructorDeclarationSyntax = baseMethodDeclarationSyntax as ConstructorDeclarationSyntax;
							if (constructorDeclarationSyntax != null && constructorDeclarationSyntax.Initializer != null)
							{
								enumerable4 = enumerable4.Concat(constructorDeclarationSyntax.Initializer);
							}
							ArrowExpressionClauseSyntax expressionBodySyntax = CSharpDeclarationComputer.GetExpressionBodySyntax(baseMethodDeclarationSyntax);
							if (expressionBodySyntax != null)
							{
								enumerable4 = enumerable4.Concat(expressionBodySyntax);
							}
							enumerable4 = Enumerable.Concat<SyntaxNode>(enumerable4, CSharpDeclarationComputer.GetAttributes(baseMethodDeclarationSyntax.AttributeLists));
							MethodDeclarationSyntax methodDeclarationSyntax = node as MethodDeclarationSyntax;
							if (methodDeclarationSyntax != null && methodDeclarationSyntax.TypeParameterList != null)
							{
								enumerable4 = Enumerable.Concat<SyntaxNode>(enumerable4, CSharpDeclarationComputer.GetTypeParameterListAttributes(methodDeclarationSyntax.TypeParameterList));
							}
							builder.Add(DeclarationComputer.GetDeclarationInfo(model, node, getSymbol, enumerable4, cancellationToken));
							return;
						}
						case (SyntaxKind)8879:
						case (SyntaxKind)8880:
						case (SyntaxKind)8881:
						case (SyntaxKind)8882:
						case (SyntaxKind)8883:
						case (SyntaxKind)8884:
						case (SyntaxKind)8885:
						case (SyntaxKind)8886:
						case (SyntaxKind)8887:
						case (SyntaxKind)8888:
						case SyntaxKind.BaseConstructorInitializer:
						case SyntaxKind.ThisConstructorInitializer:
						case SyntaxKind.AccessorList:
							return;
						case SyntaxKind.PropertyDeclaration:
						{
							PropertyDeclarationSyntax propertyDeclarationSyntax = (PropertyDeclarationSyntax)node;
							if (propertyDeclarationSyntax.AccessorList != null)
							{
								foreach (AccessorDeclarationSyntax accessorDeclarationSyntax in propertyDeclarationSyntax.AccessorList.Accessors)
								{
									CSharpDeclarationComputer.ComputeDeclarations(model, null, accessorDeclarationSyntax, shouldSkip, getSymbol, builder, num, cancellationToken);
								}
							}
							if (propertyDeclarationSyntax.ExpressionBody != null)
							{
								CSharpDeclarationComputer.ComputeDeclarations(model, null, propertyDeclarationSyntax.ExpressionBody, shouldSkip, getSymbol, builder, levelsToCompute, cancellationToken);
							}
							IEnumerable<SyntaxNode> attributes5 = CSharpDeclarationComputer.GetAttributes(propertyDeclarationSyntax.AttributeLists);
							IEnumerable<SyntaxNode> enumerable5 = Enumerable.Concat<SyntaxNode>(SpecializedCollections.SingletonEnumerable<EqualsValueClauseSyntax>(propertyDeclarationSyntax.Initializer), attributes5);
							builder.Add(DeclarationComputer.GetDeclarationInfo(model, node, getSymbol, enumerable5, cancellationToken));
							return;
						}
						case SyntaxKind.EventDeclaration:
						{
							EventDeclarationSyntax eventDeclarationSyntax = (EventDeclarationSyntax)node;
							if (eventDeclarationSyntax.AccessorList != null)
							{
								foreach (AccessorDeclarationSyntax accessorDeclarationSyntax2 in eventDeclarationSyntax.AccessorList.Accessors)
								{
									CSharpDeclarationComputer.ComputeDeclarations(model, null, accessorDeclarationSyntax2, shouldSkip, getSymbol, builder, num, cancellationToken);
								}
							}
							IEnumerable<SyntaxNode> attributes6 = CSharpDeclarationComputer.GetAttributes(eventDeclarationSyntax.AttributeLists);
							builder.Add(DeclarationComputer.GetDeclarationInfo(model, node, getSymbol, attributes6, cancellationToken));
							return;
						}
						case SyntaxKind.IndexerDeclaration:
						{
							IndexerDeclarationSyntax indexerDeclarationSyntax = (IndexerDeclarationSyntax)node;
							if (indexerDeclarationSyntax.AccessorList != null)
							{
								foreach (AccessorDeclarationSyntax accessorDeclarationSyntax3 in indexerDeclarationSyntax.AccessorList.Accessors)
								{
									CSharpDeclarationComputer.ComputeDeclarations(model, null, accessorDeclarationSyntax3, shouldSkip, getSymbol, builder, num, cancellationToken);
								}
							}
							if (indexerDeclarationSyntax.ExpressionBody != null)
							{
								CSharpDeclarationComputer.ComputeDeclarations(model, null, indexerDeclarationSyntax.ExpressionBody, shouldSkip, getSymbol, builder, levelsToCompute, cancellationToken);
							}
							IEnumerable<SyntaxNode> enumerable6 = CSharpDeclarationComputer.GetParameterListInitializersAndAttributes(indexerDeclarationSyntax.ParameterList);
							IEnumerable<SyntaxNode> attributes7 = CSharpDeclarationComputer.GetAttributes(indexerDeclarationSyntax.AttributeLists);
							enumerable6 = Enumerable.Concat<SyntaxNode>(enumerable6, attributes7);
							builder.Add(DeclarationComputer.GetDeclarationInfo(model, node, getSymbol, enumerable6, cancellationToken));
							return;
						}
						case SyntaxKind.GetAccessorDeclaration:
						case SyntaxKind.SetAccessorDeclaration:
						case SyntaxKind.AddAccessorDeclaration:
						case SyntaxKind.RemoveAccessorDeclaration:
							goto IL_05DE;
						default:
							return;
						}
						break;
					}
				}
			}
			else if (syntaxKind <= SyntaxKind.InitAccessorDeclaration)
			{
				if (syntaxKind == SyntaxKind.ArrowExpressionClause)
				{
					BasePropertyDeclarationSyntax basePropertyDeclarationSyntax = node.Parent as BasePropertyDeclarationSyntax;
					if (basePropertyDeclarationSyntax != null)
					{
						builder.Add(CSharpDeclarationComputer.GetExpressionBodyDeclarationInfo(basePropertyDeclarationSyntax, (ArrowExpressionClauseSyntax)node, model, getSymbol, cancellationToken));
					}
					return;
				}
				if (syntaxKind != SyntaxKind.InitAccessorDeclaration)
				{
					return;
				}
				goto IL_05DE;
			}
			else
			{
				if (syntaxKind != SyntaxKind.RecordDeclaration && syntaxKind != SyntaxKind.RecordStructDeclaration)
				{
					return;
				}
				if (associatedSymbol is IMethodSymbol)
				{
					RecordDeclarationSyntax recordDeclarationSyntax = (RecordDeclarationSyntax)node;
					IEnumerable<SyntaxNode> enumerable7 = CSharpDeclarationComputer.GetParameterListInitializersAndAttributes(recordDeclarationSyntax.ParameterList);
					BaseListSyntax baseList = recordDeclarationSyntax.BaseList;
					PrimaryConstructorBaseTypeSyntax primaryConstructorBaseTypeSyntax = ((baseList != null) ? baseList.Types.FirstOrDefault() : null) as PrimaryConstructorBaseTypeSyntax;
					if (primaryConstructorBaseTypeSyntax != null)
					{
						enumerable7 = enumerable7.Concat(primaryConstructorBaseTypeSyntax);
					}
					builder.Add(DeclarationComputer.GetDeclarationInfo(node, associatedSymbol, enumerable7));
					return;
				}
			}
			TypeDeclarationSyntax typeDeclarationSyntax = (TypeDeclarationSyntax)node;
			foreach (MemberDeclarationSyntax memberDeclarationSyntax3 in typeDeclarationSyntax.Members)
			{
				CSharpDeclarationComputer.ComputeDeclarations(model, null, memberDeclarationSyntax3, shouldSkip, getSymbol, builder, num, cancellationToken);
			}
			IEnumerable<SyntaxNode> enumerable8 = Enumerable.Concat<SyntaxNode>(CSharpDeclarationComputer.GetAttributes(typeDeclarationSyntax.AttributeLists), CSharpDeclarationComputer.GetTypeParameterListAttributes(typeDeclarationSyntax.TypeParameterList));
			builder.Add(DeclarationComputer.GetDeclarationInfo(model, node, getSymbol, enumerable8, cancellationToken));
			return;
			IL_05DE:
			AccessorDeclarationSyntax accessorDeclarationSyntax4 = (AccessorDeclarationSyntax)node;
			ArrayBuilder<SyntaxNode> instance = ArrayBuilder<SyntaxNode>.GetInstance();
			instance.AddIfNotNull(accessorDeclarationSyntax4.Body);
			instance.AddIfNotNull(accessorDeclarationSyntax4.ExpressionBody);
			instance.AddRange(CSharpDeclarationComputer.GetAttributes(accessorDeclarationSyntax4.AttributeLists));
			builder.Add(DeclarationComputer.GetDeclarationInfo(model, node, getSymbol, instance, cancellationToken));
			instance.Free();
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x000F278A File Offset: 0x000F098A
		private static IEnumerable<SyntaxNode> GetAttributes(SyntaxList<AttributeListSyntax> attributeLists)
		{
			foreach (AttributeListSyntax attributeListSyntax in attributeLists)
			{
				foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
				{
					yield return attributeSyntax;
				}
				SeparatedSyntaxList<AttributeSyntax>.Enumerator enumerator2 = default(SeparatedSyntaxList<AttributeSyntax>.Enumerator);
			}
			SyntaxList<AttributeListSyntax>.Enumerator enumerator = default(SyntaxList<AttributeListSyntax>.Enumerator);
			yield break;
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x000F279A File Offset: 0x000F099A
		private static IEnumerable<SyntaxNode> GetParameterListInitializersAndAttributes(BaseParameterListSyntax parameterList)
		{
			if (parameterList == null)
			{
				return SpecializedCollections.EmptyEnumerable<SyntaxNode>();
			}
			return Enumerable.SelectMany<ParameterSyntax, SyntaxNode>(parameterList.Parameters, (ParameterSyntax p) => CSharpDeclarationComputer.GetParameterInitializersAndAttributes(p));
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x000F27D4 File Offset: 0x000F09D4
		private static IEnumerable<SyntaxNode> GetParameterInitializersAndAttributes(ParameterSyntax parameter)
		{
			return Enumerable.Concat<SyntaxNode>(SpecializedCollections.SingletonEnumerable<EqualsValueClauseSyntax>(parameter.Default), CSharpDeclarationComputer.GetAttributes(parameter.AttributeLists));
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x000F27F1 File Offset: 0x000F09F1
		private static IEnumerable<SyntaxNode> GetTypeParameterListAttributes(TypeParameterListSyntax typeParameterList)
		{
			if (typeParameterList == null)
			{
				return SpecializedCollections.EmptyEnumerable<SyntaxNode>();
			}
			return Enumerable.SelectMany<TypeParameterSyntax, SyntaxNode>(typeParameterList.Parameters, (TypeParameterSyntax p) => CSharpDeclarationComputer.GetAttributes(p.AttributeLists));
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x000F282C File Offset: 0x000F0A2C
		private static DeclarationInfo GetExpressionBodyDeclarationInfo(BasePropertyDeclarationSyntax declarationWithExpressionBody, ArrowExpressionClauseSyntax expressionBody, SemanticModel model, bool getSymbol, CancellationToken cancellationToken)
		{
			IMethodSymbol methodSymbol;
			if (!getSymbol)
			{
				methodSymbol = null;
			}
			else
			{
				IPropertySymbol propertySymbol = model.GetDeclaredSymbol(declarationWithExpressionBody, cancellationToken) as IPropertySymbol;
				methodSymbol = ((propertySymbol != null) ? propertySymbol.GetMethod : null);
			}
			IMethodSymbol methodSymbol2 = methodSymbol;
			return new DeclarationInfo(expressionBody, ImmutableArray.Create<SyntaxNode>(expressionBody), methodSymbol2);
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x000F2868 File Offset: 0x000F0A68
		internal static ArrowExpressionClauseSyntax GetExpressionBodySyntax(CSharpSyntaxNode node)
		{
			ArrowExpressionClauseSyntax arrowExpressionClauseSyntax = null;
			SyntaxKind syntaxKind = node.Kind();
			switch (syntaxKind)
			{
			case SyntaxKind.MethodDeclaration:
				arrowExpressionClauseSyntax = ((MethodDeclarationSyntax)node).ExpressionBody;
				break;
			case SyntaxKind.OperatorDeclaration:
				arrowExpressionClauseSyntax = ((OperatorDeclarationSyntax)node).ExpressionBody;
				break;
			case SyntaxKind.ConversionOperatorDeclaration:
				arrowExpressionClauseSyntax = ((ConversionOperatorDeclarationSyntax)node).ExpressionBody;
				break;
			case SyntaxKind.ConstructorDeclaration:
				arrowExpressionClauseSyntax = ((ConstructorDeclarationSyntax)node).ExpressionBody;
				break;
			default:
				switch (syntaxKind)
				{
				case SyntaxKind.DestructorDeclaration:
					return ((DestructorDeclarationSyntax)node).ExpressionBody;
				case SyntaxKind.PropertyDeclaration:
					return ((PropertyDeclarationSyntax)node).ExpressionBody;
				case SyntaxKind.EventDeclaration:
					break;
				case SyntaxKind.IndexerDeclaration:
					return ((IndexerDeclarationSyntax)node).ExpressionBody;
				default:
					if (syntaxKind == SyntaxKind.ArrowExpressionClause)
					{
						return (ArrowExpressionClauseSyntax)node;
					}
					break;
				}
				ExceptionUtilities.UnexpectedValue(node.Kind());
				break;
			}
			return arrowExpressionClauseSyntax;
		}
	}
}
