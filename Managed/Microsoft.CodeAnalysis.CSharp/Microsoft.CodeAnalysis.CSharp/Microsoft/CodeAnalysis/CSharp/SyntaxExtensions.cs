using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000233 RID: 563
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public static class SyntaxExtensions
	{
		// Token: 0x06002ED5 RID: 11989 RVA: 0x000F0D4C File Offset: 0x000EEF4C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static ArrowExpressionClauseSyntax GetExpressionBodySyntax(this CSharpSyntaxNode node)
		{
			ArrowExpressionClauseSyntax arrowExpressionClauseSyntax = null;
			SyntaxKind syntaxKind = node.Kind();
			if (syntaxKind <= SyntaxKind.UnknownAccessorDeclaration)
			{
				if (syntaxKind - SyntaxKind.MethodDeclaration > 3)
				{
					switch (syntaxKind)
					{
					case SyntaxKind.DestructorDeclaration:
						break;
					case SyntaxKind.PropertyDeclaration:
						return ((PropertyDeclarationSyntax)node).ExpressionBody;
					case SyntaxKind.EventDeclaration:
					case SyntaxKind.AccessorList:
						goto IL_00A4;
					case SyntaxKind.IndexerDeclaration:
						return ((IndexerDeclarationSyntax)node).ExpressionBody;
					case SyntaxKind.GetAccessorDeclaration:
					case SyntaxKind.SetAccessorDeclaration:
					case SyntaxKind.AddAccessorDeclaration:
					case SyntaxKind.RemoveAccessorDeclaration:
					case SyntaxKind.UnknownAccessorDeclaration:
						goto IL_007A;
					default:
						goto IL_00A4;
					}
				}
				return ((BaseMethodDeclarationSyntax)node).ExpressionBody;
			}
			if (syntaxKind == SyntaxKind.ArrowExpressionClause)
			{
				return (ArrowExpressionClauseSyntax)node;
			}
			if (syntaxKind != SyntaxKind.InitAccessorDeclaration)
			{
				goto IL_00A4;
			}
			IL_007A:
			return ((AccessorDeclarationSyntax)node).ExpressionBody;
			IL_00A4:
			ExceptionUtilities.UnexpectedValue(node.Kind());
			return arrowExpressionClauseSyntax;
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000F0E0F File Offset: 0x000EF00F
		public static SyntaxToken NormalizeWhitespace(this SyntaxToken token, string indentation, bool elasticTrivia)
		{
			return SyntaxNormalizer.Normalize(token, indentation, "\r\n", elasticTrivia);
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x000F0E1E File Offset: 0x000EF01E
		internal static SyntaxToken Identifier(this DeclarationExpressionSyntax self)
		{
			return ((SingleVariableDesignationSyntax)self.Designation).Identifier;
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x000F0E30 File Offset: 0x000EF030
		public static SyntaxToken NormalizeWhitespace(this SyntaxToken token, string indentation = "    ", string eol = "\r\n", bool elasticTrivia = false)
		{
			return SyntaxNormalizer.Normalize(token, indentation, eol, elasticTrivia);
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x000F0E3B File Offset: 0x000EF03B
		public static SyntaxTriviaList NormalizeWhitespace(this SyntaxTriviaList list, string indentation, bool elasticTrivia)
		{
			return SyntaxNormalizer.Normalize(list, indentation, "\r\n", elasticTrivia);
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x000F0E4A File Offset: 0x000EF04A
		public static SyntaxTriviaList NormalizeWhitespace(this SyntaxTriviaList list, string indentation = "    ", string eol = "\r\n", bool elasticTrivia = false)
		{
			return SyntaxNormalizer.Normalize(list, indentation, eol, elasticTrivia);
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x000F0E55 File Offset: 0x000EF055
		public static SyntaxTriviaList ToSyntaxTriviaList(this IEnumerable<SyntaxTrivia> sequence)
		{
			return SyntaxFactory.TriviaList(sequence);
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x000F0E60 File Offset: 0x000EF060
		internal static XmlNameAttributeElementKind GetElementKind(this XmlNameAttributeSyntax attributeSyntax)
		{
			CSharpSyntaxNode parent = attributeSyntax.Parent;
			SyntaxKind syntaxKind = parent.Kind();
			string text;
			if (syntaxKind == SyntaxKind.XmlEmptyElement)
			{
				text = ((XmlEmptyElementSyntax)parent).Name.LocalName.ValueText;
			}
			else
			{
				if (syntaxKind != SyntaxKind.XmlElementStartTag)
				{
					throw ExceptionUtilities.UnexpectedValue(syntaxKind);
				}
				text = ((XmlElementStartTagSyntax)parent).Name.LocalName.ValueText;
			}
			if (DocumentationCommentXmlNames.ElementEquals(text, "param", false))
			{
				return XmlNameAttributeElementKind.Parameter;
			}
			if (DocumentationCommentXmlNames.ElementEquals(text, "paramref", false))
			{
				return XmlNameAttributeElementKind.ParameterReference;
			}
			if (DocumentationCommentXmlNames.ElementEquals(text, "typeparam", false))
			{
				return XmlNameAttributeElementKind.TypeParameter;
			}
			if (DocumentationCommentXmlNames.ElementEquals(text, "typeparamref", false))
			{
				return XmlNameAttributeElementKind.TypeParameterReference;
			}
			throw ExceptionUtilities.UnexpectedValue(text);
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x000F0F13 File Offset: 0x000EF113
		internal static bool ReportDocumentationCommentDiagnostics(this SyntaxTree tree)
		{
			return tree.Options.DocumentationMode >= DocumentationMode.Diagnose;
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x000F0F26 File Offset: 0x000EF126
		public static SimpleNameSyntax WithIdentifier(this SimpleNameSyntax simpleName, SyntaxToken identifier)
		{
			if (simpleName.Kind() != SyntaxKind.IdentifierName)
			{
				return ((GenericNameSyntax)simpleName).WithIdentifier(identifier);
			}
			return ((IdentifierNameSyntax)simpleName).WithIdentifier(identifier);
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x000F0F4E File Offset: 0x000EF14E
		internal static bool IsTypeInContextWhichNeedsDynamicAttribute(this IdentifierNameSyntax typeNode)
		{
			return SyntaxFacts.IsInTypeOnlyContext(typeNode) && SyntaxExtensions.IsInContextWhichNeedsDynamicAttribute(typeNode);
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000F0F60 File Offset: 0x000EF160
		internal static ExpressionSyntax SkipParens(this ExpressionSyntax expression)
		{
			while (expression.Kind() == SyntaxKind.ParenthesizedExpression)
			{
				expression = ((ParenthesizedExpressionSyntax)expression).Expression;
			}
			return expression;
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x000F0F80 File Offset: 0x000EF180
		internal static bool IsDeconstructionLeft(this ExpressionSyntax node)
		{
			SyntaxKind syntaxKind = node.Kind();
			return syntaxKind == SyntaxKind.TupleExpression || (syntaxKind == SyntaxKind.DeclarationExpression && ((DeclarationExpressionSyntax)node).Designation.Kind() == SyntaxKind.ParenthesizedVariableDesignation);
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x000F0FC1 File Offset: 0x000EF1C1
		internal static bool IsDeconstruction(this AssignmentExpressionSyntax self)
		{
			return self.Left.IsDeconstructionLeft();
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x000F0FD0 File Offset: 0x000EF1D0
		private static bool IsInContextWhichNeedsDynamicAttribute(CSharpSyntaxNode node)
		{
			SyntaxKind syntaxKind = node.Kind();
			if (syntaxKind <= SyntaxKind.DelegateDeclaration)
			{
				if (syntaxKind <= SyntaxKind.EqualsValueClause)
				{
					if (syntaxKind != SyntaxKind.Block && syntaxKind - SyntaxKind.VariableDeclarator > 1)
					{
						goto IL_00AC;
					}
					return false;
				}
				else
				{
					if (syntaxKind == SyntaxKind.Attribute)
					{
						return false;
					}
					if (syntaxKind != SyntaxKind.DelegateDeclaration)
					{
						goto IL_00AC;
					}
				}
			}
			else if (syntaxKind <= SyntaxKind.IndexerDeclaration)
			{
				switch (syntaxKind)
				{
				case SyntaxKind.BaseList:
				case SyntaxKind.SimpleBaseType:
				case SyntaxKind.FieldDeclaration:
				case SyntaxKind.EventFieldDeclaration:
				case SyntaxKind.MethodDeclaration:
				case SyntaxKind.OperatorDeclaration:
				case SyntaxKind.ConversionOperatorDeclaration:
					break;
				case SyntaxKind.TypeParameterConstraintClause:
					return false;
				case SyntaxKind.ConstructorConstraint:
				case SyntaxKind.ClassConstraint:
				case SyntaxKind.StructConstraint:
				case SyntaxKind.TypeConstraint:
				case SyntaxKind.ExplicitInterfaceSpecifier:
				case SyntaxKind.EnumMemberDeclaration:
					goto IL_00AC;
				default:
					if (syntaxKind - SyntaxKind.PropertyDeclaration > 2)
					{
						goto IL_00AC;
					}
					break;
				}
			}
			else if (syntaxKind != SyntaxKind.Parameter && syntaxKind != SyntaxKind.PrimaryConstructorBaseType)
			{
				goto IL_00AC;
			}
			return true;
			IL_00AC:
			return node.Parent != null && SyntaxExtensions.IsInContextWhichNeedsDynamicAttribute(node.Parent);
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x000F10A0 File Offset: 0x000EF2A0
		public static IndexerDeclarationSyntax Update(this IndexerDeclarationSyntax syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken thisKeyword, BracketedParameterListSyntax parameterList, AccessorListSyntax accessorList)
		{
			return syntax.Update(attributeLists, modifiers, type, explicitInterfaceSpecifier, thisKeyword, parameterList, accessorList, null, default(SyntaxToken));
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x000F10C8 File Offset: 0x000EF2C8
		public static OperatorDeclarationSyntax Update(this OperatorDeclarationSyntax syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken operatorKeyword, SyntaxToken operatorToken, ParameterListSyntax parameterList, BlockSyntax block, SyntaxToken semicolonToken)
		{
			return syntax.Update(attributeLists, modifiers, returnType, operatorKeyword, operatorToken, parameterList, block, null, semicolonToken);
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x000F10EC File Offset: 0x000EF2EC
		public static MethodDeclarationSyntax Update(this MethodDeclarationSyntax syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BlockSyntax block, SyntaxToken semicolonToken)
		{
			return syntax.Update(attributeLists, modifiers, returnType, explicitInterfaceSpecifier, identifier, typeParameterList, parameterList, constraintClauses, block, null, semicolonToken);
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x000F1114 File Offset: 0x000EF314
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static CSharpSyntaxNode GetContainingDeconstruction(this ExpressionSyntax expr)
		{
			SyntaxKind syntaxKind = expr.Kind();
			if (syntaxKind != SyntaxKind.TupleExpression && syntaxKind != SyntaxKind.DeclarationExpression && syntaxKind != SyntaxKind.IdentifierName)
			{
				return null;
			}
			SyntaxKind syntaxKind2;
			for (;;)
			{
				CSharpSyntaxNode parent = expr.Parent;
				if (parent == null)
				{
					break;
				}
				syntaxKind2 = parent.Kind();
				if (syntaxKind2 != SyntaxKind.Argument)
				{
					goto Block_5;
				}
				CSharpSyntaxNode parent2 = parent.Parent;
				if (parent2 == null || parent2.Kind() != SyntaxKind.TupleExpression)
				{
					goto IL_0078;
				}
				expr = (TupleExpressionSyntax)parent.Parent;
			}
			return null;
			Block_5:
			if (syntaxKind2 != SyntaxKind.SimpleAssignmentExpression)
			{
				if (syntaxKind2 != SyntaxKind.ForEachVariableStatement)
				{
					return null;
				}
				CSharpSyntaxNode parent;
				if (((ForEachVariableStatementSyntax)parent).Variable == expr)
				{
					return parent;
				}
				return null;
			}
			else
			{
				CSharpSyntaxNode parent;
				if (((AssignmentExpressionSyntax)parent).Left == expr)
				{
					return parent;
				}
				return null;
			}
			IL_0078:
			return null;
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x000F11C0 File Offset: 0x000EF3C0
		internal static bool IsOutDeclaration(this DeclarationExpressionSyntax p)
		{
			CSharpSyntaxNode parent = p.Parent;
			return parent != null && parent.Kind() == SyntaxKind.Argument && ((ArgumentSyntax)p.Parent).RefOrOutKeyword.Kind() == SyntaxKind.OutKeyword;
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x000F11FB File Offset: 0x000EF3FB
		internal static bool IsOutVarDeclaration(this DeclarationExpressionSyntax p)
		{
			return p.Designation.Kind() == SyntaxKind.SingleVariableDesignation && p.IsOutDeclaration();
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x000F1218 File Offset: 0x000EF418
		internal static void VisitRankSpecifiers<[global::System.Runtime.CompilerServices.Nullable(2)] TArg>(this TypeSyntax type, Action<ArrayRankSpecifierSyntax, TArg> action, in TArg argument)
		{
			ArrayBuilder<SyntaxNode> instance = ArrayBuilder<SyntaxNode>.GetInstance();
			instance.Push(type);
			while (instance.Count > 0)
			{
				SyntaxNode syntaxNode = instance.Pop<SyntaxNode>();
				ArrayRankSpecifierSyntax arrayRankSpecifierSyntax = syntaxNode as ArrayRankSpecifierSyntax;
				if (arrayRankSpecifierSyntax == null)
				{
					type = (TypeSyntax)syntaxNode;
					SyntaxKind syntaxKind = type.Kind();
					if (syntaxKind <= SyntaxKind.TupleType)
					{
						switch (syntaxKind)
						{
						case SyntaxKind.IdentifierName:
						case SyntaxKind.PredefinedType:
						case SyntaxKind.OmittedTypeArgument:
							continue;
						case SyntaxKind.QualifiedName:
						{
							QualifiedNameSyntax qualifiedNameSyntax = (QualifiedNameSyntax)type;
							instance.Push(qualifiedNameSyntax.Right);
							instance.Push(qualifiedNameSyntax.Left);
							continue;
						}
						case SyntaxKind.GenericName:
						{
							GenericNameSyntax genericNameSyntax = (GenericNameSyntax)type;
							for (int i = genericNameSyntax.TypeArgumentList.Arguments.Count - 1; i >= 0; i--)
							{
								instance.Push(genericNameSyntax.TypeArgumentList.Arguments[i]);
							}
							continue;
						}
						case SyntaxKind.TypeArgumentList:
						case SyntaxKind.ArrayRankSpecifier:
							break;
						case SyntaxKind.AliasQualifiedName:
						{
							AliasQualifiedNameSyntax aliasQualifiedNameSyntax = (AliasQualifiedNameSyntax)type;
							instance.Push(aliasQualifiedNameSyntax.Name);
							continue;
						}
						case SyntaxKind.ArrayType:
						{
							ArrayTypeSyntax arrayTypeSyntax = (ArrayTypeSyntax)type;
							for (int j = arrayTypeSyntax.RankSpecifiers.Count - 1; j >= 0; j--)
							{
								instance.Push(arrayTypeSyntax.RankSpecifiers[j]);
							}
							instance.Push(arrayTypeSyntax.ElementType);
							continue;
						}
						case SyntaxKind.PointerType:
						{
							PointerTypeSyntax pointerTypeSyntax = (PointerTypeSyntax)type;
							instance.Push(pointerTypeSyntax.ElementType);
							continue;
						}
						case SyntaxKind.NullableType:
						{
							NullableTypeSyntax nullableTypeSyntax = (NullableTypeSyntax)type;
							instance.Push(nullableTypeSyntax.ElementType);
							continue;
						}
						default:
							if (syntaxKind == SyntaxKind.TupleType)
							{
								TupleTypeSyntax tupleTypeSyntax = (TupleTypeSyntax)type;
								for (int k = tupleTypeSyntax.Elements.Count - 1; k >= 0; k--)
								{
									instance.Push(tupleTypeSyntax.Elements[k].Type);
								}
								continue;
							}
							break;
						}
					}
					else
					{
						if (syntaxKind == SyntaxKind.RefType)
						{
							RefTypeSyntax refTypeSyntax = (RefTypeSyntax)type;
							instance.Push(refTypeSyntax.Type);
							continue;
						}
						if (syntaxKind == SyntaxKind.FunctionPointerType)
						{
							FunctionPointerTypeSyntax functionPointerTypeSyntax = (FunctionPointerTypeSyntax)type;
							for (int l = functionPointerTypeSyntax.ParameterList.Parameters.Count - 1; l >= 0; l--)
							{
								TypeSyntax type2 = functionPointerTypeSyntax.ParameterList.Parameters[l].Type;
								instance.Push(type2);
							}
							continue;
						}
					}
					throw ExceptionUtilities.UnexpectedValue(type.Kind());
				}
				action.Invoke(arrayRankSpecifierSyntax, argument);
			}
			instance.Free();
		}
	}
}
