using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000218 RID: 536
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public static class SyntaxFacts
	{
		// Token: 0x060026DA RID: 9946 RVA: 0x000D2584 File Offset: 0x000D0784
		internal static bool IsHexDigit(char c)
		{
			return (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f');
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x000D25AB File Offset: 0x000D07AB
		internal static bool IsBinaryDigit(char c)
		{
			return (c == '0') | (c == '1');
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x000D25B8 File Offset: 0x000D07B8
		internal static bool IsDecDigit(char c)
		{
			return c >= '0' && c <= '9';
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x000D25C9 File Offset: 0x000D07C9
		internal static int HexValue(char c)
		{
			if (c < '0' || c > '9')
			{
				return (int)((c & 'ß') - 'A' + '\n');
			}
			return (int)(c - '0');
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x000D25E7 File Offset: 0x000D07E7
		internal static int BinaryValue(char c)
		{
			return (int)(c - '0');
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x000D25ED File Offset: 0x000D07ED
		internal static int DecValue(char c)
		{
			return (int)(c - '0');
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x000D25F4 File Offset: 0x000D07F4
		public static bool IsWhitespace(char ch)
		{
			return ch == ' ' || ch == '\t' || ch == '\v' || ch == '\f' || ch == '\u00a0' || ch == '\ufeff' || ch == '\u001a' || (ch > 'ÿ' && CharUnicodeInfo.GetUnicodeCategory(ch) == 11);
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x000D2640 File Offset: 0x000D0840
		public static bool IsNewLine(char ch)
		{
			return ch == '\r' || ch == '\n' || ch == '\u0085' || ch == '\u2028' || ch == '\u2029';
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x000D2666 File Offset: 0x000D0866
		public static bool IsIdentifierStartCharacter(char ch)
		{
			return UnicodeCharacterUtilities.IsIdentifierStartCharacter(ch);
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x000D266E File Offset: 0x000D086E
		public static bool IsIdentifierPartCharacter(char ch)
		{
			return UnicodeCharacterUtilities.IsIdentifierPartCharacter(ch);
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x000D2676 File Offset: 0x000D0876
		public static bool IsValidIdentifier(string name)
		{
			return UnicodeCharacterUtilities.IsValidIdentifier(name);
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x000D2680 File Offset: 0x000D0880
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static bool ContainsDroppedIdentifierCharacters(string name)
		{
			if (RoslynString.IsNullOrEmpty(name))
			{
				return false;
			}
			if (name.get_Chars(0) == '@')
			{
				return true;
			}
			int length = name.Length;
			for (int i = 0; i < length; i++)
			{
				if (UnicodeCharacterUtilities.IsFormattingChar(name.get_Chars(i)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x000D26C8 File Offset: 0x000D08C8
		internal static bool IsNonAsciiQuotationMark(char ch)
		{
			switch (ch)
			{
			case '‘':
			case '’':
				return true;
			case '“':
			case '”':
				return true;
			}
			return false;
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x000D26F8 File Offset: 0x000D08F8
		public static bool IsAliasQualifier(SyntaxNode node)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.AliasQualifiedNameSyntax aliasQualifiedNameSyntax = node.Parent as Microsoft.CodeAnalysis.CSharp.Syntax.AliasQualifiedNameSyntax;
			return aliasQualifiedNameSyntax != null && aliasQualifiedNameSyntax.Alias == node;
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x000D2720 File Offset: 0x000D0920
		public static bool IsAttributeName(SyntaxNode node)
		{
			SyntaxNode parent = node.Parent;
			if (parent == null || !SyntaxFacts.IsName(node.Kind()))
			{
				return false;
			}
			SyntaxKind syntaxKind = parent.Kind();
			if (syntaxKind == SyntaxKind.QualifiedName)
			{
				return ((Microsoft.CodeAnalysis.CSharp.Syntax.QualifiedNameSyntax)parent).Right == node && SyntaxFacts.IsAttributeName(parent);
			}
			if (syntaxKind != SyntaxKind.AliasQualifiedName)
			{
				Microsoft.CodeAnalysis.CSharp.Syntax.AttributeSyntax attributeSyntax = node.Parent as Microsoft.CodeAnalysis.CSharp.Syntax.AttributeSyntax;
				return attributeSyntax != null && attributeSyntax.Name == node;
			}
			return ((Microsoft.CodeAnalysis.CSharp.Syntax.AliasQualifiedNameSyntax)parent).Name == node && SyntaxFacts.IsAttributeName(parent);
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x000D27A8 File Offset: 0x000D09A8
		public static bool IsInvoked(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax node)
		{
			node = SyntaxFactory.GetStandaloneExpression(node);
			Microsoft.CodeAnalysis.CSharp.Syntax.InvocationExpressionSyntax invocationExpressionSyntax = node.Parent as Microsoft.CodeAnalysis.CSharp.Syntax.InvocationExpressionSyntax;
			return invocationExpressionSyntax != null && invocationExpressionSyntax.Expression == node;
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x000D27D8 File Offset: 0x000D09D8
		public static bool IsIndexed(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax node)
		{
			node = SyntaxFactory.GetStandaloneExpression(node);
			Microsoft.CodeAnalysis.CSharp.Syntax.ElementAccessExpressionSyntax elementAccessExpressionSyntax = node.Parent as Microsoft.CodeAnalysis.CSharp.Syntax.ElementAccessExpressionSyntax;
			return elementAccessExpressionSyntax != null && elementAccessExpressionSyntax.Expression == node;
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x000D2808 File Offset: 0x000D0A08
		public static bool IsNamespaceAliasQualifier(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax node)
		{
			Microsoft.CodeAnalysis.CSharp.Syntax.AliasQualifiedNameSyntax aliasQualifiedNameSyntax = node.Parent as Microsoft.CodeAnalysis.CSharp.Syntax.AliasQualifiedNameSyntax;
			return aliasQualifiedNameSyntax != null && aliasQualifiedNameSyntax.Alias == node;
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x000D2830 File Offset: 0x000D0A30
		public static bool IsInTypeOnlyContext(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax node)
		{
			node = SyntaxFactory.GetStandaloneExpression(node);
			CSharpSyntaxNode parent = node.Parent;
			if (parent != null)
			{
				SyntaxKind syntaxKind = parent.Kind();
				if (syntaxKind > SyntaxKind.LocalFunctionStatement)
				{
					if (syntaxKind <= SyntaxKind.IncompleteMember)
					{
						if (syntaxKind <= SyntaxKind.SimpleBaseType)
						{
							if (syntaxKind == SyntaxKind.Attribute)
							{
								return ((Microsoft.CodeAnalysis.CSharp.Syntax.AttributeSyntax)parent).Name == node;
							}
							if (syntaxKind == SyntaxKind.DelegateDeclaration)
							{
								return ((Microsoft.CodeAnalysis.CSharp.Syntax.DelegateDeclarationSyntax)parent).ReturnType == node;
							}
							if (syntaxKind != SyntaxKind.SimpleBaseType)
							{
								return false;
							}
							return true;
						}
						else if (syntaxKind <= SyntaxKind.IndexerDeclaration)
						{
							switch (syntaxKind)
							{
							case SyntaxKind.TypeConstraint:
								return ((Microsoft.CodeAnalysis.CSharp.Syntax.TypeConstraintSyntax)parent).Type == node;
							case SyntaxKind.ExplicitInterfaceSpecifier:
								return ((Microsoft.CodeAnalysis.CSharp.Syntax.ExplicitInterfaceSpecifierSyntax)parent).Name == node;
							case SyntaxKind.EnumMemberDeclaration:
							case SyntaxKind.FieldDeclaration:
							case SyntaxKind.EventFieldDeclaration:
								return false;
							case SyntaxKind.MethodDeclaration:
								return ((Microsoft.CodeAnalysis.CSharp.Syntax.MethodDeclarationSyntax)parent).ReturnType == node;
							case SyntaxKind.OperatorDeclaration:
								return ((Microsoft.CodeAnalysis.CSharp.Syntax.OperatorDeclarationSyntax)parent).ReturnType == node;
							case SyntaxKind.ConversionOperatorDeclaration:
								return ((Microsoft.CodeAnalysis.CSharp.Syntax.ConversionOperatorDeclarationSyntax)parent).Type == node;
							default:
								switch (syntaxKind)
								{
								case SyntaxKind.PropertyDeclaration:
									return ((Microsoft.CodeAnalysis.CSharp.Syntax.PropertyDeclarationSyntax)parent).Type == node;
								case SyntaxKind.EventDeclaration:
									return ((Microsoft.CodeAnalysis.CSharp.Syntax.EventDeclarationSyntax)parent).Type == node;
								case SyntaxKind.IndexerDeclaration:
									return ((Microsoft.CodeAnalysis.CSharp.Syntax.IndexerDeclarationSyntax)parent).Type == node;
								default:
									return false;
								}
								break;
							}
						}
						else if (syntaxKind != SyntaxKind.Parameter)
						{
							if (syntaxKind != SyntaxKind.IncompleteMember)
							{
								return false;
							}
							return ((Microsoft.CodeAnalysis.CSharp.Syntax.IncompleteMemberSyntax)parent).Type == node;
						}
					}
					else if (syntaxKind <= SyntaxKind.DeclarationExpression)
					{
						if (syntaxKind <= SyntaxKind.DeclarationPattern)
						{
							if (syntaxKind == SyntaxKind.TupleElement)
							{
								return ((Microsoft.CodeAnalysis.CSharp.Syntax.TupleElementSyntax)parent).Type == node;
							}
							if (syntaxKind != SyntaxKind.DeclarationPattern)
							{
								return false;
							}
							return ((Microsoft.CodeAnalysis.CSharp.Syntax.DeclarationPatternSyntax)parent).Type == node;
						}
						else
						{
							if (syntaxKind == SyntaxKind.RecursivePattern)
							{
								return ((Microsoft.CodeAnalysis.CSharp.Syntax.RecursivePatternSyntax)parent).Type == node;
							}
							if (syntaxKind != SyntaxKind.DeclarationExpression)
							{
								return false;
							}
							return ((Microsoft.CodeAnalysis.CSharp.Syntax.DeclarationExpressionSyntax)parent).Type == node;
						}
					}
					else if (syntaxKind <= SyntaxKind.FunctionPointerType)
					{
						if (syntaxKind == SyntaxKind.RefType)
						{
							return ((Microsoft.CodeAnalysis.CSharp.Syntax.RefTypeSyntax)parent).Type == node;
						}
						if (syntaxKind != SyntaxKind.FunctionPointerType)
						{
							return false;
						}
						throw ExceptionUtilities.Unreachable;
					}
					else if (syntaxKind != SyntaxKind.FunctionPointerParameter)
					{
						if (syntaxKind != SyntaxKind.PrimaryConstructorBaseType)
						{
							return false;
						}
						return ((Microsoft.CodeAnalysis.CSharp.Syntax.PrimaryConstructorBaseTypeSyntax)parent).Type == node;
					}
					return ((Microsoft.CodeAnalysis.CSharp.Syntax.BaseParameterSyntax)parent).Type == node;
				}
				if (syntaxKind <= SyntaxKind.AsExpression)
				{
					if (syntaxKind <= SyntaxKind.NullableType)
					{
						if (syntaxKind == SyntaxKind.ConversionOperatorMemberCref)
						{
							return ((Microsoft.CodeAnalysis.CSharp.Syntax.ConversionOperatorMemberCrefSyntax)parent).Type == node;
						}
						if (syntaxKind == SyntaxKind.CrefParameter)
						{
							return true;
						}
						switch (syntaxKind)
						{
						case SyntaxKind.TypeArgumentList:
							return true;
						case SyntaxKind.PredefinedType:
							return true;
						case SyntaxKind.ArrayType:
							return ((Microsoft.CodeAnalysis.CSharp.Syntax.ArrayTypeSyntax)parent).ElementType == node;
						case SyntaxKind.PointerType:
							return ((Microsoft.CodeAnalysis.CSharp.Syntax.PointerTypeSyntax)parent).ElementType == node;
						case SyntaxKind.NullableType:
							return ((Microsoft.CodeAnalysis.CSharp.Syntax.NullableTypeSyntax)parent).ElementType == node;
						}
					}
					else if (syntaxKind <= SyntaxKind.ObjectCreationExpression)
					{
						if (syntaxKind == SyntaxKind.CastExpression)
						{
							return ((Microsoft.CodeAnalysis.CSharp.Syntax.CastExpressionSyntax)parent).Type == node;
						}
						if (syntaxKind == SyntaxKind.ObjectCreationExpression)
						{
							return ((Microsoft.CodeAnalysis.CSharp.Syntax.ObjectCreationExpressionSyntax)parent).Type == node;
						}
					}
					else
					{
						if (syntaxKind == SyntaxKind.StackAllocArrayCreationExpression)
						{
							return ((Microsoft.CodeAnalysis.CSharp.Syntax.StackAllocArrayCreationExpressionSyntax)parent).Type == node;
						}
						if (syntaxKind - SyntaxKind.IsExpression <= 1)
						{
							return ((Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax)parent).Right == node;
						}
					}
				}
				else if (syntaxKind <= SyntaxKind.JoinClause)
				{
					switch (syntaxKind)
					{
					case SyntaxKind.TypeOfExpression:
						return ((Microsoft.CodeAnalysis.CSharp.Syntax.TypeOfExpressionSyntax)parent).Type == node;
					case SyntaxKind.SizeOfExpression:
						return ((Microsoft.CodeAnalysis.CSharp.Syntax.SizeOfExpressionSyntax)parent).Type == node;
					case SyntaxKind.CheckedExpression:
					case SyntaxKind.UncheckedExpression:
					case SyntaxKind.MakeRefExpression:
						break;
					case SyntaxKind.DefaultExpression:
						return ((Microsoft.CodeAnalysis.CSharp.Syntax.DefaultExpressionSyntax)parent).Type == node;
					case SyntaxKind.RefValueExpression:
						return ((Microsoft.CodeAnalysis.CSharp.Syntax.RefValueExpressionSyntax)parent).Type == node;
					default:
						if (syntaxKind == SyntaxKind.FromClause)
						{
							return ((Microsoft.CodeAnalysis.CSharp.Syntax.FromClauseSyntax)parent).Type == node;
						}
						if (syntaxKind == SyntaxKind.JoinClause)
						{
							return ((Microsoft.CodeAnalysis.CSharp.Syntax.JoinClauseSyntax)parent).Type == node;
						}
						break;
					}
				}
				else if (syntaxKind <= SyntaxKind.ForEachStatement)
				{
					if (syntaxKind == SyntaxKind.VariableDeclaration)
					{
						return ((Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclarationSyntax)parent).Type == node;
					}
					if (syntaxKind == SyntaxKind.ForEachStatement)
					{
						return ((Microsoft.CodeAnalysis.CSharp.Syntax.ForEachStatementSyntax)parent).Type == node;
					}
				}
				else
				{
					if (syntaxKind == SyntaxKind.CatchDeclaration)
					{
						return ((Microsoft.CodeAnalysis.CSharp.Syntax.CatchDeclarationSyntax)parent).Type == node;
					}
					if (syntaxKind == SyntaxKind.LocalFunctionStatement)
					{
						return ((Microsoft.CodeAnalysis.CSharp.Syntax.LocalFunctionStatementSyntax)parent).ReturnType == node;
					}
				}
			}
			return false;
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x000D2CD4 File Offset: 0x000D0ED4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool IsInNamespaceOrTypeContext(Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax node)
		{
			if (node != null)
			{
				node = SyntaxFactory.GetStandaloneExpression(node);
				CSharpSyntaxNode parent = node.Parent;
				if (parent != null)
				{
					SyntaxKind syntaxKind = parent.Kind();
					if (syntaxKind == SyntaxKind.QualifiedName)
					{
						return ((Microsoft.CodeAnalysis.CSharp.Syntax.QualifiedNameSyntax)parent).Left == node;
					}
					if (syntaxKind == SyntaxKind.UsingDirective)
					{
						return ((Microsoft.CodeAnalysis.CSharp.Syntax.UsingDirectiveSyntax)parent).Name == node;
					}
					return SyntaxFacts.IsInTypeOnlyContext(node);
				}
			}
			return false;
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x000D2D34 File Offset: 0x000D0F34
		public static bool IsNamedArgumentName(SyntaxNode node)
		{
			if (!node.IsKind(SyntaxKind.IdentifierName))
			{
				return false;
			}
			SyntaxNode parent = node.Parent;
			if (parent == null || !parent.IsKind(SyntaxKind.NameColon))
			{
				return false;
			}
			SyntaxNode parent2 = parent.Parent;
			if (parent2.IsKind(SyntaxKind.Subpattern))
			{
				return true;
			}
			if (parent2 == null || (!parent2.IsKind(SyntaxKind.Argument) && !parent2.IsKind(SyntaxKind.AttributeArgument)))
			{
				return false;
			}
			SyntaxNode parent3 = parent2.Parent;
			if (parent3 == null)
			{
				return false;
			}
			if (parent3.IsKind(SyntaxKind.TupleExpression))
			{
				return true;
			}
			if (!(parent3 is Microsoft.CodeAnalysis.CSharp.Syntax.BaseArgumentListSyntax) && !parent3.IsKind(SyntaxKind.AttributeArgumentList))
			{
				return false;
			}
			SyntaxNode parent4 = parent3.Parent;
			if (parent4 == null)
			{
				return false;
			}
			SyntaxKind syntaxKind = parent4.Kind();
			if (syntaxKind <= SyntaxKind.ImplicitObjectCreationExpression)
			{
				if (syntaxKind <= SyntaxKind.ObjectInitializerExpression)
				{
					if (syntaxKind - SyntaxKind.InvocationExpression > 1 && syntaxKind != SyntaxKind.ObjectInitializerExpression)
					{
						return false;
					}
				}
				else if (syntaxKind != SyntaxKind.ObjectCreationExpression && syntaxKind != SyntaxKind.ImplicitObjectCreationExpression)
				{
					return false;
				}
			}
			else if (syntaxKind <= SyntaxKind.ThisConstructorInitializer)
			{
				if (syntaxKind != SyntaxKind.Attribute && syntaxKind - SyntaxKind.BaseConstructorInitializer > 1)
				{
					return false;
				}
			}
			else if (syntaxKind != SyntaxKind.TupleExpression && syntaxKind != SyntaxKind.PrimaryConstructorBaseType)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x000D2E54 File Offset: 0x000D1054
		public static bool IsFixedStatementExpression(SyntaxNode node)
		{
			SyntaxNode syntaxNode = node.Parent;
			while (syntaxNode != null && (syntaxNode.IsKind(SyntaxKind.ParenthesizedExpression) || syntaxNode.IsKind(SyntaxKind.CastExpression)))
			{
				syntaxNode = syntaxNode.Parent;
			}
			if (syntaxNode == null || !syntaxNode.IsKind(SyntaxKind.EqualsValueClause))
			{
				return false;
			}
			syntaxNode = syntaxNode.Parent;
			if (syntaxNode == null || !syntaxNode.IsKind(SyntaxKind.VariableDeclarator))
			{
				return false;
			}
			syntaxNode = syntaxNode.Parent;
			if (syntaxNode == null || !syntaxNode.IsKind(SyntaxKind.VariableDeclaration))
			{
				return false;
			}
			syntaxNode = syntaxNode.Parent;
			return syntaxNode != null && syntaxNode.IsKind(SyntaxKind.FixedStatement);
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x000D2EEC File Offset: 0x000D10EC
		public static string GetText(Accessibility accessibility)
		{
			switch (accessibility)
			{
			case Accessibility.NotApplicable:
				return string.Empty;
			case Accessibility.Private:
				return SyntaxFacts.GetText(SyntaxKind.PrivateKeyword);
			case Accessibility.ProtectedAndInternal:
				return SyntaxFacts.GetText(SyntaxKind.PrivateKeyword) + " " + SyntaxFacts.GetText(SyntaxKind.ProtectedKeyword);
			case Accessibility.Protected:
				return SyntaxFacts.GetText(SyntaxKind.ProtectedKeyword);
			case Accessibility.Internal:
				return SyntaxFacts.GetText(SyntaxKind.InternalKeyword);
			case Accessibility.ProtectedOrInternal:
				return SyntaxFacts.GetText(SyntaxKind.ProtectedKeyword) + " " + SyntaxFacts.GetText(SyntaxKind.InternalKeyword);
			case Accessibility.Public:
				return SyntaxFacts.GetText(SyntaxKind.PublicKeyword);
			default:
				throw ExceptionUtilities.UnexpectedValue(accessibility);
			}
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x000D2F98 File Offset: 0x000D1198
		internal static bool IsStatementExpression(SyntaxNode syntax)
		{
			SyntaxKind syntaxKind = syntax.Kind();
			if (syntaxKind <= SyntaxKind.InvocationExpression)
			{
				if (syntaxKind == SyntaxKind.IdentifierName)
				{
					return syntax.IsMissing;
				}
				if (syntaxKind != SyntaxKind.InvocationExpression)
				{
					return false;
				}
			}
			else if (syntaxKind != SyntaxKind.ObjectCreationExpression)
			{
				if (syntaxKind == SyntaxKind.ConditionalAccessExpression)
				{
					return SyntaxFacts.IsStatementExpression(((Microsoft.CodeAnalysis.CSharp.Syntax.ConditionalAccessExpressionSyntax)syntax).WhenNotNull);
				}
				switch (syntaxKind)
				{
				case SyntaxKind.SimpleAssignmentExpression:
				case SyntaxKind.AddAssignmentExpression:
				case SyntaxKind.SubtractAssignmentExpression:
				case SyntaxKind.MultiplyAssignmentExpression:
				case SyntaxKind.DivideAssignmentExpression:
				case SyntaxKind.ModuloAssignmentExpression:
				case SyntaxKind.AndAssignmentExpression:
				case SyntaxKind.ExclusiveOrAssignmentExpression:
				case SyntaxKind.OrAssignmentExpression:
				case SyntaxKind.LeftShiftAssignmentExpression:
				case SyntaxKind.RightShiftAssignmentExpression:
				case SyntaxKind.CoalesceAssignmentExpression:
				case SyntaxKind.PreIncrementExpression:
				case SyntaxKind.PreDecrementExpression:
				case SyntaxKind.PostIncrementExpression:
				case SyntaxKind.PostDecrementExpression:
				case SyntaxKind.AwaitExpression:
					break;
				case (SyntaxKind)8726:
				case (SyntaxKind)8727:
				case (SyntaxKind)8728:
				case (SyntaxKind)8729:
				case SyntaxKind.UnaryPlusExpression:
				case SyntaxKind.UnaryMinusExpression:
				case SyntaxKind.BitwiseNotExpression:
				case SyntaxKind.LogicalNotExpression:
				case SyntaxKind.PointerIndirectionExpression:
				case SyntaxKind.AddressOfExpression:
					return false;
				default:
					return false;
				}
			}
			return true;
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x000D3077 File Offset: 0x000D1277
		[Obsolete("IsLambdaBody API is obsolete", true)]
		public static bool IsLambdaBody(SyntaxNode node)
		{
			return LambdaUtilities.IsLambdaBody(node, false);
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x000D3080 File Offset: 0x000D1280
		internal static bool IsIdentifierVar(this SyntaxToken node)
		{
			return node.ContextualKind == SyntaxKind.VarKeyword;
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x000D308F File Offset: 0x000D128F
		internal static bool IsIdentifierVarOrPredefinedType(this SyntaxToken node)
		{
			return node.IsIdentifierVar() || SyntaxFacts.IsPredefinedType(node.Kind);
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x000D30A6 File Offset: 0x000D12A6
		internal static bool IsDeclarationExpressionType(SyntaxNode node, [global::System.Runtime.CompilerServices.Nullable(2)] [NotNullWhen(true)] out Microsoft.CodeAnalysis.CSharp.Syntax.DeclarationExpressionSyntax parent)
		{
			parent = node.Parent as Microsoft.CodeAnalysis.CSharp.Syntax.DeclarationExpressionSyntax;
			Microsoft.CodeAnalysis.CSharp.Syntax.DeclarationExpressionSyntax declarationExpressionSyntax = parent;
			return node == ((declarationExpressionSyntax != null) ? declarationExpressionSyntax.Type : null);
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x000D30C8 File Offset: 0x000D12C8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public static string TryGetInferredMemberName(this SyntaxNode syntax)
		{
			SyntaxKind syntaxKind = syntax.Kind();
			SyntaxToken syntaxToken;
			if (syntaxKind <= SyntaxKind.ParenthesizedVariableDesignation)
			{
				if (syntaxKind == SyntaxKind.SingleVariableDesignation)
				{
					syntaxToken = ((Microsoft.CodeAnalysis.CSharp.Syntax.SingleVariableDesignationSyntax)syntax).Identifier;
					goto IL_0096;
				}
				if (syntaxKind != SyntaxKind.ParenthesizedVariableDesignation)
				{
					goto IL_0081;
				}
			}
			else if (syntaxKind != SyntaxKind.DiscardDesignation)
			{
				if (syntaxKind != SyntaxKind.DeclarationExpression)
				{
					goto IL_0081;
				}
				Microsoft.CodeAnalysis.CSharp.Syntax.DeclarationExpressionSyntax declarationExpressionSyntax = (Microsoft.CodeAnalysis.CSharp.Syntax.DeclarationExpressionSyntax)syntax;
				SyntaxKind syntaxKind2 = declarationExpressionSyntax.Designation.Kind();
				if (syntaxKind2 == SyntaxKind.ParenthesizedVariableDesignation || syntaxKind2 == SyntaxKind.DiscardDesignation)
				{
					return null;
				}
				syntaxToken = ((Microsoft.CodeAnalysis.CSharp.Syntax.SingleVariableDesignationSyntax)declarationExpressionSyntax.Designation).Identifier;
				goto IL_0096;
			}
			return null;
			IL_0081:
			Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expressionSyntax = syntax as Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax;
			if (expressionSyntax == null)
			{
				return null;
			}
			syntaxToken = expressionSyntax.ExtractAnonymousTypeMemberName();
			IL_0096:
			if (syntaxToken.RawKind == 0)
			{
				return null;
			}
			return syntaxToken.ValueText;
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x000D317D File Offset: 0x000D137D
		public static bool IsReservedTupleElementName(string elementName)
		{
			return NamedTypeSymbol.IsTupleElementNameReserved(elementName) != -1;
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x000D318B File Offset: 0x000D138B
		internal static bool HasAnyBody(this Microsoft.CodeAnalysis.CSharp.Syntax.BaseMethodDeclarationSyntax declaration)
		{
			return (declaration.Body ?? declaration.ExpressionBody) != null;
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x000D31A0 File Offset: 0x000D13A0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static bool IsTopLevelStatement([NotNullWhen(true)] Microsoft.CodeAnalysis.CSharp.Syntax.GlobalStatementSyntax syntax)
		{
			if (syntax == null)
			{
				return false;
			}
			CSharpSyntaxNode parent = syntax.Parent;
			bool? flag = ((parent != null) ? new bool?(parent.IsKind(SyntaxKind.CompilationUnit)) : default(bool?));
			bool flag2 = true;
			return (flag.GetValueOrDefault() == flag2) & (flag != null);
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x000D31EB File Offset: 0x000D13EB
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static bool IsSimpleProgramTopLevelStatement(Microsoft.CodeAnalysis.CSharp.Syntax.GlobalStatementSyntax syntax)
		{
			return SyntaxFacts.IsTopLevelStatement(syntax) && syntax.SyntaxTree.Options.Kind == SourceCodeKind.Regular;
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x000D320C File Offset: 0x000D140C
		internal static bool HasAwaitOperations(SyntaxNode node)
		{
			return Enumerable.Any<SyntaxNode>(node.DescendantNodesAndSelf((SyntaxNode child) => !SyntaxFacts.IsNestedFunction(child), false), delegate(SyntaxNode node)
			{
				if (!(node is Microsoft.CodeAnalysis.CSharp.Syntax.AwaitExpressionSyntax))
				{
					Microsoft.CodeAnalysis.CSharp.Syntax.LocalDeclarationStatementSyntax localDeclarationStatementSyntax = node as Microsoft.CodeAnalysis.CSharp.Syntax.LocalDeclarationStatementSyntax;
					if (localDeclarationStatementSyntax == null)
					{
						Microsoft.CodeAnalysis.CSharp.Syntax.CommonForEachStatementSyntax commonForEachStatementSyntax = node as Microsoft.CodeAnalysis.CSharp.Syntax.CommonForEachStatementSyntax;
						if (commonForEachStatementSyntax == null)
						{
							Microsoft.CodeAnalysis.CSharp.Syntax.UsingStatementSyntax usingStatementSyntax = node as Microsoft.CodeAnalysis.CSharp.Syntax.UsingStatementSyntax;
							if (usingStatementSyntax != null)
							{
								if (usingStatementSyntax.AwaitKeyword.IsKind(SyntaxKind.AwaitKeyword))
								{
									return true;
								}
							}
						}
						else if (commonForEachStatementSyntax.AwaitKeyword.IsKind(SyntaxKind.AwaitKeyword))
						{
							return true;
						}
					}
					else if (localDeclarationStatementSyntax.AwaitKeyword.IsKind(SyntaxKind.AwaitKeyword))
					{
						return true;
					}
					return false;
				}
				return true;
			});
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x000D3264 File Offset: 0x000D1464
		internal static bool IsNestedFunction(SyntaxNode child)
		{
			SyntaxKind syntaxKind = child.Kind();
			return syntaxKind - SyntaxKind.AnonymousMethodExpression <= 2 || syntaxKind == SyntaxKind.LocalFunctionStatement;
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x000D3290 File Offset: 0x000D1490
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static bool HasYieldOperations(SyntaxNode node)
		{
			if (node != null)
			{
				return Enumerable.Any<SyntaxNode>(node.DescendantNodesAndSelf((SyntaxNode child) => !SyntaxFacts.IsNestedFunction(child) && !(node is Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax), false), (SyntaxNode n) => n is Microsoft.CodeAnalysis.CSharp.Syntax.YieldStatementSyntax);
			}
			return false;
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x000D32F0 File Offset: 0x000D14F0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static bool HasReturnWithExpression(SyntaxNode node)
		{
			if (node != null)
			{
				return Enumerable.Any<SyntaxNode>(node.DescendantNodesAndSelf((SyntaxNode child) => !SyntaxFacts.IsNestedFunction(child) && !(node is Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax), false), delegate(SyntaxNode n)
				{
					Microsoft.CodeAnalysis.CSharp.Syntax.ReturnStatementSyntax returnStatementSyntax = n as Microsoft.CodeAnalysis.CSharp.Syntax.ReturnStatementSyntax;
					return returnStatementSyntax != null && returnStatementSyntax.Expression != null;
				});
			}
			return false;
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060026FF RID: 9983 RVA: 0x000D3350 File Offset: 0x000D1550
		public static IEqualityComparer<SyntaxKind> EqualityComparer { get; } = new SyntaxFacts.SyntaxKindEqualityComparer();

		// Token: 0x06002700 RID: 9984 RVA: 0x000D3357 File Offset: 0x000D1557
		public static bool IsKeywordKind(SyntaxKind kind)
		{
			return SyntaxFacts.IsReservedKeyword(kind) || SyntaxFacts.IsContextualKeyword(kind);
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x000D3369 File Offset: 0x000D1569
		public static IEnumerable<SyntaxKind> GetReservedKeywordKinds()
		{
			int num;
			for (int i = 8304; i <= 8384; i = num + 1)
			{
				yield return (SyntaxKind)i;
				num = i;
			}
			yield break;
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x000D3372 File Offset: 0x000D1572
		public static IEnumerable<SyntaxKind> GetKeywordKinds()
		{
			foreach (SyntaxKind syntaxKind in SyntaxFacts.GetReservedKeywordKinds())
			{
				yield return syntaxKind;
			}
			IEnumerator<SyntaxKind> enumerator = null;
			foreach (SyntaxKind syntaxKind2 in SyntaxFacts.GetContextualKeywordKinds())
			{
				yield return syntaxKind2;
			}
			enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x000D337B File Offset: 0x000D157B
		public static bool IsReservedKeyword(SyntaxKind kind)
		{
			return kind >= SyntaxKind.BoolKeyword && kind <= SyntaxKind.ImplicitKeyword;
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x000D3392 File Offset: 0x000D1592
		public static bool IsAttributeTargetSpecifier(SyntaxKind kind)
		{
			return kind - SyntaxKind.AssemblyKeyword <= 1;
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x000D33A1 File Offset: 0x000D15A1
		public static bool IsAccessibilityModifier(SyntaxKind kind)
		{
			return kind - SyntaxKind.PublicKeyword <= 3;
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x000D33B0 File Offset: 0x000D15B0
		public static bool IsPreprocessorKeyword(SyntaxKind kind)
		{
			if (kind <= SyntaxKind.DefaultKeyword)
			{
				if (kind - SyntaxKind.TrueKeyword > 3 && kind != SyntaxKind.DefaultKeyword)
				{
					return false;
				}
			}
			else if (kind - SyntaxKind.ElifKeyword > 14 && kind - SyntaxKind.LoadKeyword > 4)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x000D33E6 File Offset: 0x000D15E6
		internal static bool IsPreprocessorContextualKeyword(SyntaxKind kind)
		{
			if (kind <= SyntaxKind.DefaultKeyword)
			{
				if (kind - SyntaxKind.TrueKeyword > 1 && kind != SyntaxKind.DefaultKeyword)
				{
					goto IL_0032;
				}
			}
			else if (kind - SyntaxKind.HiddenKeyword > 3 && kind - SyntaxKind.EnableKeyword > 2)
			{
				goto IL_0032;
			}
			return false;
			IL_0032:
			return SyntaxFacts.IsPreprocessorKeyword(kind);
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x000D3420 File Offset: 0x000D1620
		public static IEnumerable<SyntaxKind> GetPreprocessorKeywordKinds()
		{
			yield return SyntaxKind.TrueKeyword;
			yield return SyntaxKind.FalseKeyword;
			yield return SyntaxKind.DefaultKeyword;
			yield return SyntaxKind.HiddenKeyword;
			int num;
			for (int i = 8467; i <= 8480; i = num + 1)
			{
				yield return (SyntaxKind)i;
				num = i;
			}
			yield break;
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x000D3429 File Offset: 0x000D1629
		public static bool IsPunctuation(SyntaxKind kind)
		{
			return kind >= SyntaxKind.TildeToken && kind <= SyntaxKind.QuestionQuestionEqualsToken;
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x000D3440 File Offset: 0x000D1640
		public static bool IsLanguagePunctuation(SyntaxKind kind)
		{
			return SyntaxFacts.IsPunctuation(kind) && !SyntaxFacts.IsPreprocessorKeyword(kind) && !SyntaxFacts.IsDebuggerSpecialPunctuation(kind);
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x000D345D File Offset: 0x000D165D
		public static bool IsPreprocessorPunctuation(SyntaxKind kind)
		{
			return kind == SyntaxKind.HashToken;
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x000D3467 File Offset: 0x000D1667
		private static bool IsDebuggerSpecialPunctuation(SyntaxKind kind)
		{
			return kind == SyntaxKind.DollarToken;
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x000D3471 File Offset: 0x000D1671
		public static IEnumerable<SyntaxKind> GetPunctuationKinds()
		{
			int num;
			for (int i = 8193; i <= 8283; i = num + 1)
			{
				yield return (SyntaxKind)i;
				num = i;
			}
			yield break;
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x000D347A File Offset: 0x000D167A
		public static bool IsPunctuationOrKeyword(SyntaxKind kind)
		{
			return kind >= SyntaxKind.TildeToken && kind <= SyntaxKind.EndOfFileToken;
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x000D3491 File Offset: 0x000D1691
		internal static bool IsLiteral(SyntaxKind kind)
		{
			return kind - SyntaxKind.IdentifierToken <= 6;
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x000D34A0 File Offset: 0x000D16A0
		public static bool IsAnyToken(SyntaxKind kind)
		{
			if (kind >= SyntaxKind.TildeToken && kind < SyntaxKind.EndOfLineTrivia)
			{
				return true;
			}
			if (kind <= SyntaxKind.UnderscoreToken)
			{
				if (kind - SyntaxKind.InterpolatedStringStartToken > 5 && kind != SyntaxKind.UnderscoreToken)
				{
					return false;
				}
			}
			else if (kind != SyntaxKind.InterpolatedStringToken && kind != SyntaxKind.InterpolatedStringTextToken)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x000D34EE File Offset: 0x000D16EE
		public static bool IsTrivia(SyntaxKind kind)
		{
			return kind - SyntaxKind.EndOfLineTrivia <= 7 || kind == SyntaxKind.ConflictMarkerTrivia || SyntaxFacts.IsPreprocessorDirective(kind);
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x000D350A File Offset: 0x000D170A
		public static bool IsPreprocessorDirective(SyntaxKind kind)
		{
			return kind - SyntaxKind.IfDirectiveTrivia <= 14 || kind - SyntaxKind.ShebangDirectiveTrivia <= 1 || kind == SyntaxKind.NullableDirectiveTrivia;
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x000D352C File Offset: 0x000D172C
		public static bool IsName(SyntaxKind kind)
		{
			return kind - SyntaxKind.IdentifierName <= 2 || kind == SyntaxKind.AliasQualifiedName;
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x000D3543 File Offset: 0x000D1743
		public static bool IsPredefinedType(SyntaxKind kind)
		{
			return kind - SyntaxKind.BoolKeyword <= 15;
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x000D3553 File Offset: 0x000D1753
		public static bool IsTypeSyntax(SyntaxKind kind)
		{
			if (kind <= SyntaxKind.NullableType)
			{
				if (kind - SyntaxKind.PredefinedType > 1 && kind - SyntaxKind.PointerType > 1)
				{
					goto IL_0030;
				}
			}
			else if (kind != SyntaxKind.TupleType && kind != SyntaxKind.FunctionPointerType)
			{
				goto IL_0030;
			}
			return true;
			IL_0030:
			return SyntaxFacts.IsName(kind);
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x000D358B File Offset: 0x000D178B
		public static bool IsGlobalMemberDeclaration(SyntaxKind kind)
		{
			return kind == SyntaxKind.GlobalStatement || kind - SyntaxKind.FieldDeclaration <= 2 || kind - SyntaxKind.PropertyDeclaration <= 1;
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x000D35AC File Offset: 0x000D17AC
		public static bool IsTypeDeclaration(SyntaxKind kind)
		{
			return kind - SyntaxKind.ClassDeclaration <= 4 || kind == SyntaxKind.RecordDeclaration || kind == SyntaxKind.RecordStructDeclaration;
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x000D35CB File Offset: 0x000D17CB
		public static bool IsNamespaceMemberDeclaration(SyntaxKind kind)
		{
			return SyntaxFacts.IsTypeDeclaration(kind) || kind == SyntaxKind.NamespaceDeclaration;
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x000D35DF File Offset: 0x000D17DF
		public static bool IsAnyUnaryExpression(SyntaxKind token)
		{
			return SyntaxFacts.IsPrefixUnaryExpression(token) || SyntaxFacts.IsPostfixUnaryExpression(token);
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x000D35F1 File Offset: 0x000D17F1
		public static bool IsPrefixUnaryExpression(SyntaxKind token)
		{
			return SyntaxFacts.GetPrefixUnaryExpression(token) > SyntaxKind.None;
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x000D35FC File Offset: 0x000D17FC
		public static bool IsPrefixUnaryExpressionOperatorToken(SyntaxKind token)
		{
			return SyntaxFacts.GetPrefixUnaryExpression(token) > SyntaxKind.None;
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x000D3608 File Offset: 0x000D1808
		public static SyntaxKind GetPrefixUnaryExpression(SyntaxKind token)
		{
			switch (token)
			{
			case SyntaxKind.TildeToken:
				return SyntaxKind.BitwiseNotExpression;
			case SyntaxKind.ExclamationToken:
				return SyntaxKind.LogicalNotExpression;
			case SyntaxKind.DollarToken:
			case SyntaxKind.PercentToken:
			case SyntaxKind.OpenParenToken:
			case SyntaxKind.CloseParenToken:
				break;
			case SyntaxKind.CaretToken:
				return SyntaxKind.IndexExpression;
			case SyntaxKind.AmpersandToken:
				return SyntaxKind.AddressOfExpression;
			case SyntaxKind.AsteriskToken:
				return SyntaxKind.PointerIndirectionExpression;
			case SyntaxKind.MinusToken:
				return SyntaxKind.UnaryMinusExpression;
			case SyntaxKind.PlusToken:
				return SyntaxKind.UnaryPlusExpression;
			default:
				if (token == SyntaxKind.MinusMinusToken)
				{
					return SyntaxKind.PreDecrementExpression;
				}
				if (token == SyntaxKind.PlusPlusToken)
				{
					return SyntaxKind.PreIncrementExpression;
				}
				break;
			}
			return SyntaxKind.None;
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x000D3696 File Offset: 0x000D1896
		public static bool IsPostfixUnaryExpression(SyntaxKind token)
		{
			return SyntaxFacts.GetPostfixUnaryExpression(token) > SyntaxKind.None;
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x000D36A1 File Offset: 0x000D18A1
		public static bool IsPostfixUnaryExpressionToken(SyntaxKind token)
		{
			return SyntaxFacts.GetPostfixUnaryExpression(token) > SyntaxKind.None;
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x000D36AC File Offset: 0x000D18AC
		public static SyntaxKind GetPostfixUnaryExpression(SyntaxKind token)
		{
			if (token == SyntaxKind.ExclamationToken)
			{
				return SyntaxKind.SuppressNullableWarningExpression;
			}
			if (token == SyntaxKind.MinusMinusToken)
			{
				return SyntaxKind.PostDecrementExpression;
			}
			if (token == SyntaxKind.PlusPlusToken)
			{
				return SyntaxKind.PostIncrementExpression;
			}
			return SyntaxKind.None;
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x000D36D9 File Offset: 0x000D18D9
		internal static bool IsIncrementOrDecrementOperator(SyntaxKind token)
		{
			return token - SyntaxKind.MinusMinusToken <= 1;
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x000D36E8 File Offset: 0x000D18E8
		public static bool IsUnaryOperatorDeclarationToken(SyntaxKind token)
		{
			return SyntaxFacts.IsPrefixUnaryExpressionOperatorToken(token) || token == SyntaxKind.TrueKeyword || token == SyntaxKind.FalseKeyword;
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x000D3704 File Offset: 0x000D1904
		public static bool IsAnyOverloadableOperator(SyntaxKind kind)
		{
			return SyntaxFacts.IsOverloadableBinaryOperator(kind) || SyntaxFacts.IsOverloadableUnaryOperator(kind);
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x000D3718 File Offset: 0x000D1918
		public static bool IsOverloadableBinaryOperator(SyntaxKind kind)
		{
			if (kind <= SyntaxKind.BarToken)
			{
				if (kind - SyntaxKind.PercentToken > 3 && kind - SyntaxKind.MinusToken > 1 && kind != SyntaxKind.BarToken)
				{
					return false;
				}
			}
			else if (kind <= SyntaxKind.GreaterThanToken)
			{
				if (kind != SyntaxKind.LessThanToken && kind != SyntaxKind.GreaterThanToken)
				{
					return false;
				}
			}
			else if (kind != SyntaxKind.SlashToken)
			{
				switch (kind)
				{
				case SyntaxKind.ExclamationEqualsToken:
				case SyntaxKind.EqualsEqualsToken:
				case SyntaxKind.LessThanEqualsToken:
				case SyntaxKind.LessThanLessThanToken:
				case SyntaxKind.GreaterThanEqualsToken:
				case SyntaxKind.GreaterThanGreaterThanToken:
					break;
				case SyntaxKind.EqualsGreaterThanToken:
				case SyntaxKind.LessThanLessThanEqualsToken:
					return false;
				default:
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x000D379E File Offset: 0x000D199E
		public static bool IsOverloadableUnaryOperator(SyntaxKind kind)
		{
			if (kind <= SyntaxKind.PlusToken)
			{
				if (kind - SyntaxKind.TildeToken > 1 && kind - SyntaxKind.MinusToken > 1)
				{
					return false;
				}
			}
			else if (kind - SyntaxKind.MinusMinusToken > 1 && kind - SyntaxKind.TrueKeyword > 1)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x000D37D5 File Offset: 0x000D19D5
		public static bool IsPrimaryFunction(SyntaxKind keyword)
		{
			return SyntaxFacts.GetPrimaryFunction(keyword) > SyntaxKind.None;
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x000D37E0 File Offset: 0x000D19E0
		public static SyntaxKind GetPrimaryFunction(SyntaxKind keyword)
		{
			if (keyword <= SyntaxKind.DefaultKeyword)
			{
				if (keyword == SyntaxKind.TypeOfKeyword)
				{
					return SyntaxKind.TypeOfExpression;
				}
				if (keyword == SyntaxKind.SizeOfKeyword)
				{
					return SyntaxKind.SizeOfExpression;
				}
				if (keyword == SyntaxKind.DefaultKeyword)
				{
					return SyntaxKind.DefaultExpression;
				}
			}
			else
			{
				switch (keyword)
				{
				case SyntaxKind.MakeRefKeyword:
					return SyntaxKind.MakeRefExpression;
				case SyntaxKind.RefTypeKeyword:
					return SyntaxKind.RefTypeExpression;
				case SyntaxKind.RefValueKeyword:
					return SyntaxKind.RefValueExpression;
				default:
					if (keyword == SyntaxKind.CheckedKeyword)
					{
						return SyntaxKind.CheckedExpression;
					}
					if (keyword == SyntaxKind.UncheckedKeyword)
					{
						return SyntaxKind.UncheckedExpression;
					}
					break;
				}
			}
			return SyntaxKind.None;
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x000D386A File Offset: 0x000D1A6A
		public static bool IsLiteralExpression(SyntaxKind token)
		{
			return SyntaxFacts.GetLiteralExpression(token) > SyntaxKind.None;
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x000D3878 File Offset: 0x000D1A78
		public static SyntaxKind GetLiteralExpression(SyntaxKind token)
		{
			switch (token)
			{
			case SyntaxKind.NullKeyword:
				return SyntaxKind.NullLiteralExpression;
			case SyntaxKind.TrueKeyword:
				return SyntaxKind.TrueLiteralExpression;
			case SyntaxKind.FalseKeyword:
				return SyntaxKind.FalseLiteralExpression;
			default:
				if (token == SyntaxKind.ArgListKeyword)
				{
					return SyntaxKind.ArgListExpression;
				}
				switch (token)
				{
				case SyntaxKind.NumericLiteralToken:
					return SyntaxKind.NumericLiteralExpression;
				case SyntaxKind.CharacterLiteralToken:
					return SyntaxKind.CharacterLiteralExpression;
				case SyntaxKind.StringLiteralToken:
					return SyntaxKind.StringLiteralExpression;
				default:
					return SyntaxKind.None;
				}
				break;
			}
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x000D38EA File Offset: 0x000D1AEA
		public static bool IsInstanceExpression(SyntaxKind token)
		{
			return SyntaxFacts.GetInstanceExpression(token) > SyntaxKind.None;
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x000D38F5 File Offset: 0x000D1AF5
		public static SyntaxKind GetInstanceExpression(SyntaxKind token)
		{
			if (token == SyntaxKind.ThisKeyword)
			{
				return SyntaxKind.ThisExpression;
			}
			if (token != SyntaxKind.BaseKeyword)
			{
				return SyntaxKind.None;
			}
			return SyntaxKind.BaseExpression;
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x000D3916 File Offset: 0x000D1B16
		public static bool IsBinaryExpression(SyntaxKind token)
		{
			return SyntaxFacts.GetBinaryExpression(token) > SyntaxKind.None;
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x000D3921 File Offset: 0x000D1B21
		public static bool IsBinaryExpressionOperatorToken(SyntaxKind token)
		{
			return SyntaxFacts.GetBinaryExpression(token) > SyntaxKind.None;
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x000D392C File Offset: 0x000D1B2C
		public static SyntaxKind GetBinaryExpression(SyntaxKind token)
		{
			if (token <= SyntaxKind.GreaterThanToken)
			{
				if (token <= SyntaxKind.BarToken)
				{
					switch (token)
					{
					case SyntaxKind.PercentToken:
						return SyntaxKind.ModuloExpression;
					case SyntaxKind.CaretToken:
						return SyntaxKind.ExclusiveOrExpression;
					case SyntaxKind.AmpersandToken:
						return SyntaxKind.BitwiseAndExpression;
					case SyntaxKind.AsteriskToken:
						return SyntaxKind.MultiplyExpression;
					case SyntaxKind.OpenParenToken:
					case SyntaxKind.CloseParenToken:
						break;
					case SyntaxKind.MinusToken:
						return SyntaxKind.SubtractExpression;
					case SyntaxKind.PlusToken:
						return SyntaxKind.AddExpression;
					default:
						if (token == SyntaxKind.BarToken)
						{
							return SyntaxKind.BitwiseOrExpression;
						}
						break;
					}
				}
				else
				{
					if (token == SyntaxKind.LessThanToken)
					{
						return SyntaxKind.LessThanExpression;
					}
					if (token == SyntaxKind.GreaterThanToken)
					{
						return SyntaxKind.GreaterThanExpression;
					}
				}
			}
			else if (token <= SyntaxKind.GreaterThanGreaterThanToken)
			{
				if (token == SyntaxKind.SlashToken)
				{
					return SyntaxKind.DivideExpression;
				}
				switch (token)
				{
				case SyntaxKind.BarBarToken:
					return SyntaxKind.LogicalOrExpression;
				case SyntaxKind.AmpersandAmpersandToken:
					return SyntaxKind.LogicalAndExpression;
				case SyntaxKind.QuestionQuestionToken:
					return SyntaxKind.CoalesceExpression;
				case SyntaxKind.ExclamationEqualsToken:
					return SyntaxKind.NotEqualsExpression;
				case SyntaxKind.EqualsEqualsToken:
					return SyntaxKind.EqualsExpression;
				case SyntaxKind.LessThanEqualsToken:
					return SyntaxKind.LessThanOrEqualExpression;
				case SyntaxKind.LessThanLessThanToken:
					return SyntaxKind.LeftShiftExpression;
				case SyntaxKind.GreaterThanEqualsToken:
					return SyntaxKind.GreaterThanOrEqualExpression;
				case SyntaxKind.GreaterThanGreaterThanToken:
					return SyntaxKind.RightShiftExpression;
				}
			}
			else
			{
				if (token == SyntaxKind.IsKeyword)
				{
					return SyntaxKind.IsExpression;
				}
				if (token == SyntaxKind.AsKeyword)
				{
					return SyntaxKind.AsExpression;
				}
			}
			return SyntaxKind.None;
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x000D3A91 File Offset: 0x000D1C91
		public static bool IsAssignmentExpression(SyntaxKind kind)
		{
			return kind - SyntaxKind.SimpleAssignmentExpression <= 11;
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x000D3AA1 File Offset: 0x000D1CA1
		public static bool IsAssignmentExpressionOperatorToken(SyntaxKind token)
		{
			return token == SyntaxKind.EqualsToken || token == SyntaxKind.LessThanLessThanEqualsToken || token - SyntaxKind.GreaterThanGreaterThanEqualsToken <= 9;
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x000D3AC4 File Offset: 0x000D1CC4
		public static SyntaxKind GetAssignmentExpression(SyntaxKind token)
		{
			if (token != SyntaxKind.EqualsToken)
			{
				switch (token)
				{
				case SyntaxKind.LessThanLessThanEqualsToken:
					return SyntaxKind.LeftShiftAssignmentExpression;
				case SyntaxKind.GreaterThanGreaterThanEqualsToken:
					return SyntaxKind.RightShiftAssignmentExpression;
				case SyntaxKind.SlashEqualsToken:
					return SyntaxKind.DivideAssignmentExpression;
				case SyntaxKind.AsteriskEqualsToken:
					return SyntaxKind.MultiplyAssignmentExpression;
				case SyntaxKind.BarEqualsToken:
					return SyntaxKind.OrAssignmentExpression;
				case SyntaxKind.AmpersandEqualsToken:
					return SyntaxKind.AndAssignmentExpression;
				case SyntaxKind.PlusEqualsToken:
					return SyntaxKind.AddAssignmentExpression;
				case SyntaxKind.MinusEqualsToken:
					return SyntaxKind.SubtractAssignmentExpression;
				case SyntaxKind.CaretEqualsToken:
					return SyntaxKind.ExclusiveOrAssignmentExpression;
				case SyntaxKind.PercentEqualsToken:
					return SyntaxKind.ModuloAssignmentExpression;
				case SyntaxKind.QuestionQuestionEqualsToken:
					return SyntaxKind.CoalesceAssignmentExpression;
				}
				return SyntaxKind.None;
			}
			return SyntaxKind.SimpleAssignmentExpression;
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x000D3B64 File Offset: 0x000D1D64
		public static SyntaxKind GetCheckStatement(SyntaxKind keyword)
		{
			if (keyword == SyntaxKind.CheckedKeyword)
			{
				return SyntaxKind.CheckedStatement;
			}
			if (keyword != SyntaxKind.UncheckedKeyword)
			{
				return SyntaxKind.None;
			}
			return SyntaxKind.UncheckedStatement;
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x000D3B88 File Offset: 0x000D1D88
		public static SyntaxKind GetAccessorDeclarationKind(SyntaxKind keyword)
		{
			switch (keyword)
			{
			case SyntaxKind.GetKeyword:
				return SyntaxKind.GetAccessorDeclaration;
			case SyntaxKind.SetKeyword:
				return SyntaxKind.SetAccessorDeclaration;
			case SyntaxKind.AddKeyword:
				return SyntaxKind.AddAccessorDeclaration;
			case SyntaxKind.RemoveKeyword:
				return SyntaxKind.RemoveAccessorDeclaration;
			default:
				if (keyword != SyntaxKind.InitKeyword)
				{
					return SyntaxKind.None;
				}
				return SyntaxKind.InitAccessorDeclaration;
			}
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x000D3BDA File Offset: 0x000D1DDA
		public static bool IsAccessorDeclaration(SyntaxKind kind)
		{
			return kind - SyntaxKind.GetAccessorDeclaration <= 3 || kind == SyntaxKind.InitAccessorDeclaration;
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x000D3BF1 File Offset: 0x000D1DF1
		public static bool IsAccessorDeclarationKeyword(SyntaxKind keyword)
		{
			return keyword - SyntaxKind.GetKeyword <= 3 || keyword == SyntaxKind.InitKeyword;
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x000D3C08 File Offset: 0x000D1E08
		public static SyntaxKind GetSwitchLabelKind(SyntaxKind keyword)
		{
			if (keyword == SyntaxKind.CaseKeyword)
			{
				return SyntaxKind.CaseSwitchLabel;
			}
			if (keyword != SyntaxKind.DefaultKeyword)
			{
				return SyntaxKind.None;
			}
			return SyntaxKind.DefaultSwitchLabel;
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x000D3C29 File Offset: 0x000D1E29
		public static SyntaxKind GetBaseTypeDeclarationKind(SyntaxKind kind)
		{
			if (kind != SyntaxKind.EnumKeyword)
			{
				return SyntaxFacts.GetTypeDeclarationKind(kind);
			}
			return SyntaxKind.EnumDeclaration;
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x000D3C3F File Offset: 0x000D1E3F
		public static SyntaxKind GetTypeDeclarationKind(SyntaxKind kind)
		{
			switch (kind)
			{
			case SyntaxKind.ClassKeyword:
				return SyntaxKind.ClassDeclaration;
			case SyntaxKind.StructKeyword:
				return SyntaxKind.StructDeclaration;
			case SyntaxKind.InterfaceKeyword:
				return SyntaxKind.InterfaceDeclaration;
			default:
				if (kind != SyntaxKind.RecordKeyword)
				{
					return SyntaxKind.None;
				}
				return SyntaxKind.RecordDeclaration;
			}
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x000D3C7C File Offset: 0x000D1E7C
		public static SyntaxKind GetKeywordKind(string text)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 2480955249U)
			{
				if (num <= 1312329493U)
				{
					if (num <= 681154065U)
					{
						if (num <= 398550328U)
						{
							if (num <= 147955286U)
							{
								if (num != 46543125U)
								{
									if (num == 147955286U)
									{
										if (text == "__makeref")
										{
											return SyntaxKind.MakeRefKeyword;
										}
									}
								}
								else if (text == "__refvalue")
								{
									return SyntaxKind.RefValueKeyword;
								}
							}
							else if (num != 184981848U)
							{
								if (num != 231090382U)
								{
									if (num == 398550328U)
									{
										if (text == "string")
										{
											return SyntaxKind.StringKeyword;
										}
									}
								}
								else if (text == "while")
								{
									return SyntaxKind.WhileKeyword;
								}
							}
							else if (text == "false")
							{
								return SyntaxKind.FalseKeyword;
							}
						}
						else if (num <= 520654156U)
						{
							if (num != 508850813U)
							{
								if (num == 520654156U)
								{
									if (text == "decimal")
									{
										return SyntaxKind.DecimalKeyword;
									}
								}
							}
							else if (text == "protected")
							{
								return SyntaxKind.ProtectedKeyword;
							}
						}
						else if (num != 554782406U)
						{
							if (num != 559900755U)
							{
								if (num == 681154065U)
								{
									if (text == "new")
									{
										return SyntaxKind.NewKeyword;
									}
								}
							}
							else if (text == "params")
							{
								return SyntaxKind.ParamsKeyword;
							}
						}
						else if (text == "checked")
						{
							return SyntaxKind.CheckedKeyword;
						}
					}
					else if (num <= 1094220446U)
					{
						if (num <= 959999494U)
						{
							if (num != 894604266U)
							{
								if (num == 959999494U)
								{
									if (text == "if")
									{
										return SyntaxKind.IfKeyword;
									}
								}
							}
							else if (text == "delegate")
							{
								return SyntaxKind.DelegateKeyword;
							}
						}
						else if (num != 1037866200U)
						{
							if (num != 1071722548U)
							{
								if (num == 1094220446U)
								{
									if (text == "in")
									{
										return SyntaxKind.InKeyword;
									}
								}
							}
							else if (text == "__reftype")
							{
								return SyntaxKind.RefTypeKeyword;
							}
						}
						else if (text == "base")
						{
							return SyntaxKind.BaseKeyword;
						}
					}
					else if (num <= 1123320834U)
					{
						if (num != 1116268876U)
						{
							if (num == 1123320834U)
							{
								if (text == "ref")
								{
									return SyntaxKind.RefKeyword;
								}
							}
						}
						else if (text == "catch")
						{
							return SyntaxKind.CatchKeyword;
						}
					}
					else if (num != 1219850847U)
					{
						if (num != 1303515621U)
						{
							if (num == 1312329493U)
							{
								if (text == "is")
								{
									return SyntaxKind.IsKeyword;
								}
							}
						}
						else if (text == "true")
						{
							return SyntaxKind.TrueKeyword;
						}
					}
					else if (text == "void")
					{
						return SyntaxKind.VoidKeyword;
					}
				}
				else if (num <= 1860254461U)
				{
					if (num <= 1657474316U)
					{
						if (num <= 1579491469U)
						{
							if (num != 1570143932U)
							{
								if (num == 1579491469U)
								{
									if (text == "as")
									{
										return SyntaxKind.AsKeyword;
									}
								}
							}
							else if (text == "virtual")
							{
								return SyntaxKind.VirtualKeyword;
							}
						}
						else if (num != 1630192034U)
						{
							if (num != 1646057492U)
							{
								if (num == 1657474316U)
								{
									if (text == "private")
									{
										return SyntaxKind.PrivateKeyword;
									}
								}
							}
							else if (text == "do")
							{
								return SyntaxKind.DoKeyword;
							}
						}
						else if (text == "ushort")
						{
							return SyntaxKind.UShortKeyword;
						}
					}
					else if (num <= 1716507092U)
					{
						if (num != 1683620383U)
						{
							if (num == 1716507092U)
							{
								if (text == "const")
								{
									return SyntaxKind.ConstKeyword;
								}
							}
						}
						else if (text == "byte")
						{
							return SyntaxKind.ByteKeyword;
						}
					}
					else if (num != 1760006473U)
					{
						if (num != 1775113223U)
						{
							if (num == 1860254461U)
							{
								if (text == "sizeof")
								{
									return SyntaxKind.SizeOfKeyword;
								}
							}
						}
						else if (text == "using")
						{
							return SyntaxKind.UsingKeyword;
						}
					}
					else if (text == "explicit")
					{
						return SyntaxKind.ExplicitKeyword;
					}
				}
				else if (num <= 2246981567U)
				{
					if (num <= 2054714927U)
					{
						if (num != 1996966820U)
						{
							if (num == 2054714927U)
							{
								if (text == "throw")
								{
									return SyntaxKind.ThrowKeyword;
								}
							}
						}
						else if (text == "null")
						{
							return SyntaxKind.NullKeyword;
						}
					}
					else if (num != 2171383808U)
					{
						if (num != 2229948720U)
						{
							if (num == 2246981567U)
							{
								if (text == "return")
								{
									return SyntaxKind.ReturnKeyword;
								}
							}
						}
						else if (text == "interface")
						{
							return SyntaxKind.InterfaceKeyword;
						}
					}
					else if (text == "enum")
					{
						return SyntaxKind.EnumKeyword;
					}
				}
				else if (num <= 2424823223U)
				{
					if (num != 2325638003U)
					{
						if (num == 2424823223U)
						{
							if (text == "extern")
							{
								return SyntaxKind.ExternKeyword;
							}
						}
					}
					else if (text == "abstract")
					{
						return SyntaxKind.AbstractKeyword;
					}
				}
				else if (num != 2462236192U)
				{
					if (num != 2470140894U)
					{
						if (num == 2480955249U)
						{
							if (text == "switch")
							{
								return SyntaxKind.SwitchKeyword;
							}
						}
					}
					else if (text == "default")
					{
						return SyntaxKind.DefaultKeyword;
					}
				}
				else if (text == "struct")
				{
					return SyntaxKind.StructKeyword;
				}
			}
			else if (num <= 3122818005U)
			{
				if (num <= 2819722039U)
				{
					if (num <= 2602907825U)
					{
						if (num <= 2515107422U)
						{
							if (num != 2497774445U)
							{
								if (num == 2515107422U)
								{
									if (text == "int")
									{
										return SyntaxKind.IntKeyword;
									}
								}
							}
							else if (text == "volatile")
							{
								return SyntaxKind.VolatileKeyword;
							}
						}
						else if (num != 2591649024U)
						{
							if (num != 2593171616U)
							{
								if (num == 2602907825U)
								{
									if (text == "case")
									{
										return SyntaxKind.CaseKeyword;
									}
								}
							}
							else if (text == "typeof")
							{
								return SyntaxKind.TypeOfKeyword;
							}
						}
						else if (text == "internal")
						{
							return SyntaxKind.InternalKeyword;
						}
					}
					else if (num <= 2667225454U)
					{
						if (num != 2618047400U)
						{
							if (num == 2667225454U)
							{
								if (text == "ulong")
								{
									return SyntaxKind.ULongKeyword;
								}
							}
						}
						else if (text == "finally")
						{
							return SyntaxKind.FinallyKeyword;
						}
					}
					else if (num != 2699759368U)
					{
						if (num != 2797886853U)
						{
							if (num == 2819722039U)
							{
								if (text == "unsafe")
								{
									return SyntaxKind.UnsafeKeyword;
								}
							}
						}
						else if (text == "float")
						{
							return SyntaxKind.FloatKeyword;
						}
					}
					else if (text == "double")
					{
						return SyntaxKind.DoubleKeyword;
					}
				}
				else if (num <= 2901640080U)
				{
					if (num <= 2870621791U)
					{
						if (num != 2823553821U)
						{
							if (num == 2870621791U)
							{
								if (text == "out")
								{
									return SyntaxKind.OutKeyword;
								}
							}
						}
						else if (text == "char")
						{
							return SyntaxKind.CharKeyword;
						}
					}
					else if (num != 2872970239U)
					{
						if (num != 2887626766U)
						{
							if (num == 2901640080U)
							{
								if (text == "for")
								{
									return SyntaxKind.ForKeyword;
								}
							}
						}
						else if (text == "try")
						{
							return SyntaxKind.TryKeyword;
						}
					}
					else if (text == "class")
					{
						return SyntaxKind.ClassKeyword;
					}
				}
				else if (num <= 3019201529U)
				{
					if (num != 2977070660U)
					{
						if (num == 3019201529U)
						{
							if (text == "fixed")
							{
								return SyntaxKind.FixedKeyword;
							}
						}
					}
					else if (text == "continue")
					{
						return SyntaxKind.ContinueKeyword;
					}
				}
				else if (num != 3076491097U)
				{
					if (num != 3099987130U)
					{
						if (num == 3122818005U)
						{
							if (text == "short")
							{
								return SyntaxKind.ShortKeyword;
							}
						}
					}
					else if (text == "object")
					{
						return SyntaxKind.ObjectKeyword;
					}
				}
				else if (text == "foreach")
				{
					return SyntaxKind.ForEachKeyword;
				}
			}
			else if (num <= 3456888823U)
			{
				if (num <= 3378807160U)
				{
					if (num <= 3270303571U)
					{
						if (num != 3183434736U)
						{
							if (num == 3270303571U)
							{
								if (text == "long")
								{
									return SyntaxKind.LongKeyword;
								}
							}
						}
						else if (text == "else")
						{
							return SyntaxKind.ElseKeyword;
						}
					}
					else if (num != 3300482109U)
					{
						if (num != 3365180733U)
						{
							if (num == 3378807160U)
							{
								if (text == "break")
								{
									return SyntaxKind.BreakKeyword;
								}
							}
						}
						else if (text == "bool")
						{
							return SyntaxKind.BoolKeyword;
						}
					}
					else if (text == "override")
					{
						return SyntaxKind.OverrideKeyword;
					}
				}
				else if (num <= 3402529440U)
				{
					if (num != 3386378657U)
					{
						if (num == 3402529440U)
						{
							if (text == "namespace")
							{
								return SyntaxKind.NamespaceKeyword;
							}
						}
					}
					else if (text == "sealed")
					{
						return SyntaxKind.SealedKeyword;
					}
				}
				else if (num != 3415750305U)
				{
					if (num != 3432027008U)
					{
						if (num == 3456888823U)
						{
							if (text == "readonly")
							{
								return SyntaxKind.ReadOnlyKeyword;
							}
						}
					}
					else if (text == "public")
					{
						return SyntaxKind.PublicKeyword;
					}
				}
				else if (text == "uint")
				{
					return SyntaxKind.UIntKeyword;
				}
			}
			else if (num <= 3894620667U)
			{
				if (num <= 3651751782U)
				{
					if (num != 3532702267U)
					{
						if (num == 3651751782U)
						{
							if (text == "implicit")
							{
								return SyntaxKind.ImplicitKeyword;
							}
						}
					}
					else if (text == "static")
					{
						return SyntaxKind.StaticKeyword;
					}
				}
				else if (num != 3660305025U)
				{
					if (num != 3887508500U)
					{
						if (num == 3894620667U)
						{
							if (text == "unchecked")
							{
								return SyntaxKind.UncheckedKeyword;
							}
						}
					}
					else if (text == "stackalloc")
					{
						return SyntaxKind.StackAllocKeyword;
					}
				}
				else if (text == "this")
				{
					return SyntaxKind.ThisKeyword;
				}
			}
			else if (num <= 4121104358U)
			{
				if (num != 4010637378U)
				{
					if (num != 4088464520U)
					{
						if (num == 4121104358U)
						{
							if (text == "goto")
							{
								return SyntaxKind.GotoKeyword;
							}
						}
					}
					else if (text == "sbyte")
					{
						return SyntaxKind.SByteKeyword;
					}
				}
				else if (text == "lock")
				{
					return SyntaxKind.LockKeyword;
				}
			}
			else if (num != 4225036029U)
			{
				if (num != 4259846381U)
				{
					if (num == 4264611999U)
					{
						if (text == "event")
						{
							return SyntaxKind.EventKeyword;
						}
					}
				}
				else if (text == "__arglist")
				{
					return SyntaxKind.ArgListKeyword;
				}
			}
			else if (text == "operator")
			{
				return SyntaxKind.OperatorKeyword;
			}
			return SyntaxKind.None;
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x000D4A44 File Offset: 0x000D2C44
		public static SyntaxKind GetOperatorKind(string operatorMetadataName)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(operatorMetadataName);
			if (num <= 1915672496U)
			{
				if (num <= 1195761148U)
				{
					if (num <= 835846267U)
					{
						if (num != 90588446U)
						{
							if (num != 215197780U)
							{
								if (num == 835846267U)
								{
									if (operatorMetadataName == "op_BitwiseAnd")
									{
										return SyntaxKind.AmpersandToken;
									}
								}
							}
							else if (operatorMetadataName == "op_Implicit")
							{
								return SyntaxKind.ImplicitKeyword;
							}
						}
						else if (operatorMetadataName == "op_OnesComplement")
						{
							return SyntaxKind.TildeToken;
						}
					}
					else if (num != 906583475U)
					{
						if (num != 1034931220U)
						{
							if (num == 1195761148U)
							{
								if (operatorMetadataName == "op_GreaterThan")
								{
									return SyntaxKind.GreaterThanToken;
								}
							}
						}
						else if (operatorMetadataName == "op_Increment")
						{
							return SyntaxKind.PlusPlusToken;
						}
					}
					else if (operatorMetadataName == "op_Addition")
					{
						return SyntaxKind.PlusToken;
					}
				}
				else if (num <= 1516143579U)
				{
					if (num != 1234170120U)
					{
						if (num != 1258540185U)
						{
							if (num == 1516143579U)
							{
								if (operatorMetadataName == "op_Equality")
								{
									return SyntaxKind.EqualsEqualsToken;
								}
							}
						}
						else if (operatorMetadataName == "op_LessThan")
						{
							return SyntaxKind.LessThanToken;
						}
					}
					else if (operatorMetadataName == "op_LessThanOrEqual")
					{
						return SyntaxKind.LessThanEqualsToken;
					}
				}
				else if (num <= 1587019679U)
				{
					if (num != 1548478473U)
					{
						if (num == 1587019679U)
						{
							if (operatorMetadataName == "op_Explicit")
							{
								return SyntaxKind.ExplicitKeyword;
							}
						}
					}
					else if (operatorMetadataName == "op_RightShift")
					{
						return SyntaxKind.GreaterThanGreaterThanToken;
					}
				}
				else if (num != 1850069070U)
				{
					if (num == 1915672496U)
					{
						if (operatorMetadataName == "op_Division")
						{
							return SyntaxKind.SlashToken;
						}
					}
				}
				else if (operatorMetadataName == "op_False")
				{
					return SyntaxKind.FalseKeyword;
				}
			}
			else if (num <= 2574677899U)
			{
				if (num <= 2429678952U)
				{
					if (num != 2242295702U)
					{
						if (num != 2366795836U)
						{
							if (num == 2429678952U)
							{
								if (operatorMetadataName == "op_Modulus")
								{
									return SyntaxKind.PercentToken;
								}
							}
						}
						else if (operatorMetadataName == "op_ExclusiveOr")
						{
							return SyntaxKind.CaretToken;
						}
					}
					else if (operatorMetadataName == "op_LeftShift")
					{
						return SyntaxKind.LessThanLessThanToken;
					}
				}
				else if (num != 2459852411U)
				{
					if (num != 2536726348U)
					{
						if (num == 2574677899U)
						{
							if (operatorMetadataName == "op_LogicalNot")
							{
								return SyntaxKind.ExclamationToken;
							}
						}
					}
					else if (operatorMetadataName == "op_Decrement")
					{
						return SyntaxKind.MinusMinusToken;
					}
				}
				else if (operatorMetadataName == "op_GreaterThanOrEqual")
				{
					return SyntaxKind.GreaterThanEqualsToken;
				}
			}
			else if (num <= 3279419199U)
			{
				if (num != 2958252495U)
				{
					if (num != 3075696130U)
					{
						if (num == 3279419199U)
						{
							if (operatorMetadataName == "op_Subtraction")
							{
								return SyntaxKind.MinusToken;
							}
						}
					}
					else if (operatorMetadataName == "op_UnaryPlus")
					{
						return SyntaxKind.PlusToken;
					}
				}
				else if (operatorMetadataName == "op_Multiply")
				{
					return SyntaxKind.AsteriskToken;
				}
			}
			else if (num <= 3568900899U)
			{
				if (num != 3492550567U)
				{
					if (num == 3568900899U)
					{
						if (operatorMetadataName == "op_True")
						{
							return SyntaxKind.TrueKeyword;
						}
					}
				}
				else if (operatorMetadataName == "op_BitwiseOr")
				{
					return SyntaxKind.BarToken;
				}
			}
			else if (num != 3716665893U)
			{
				if (num == 3794317784U)
				{
					if (operatorMetadataName == "op_Inequality")
					{
						return SyntaxKind.ExclamationEqualsToken;
					}
				}
			}
			else if (operatorMetadataName == "op_UnaryNegation")
			{
				return SyntaxKind.MinusToken;
			}
			return SyntaxKind.None;
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x000D4EB0 File Offset: 0x000D30B0
		public static SyntaxKind GetPreprocessorKeywordKind(string text)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 1788839250U)
			{
				if (num <= 563185489U)
				{
					if (num <= 400234023U)
					{
						if (num != 52100733U)
						{
							if (num != 184981848U)
							{
								if (num == 400234023U)
								{
									if (text == "line")
									{
										return SyntaxKind.LineKeyword;
									}
								}
							}
							else if (text == "false")
							{
								return SyntaxKind.FalseKeyword;
							}
						}
						else if (text == "region")
						{
							return SyntaxKind.RegionKeyword;
						}
					}
					else if (num != 435832357U)
					{
						if (num != 470986061U)
						{
							if (num == 563185489U)
							{
								if (text == "error")
								{
									return SyntaxKind.ErrorKeyword;
								}
							}
						}
						else if (text == "endif")
						{
							return SyntaxKind.EndIfKeyword;
						}
					}
					else if (text == "pragma")
					{
						return SyntaxKind.PragmaKeyword;
					}
				}
				else if (num <= 1303515621U)
				{
					if (num != 959999494U)
					{
						if (num != 1259359124U)
						{
							if (num == 1303515621U)
							{
								if (text == "true")
								{
									return SyntaxKind.TrueKeyword;
								}
							}
						}
						else if (text == "warnings")
						{
							return SyntaxKind.WarningsKeyword;
						}
					}
					else if (text == "if")
					{
						return SyntaxKind.IfKeyword;
					}
				}
				else if (num != 1550724156U)
				{
					if (num != 1612041106U)
					{
						if (num == 1788839250U)
						{
							if (text == "define")
							{
								return SyntaxKind.DefineKeyword;
							}
						}
					}
					else if (text == "checksum")
					{
						return SyntaxKind.ChecksumKeyword;
					}
				}
				else if (text == "nullable")
				{
					return SyntaxKind.NullableKeyword;
				}
			}
			else if (num <= 3183434736U)
			{
				if (num <= 2470140894U)
				{
					if (num != 1964579665U)
					{
						if (num != 2032210671U)
						{
							if (num == 2470140894U)
							{
								if (text == "default")
								{
									return SyntaxKind.DefaultKeyword;
								}
							}
						}
						else if (text == "warning")
						{
							return SyntaxKind.WarningKeyword;
						}
					}
					else if (text == "undef")
					{
						return SyntaxKind.UndefKeyword;
					}
				}
				else if (num != 2759528065U)
				{
					if (num != 2945169614U)
					{
						if (num == 3183434736U)
						{
							if (text == "else")
							{
								return SyntaxKind.ElseKeyword;
							}
						}
					}
					else if (text == "enable")
					{
						return SyntaxKind.EnableKeyword;
					}
				}
				else if (text == "annotations")
				{
					return SyntaxKind.AnnotationsKeyword;
				}
			}
			else if (num <= 3454897251U)
			{
				if (num != 3185655574U)
				{
					if (num != 3232090307U)
					{
						if (num == 3454897251U)
						{
							if (text == "disable")
							{
								return SyntaxKind.DisableKeyword;
							}
						}
					}
					else if (text == "elif")
					{
						return SyntaxKind.ElifKeyword;
					}
				}
				else if (text == "endregion")
				{
					return SyntaxKind.EndRegionKeyword;
				}
			}
			else if (num <= 3859241449U)
			{
				if (num != 3704858577U)
				{
					if (num == 3859241449U)
					{
						if (text == "load")
						{
							return SyntaxKind.LoadKeyword;
						}
					}
				}
				else if (text == "restore")
				{
					return SyntaxKind.RestoreKeyword;
				}
			}
			else if (num != 4128829753U)
			{
				if (num == 4144776981U)
				{
					if (text == "r")
					{
						return SyntaxKind.ReferenceKeyword;
					}
				}
			}
			else if (text == "hidden")
			{
				return SyntaxKind.HiddenKeyword;
			}
			return SyntaxKind.None;
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x000D52EB File Offset: 0x000D34EB
		public static IEnumerable<SyntaxKind> GetContextualKeywordKinds()
		{
			int num;
			for (int i = 8405; i <= 8446; i = num + 1)
			{
				yield return (SyntaxKind)i;
				num = i;
			}
			yield break;
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x000D52F4 File Offset: 0x000D34F4
		public static bool IsContextualKeyword(SyntaxKind kind)
		{
			return kind - SyntaxKind.YieldKeyword <= 41 || kind - SyntaxKind.VarKeyword <= 1;
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x000D530E File Offset: 0x000D350E
		public static bool IsQueryContextualKeyword(SyntaxKind kind)
		{
			return kind - SyntaxKind.WhereKeyword <= 12;
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x000D5320 File Offset: 0x000D3520
		public static SyntaxKind GetContextualKeywordKind(string text)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 1679047863U)
			{
				if (num <= 1226284502U)
				{
					if (num <= 380752755U)
					{
						if (num <= 206241385U)
						{
							if (num != 90969176U)
							{
								if (num == 206241385U)
								{
									if (text == "with")
									{
										return SyntaxKind.WithKeyword;
									}
								}
							}
							else if (text == "where")
							{
								return SyntaxKind.WhereKeyword;
							}
						}
						else if (num != 254395046U)
						{
							if (num != 297952813U)
							{
								if (num == 380752755U)
								{
									if (text == "init")
									{
										return SyntaxKind.InitKeyword;
									}
								}
							}
							else if (text == "select")
							{
								return SyntaxKind.SelectKeyword;
							}
						}
						else if (text == "and")
						{
							return SyntaxKind.AndKeyword;
						}
					}
					else if (num <= 699505802U)
					{
						if (num != 503252654U)
						{
							if (num != 601537893U)
							{
								if (num == 699505802U)
								{
									if (text == "not")
									{
										return SyntaxKind.NotKeyword;
									}
								}
							}
							else if (text == "assembly")
							{
								return SyntaxKind.AssemblyKeyword;
							}
						}
						else if (text == "global")
						{
							return SyntaxKind.GlobalKeyword;
						}
					}
					else if (num != 798074659U)
					{
						if (num != 993596020U)
						{
							if (num == 1226284502U)
							{
								if (text == "managed")
								{
									return SyntaxKind.ManagedKeyword;
								}
							}
						}
						else if (text == "add")
						{
							return SyntaxKind.AddKeyword;
						}
					}
					else if (text == "into")
					{
						return SyntaxKind.IntoKeyword;
					}
				}
				else if (num <= 1410115415U)
				{
					if (num <= 1349190650U)
					{
						if (num != 1309554226U)
						{
							if (num == 1349190650U)
							{
								if (text == "let")
								{
									return SyntaxKind.LetKeyword;
								}
							}
						}
						else if (text == "param")
						{
							return SyntaxKind.ParamKeyword;
						}
					}
					else if (num != 1361572173U)
					{
						if (num != 1362922900U)
						{
							if (num == 1410115415U)
							{
								if (text == "get")
								{
									return SyntaxKind.GetKeyword;
								}
							}
						}
						else if (text == "equals")
						{
							return SyntaxKind.EqualsKeyword;
						}
					}
					else if (text == "type")
					{
						return SyntaxKind.TypeKeyword;
					}
				}
				else if (num <= 1563699588U)
				{
					if (num != 1412156564U)
					{
						if (num != 1496340684U)
						{
							if (num == 1563699588U)
							{
								if (text == "or")
								{
									return SyntaxKind.OrKeyword;
								}
							}
						}
						else if (text == "record")
						{
							return SyntaxKind.RecordKeyword;
						}
					}
					else if (text == "by")
					{
						return SyntaxKind.ByKeyword;
					}
				}
				else if (num != 1605967500U)
				{
					if (num != 1630810064U)
					{
						if (num == 1679047863U)
						{
							if (text == "nameof")
							{
								return SyntaxKind.NameOfKeyword;
							}
						}
					}
					else if (text == "on")
					{
						return SyntaxKind.OnKeyword;
					}
				}
				else if (text == "group")
				{
					return SyntaxKind.GroupKeyword;
				}
			}
			else if (num <= 3295821558U)
			{
				if (num <= 2138539289U)
				{
					if (num <= 1821831854U)
					{
						if (num != 1736598119U)
						{
							if (num == 1821831854U)
							{
								if (text == "yield")
								{
									return SyntaxKind.YieldKeyword;
								}
							}
						}
						else if (text == "field")
						{
							return SyntaxKind.FieldKeyword;
						}
					}
					else if (num != 1989400379U)
					{
						if (num != 2099675479U)
						{
							if (num == 2138539289U)
							{
								if (text == "when")
								{
									return SyntaxKind.WhenKeyword;
								}
							}
						}
						else if (text == "alias")
						{
							return SyntaxKind.AliasKeyword;
						}
					}
					else if (text == "descending")
					{
						return SyntaxKind.DescendingKeyword;
					}
				}
				else if (num <= 2696878241U)
				{
					if (num != 2317739966U)
					{
						if (num != 2513272949U)
						{
							if (num == 2696878241U)
							{
								if (text == "ascending")
								{
									return SyntaxKind.AscendingKeyword;
								}
							}
						}
						else if (text == "from")
						{
							return SyntaxKind.FromKeyword;
						}
					}
					else if (text == "var")
					{
						return SyntaxKind.VarKeyword;
					}
				}
				else if (num != 2717370895U)
				{
					if (num != 2873489200U)
					{
						if (num == 3295821558U)
						{
							if (text == "typevar")
							{
								return SyntaxKind.TypeVarKeyword;
							}
						}
					}
					else if (text == "method")
					{
						return SyntaxKind.MethodKeyword;
					}
				}
				else if (text == "async")
				{
					return SyntaxKind.AsyncKeyword;
				}
			}
			else if (num <= 3617558685U)
			{
				if (num <= 3324446467U)
				{
					if (num != 3310188186U)
					{
						if (num == 3324446467U)
						{
							if (text == "set")
							{
								return SyntaxKind.SetKeyword;
							}
						}
					}
					else if (text == "partial")
					{
						return SyntaxKind.PartialKeyword;
					}
				}
				else if (num != 3374496889U)
				{
					if (num != 3602055880U)
					{
						if (num == 3617558685U)
						{
							if (text == "module")
							{
								return SyntaxKind.ModuleKeyword;
							}
						}
					}
					else if (text == "property")
					{
						return SyntaxKind.PropertyKeyword;
					}
				}
				else if (text == "join")
				{
					return SyntaxKind.JoinKeyword;
				}
			}
			else if (num <= 3683784189U)
			{
				if (num != 3631407781U)
				{
					if (num != 3658226030U)
					{
						if (num == 3683784189U)
						{
							if (text == "remove")
							{
								return SyntaxKind.RemoveKeyword;
							}
						}
					}
					else if (text == "_")
					{
						return SyntaxKind.UnderscoreToken;
					}
				}
				else if (text == "data")
				{
					return SyntaxKind.DataKeyword;
				}
			}
			else if (num != 3691757453U)
			{
				if (num != 3826381794U)
				{
					if (num == 4174378647U)
					{
						if (text == "unmanaged")
						{
							return SyntaxKind.UnmanagedKeyword;
						}
					}
				}
				else if (text == "orderby")
				{
					return SyntaxKind.OrderByKeyword;
				}
			}
			else if (text == "await")
			{
				return SyntaxKind.AwaitKeyword;
			}
			return SyntaxKind.None;
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000D5A90 File Offset: 0x000D3C90
		public static string GetText(SyntaxKind kind)
		{
			switch (kind)
			{
			case SyntaxKind.TildeToken:
				return "~";
			case SyntaxKind.ExclamationToken:
				return "!";
			case SyntaxKind.DollarToken:
				return "$";
			case SyntaxKind.PercentToken:
				return "%";
			case SyntaxKind.CaretToken:
				return "^";
			case SyntaxKind.AmpersandToken:
				return "&";
			case SyntaxKind.AsteriskToken:
				return "*";
			case SyntaxKind.OpenParenToken:
				return "(";
			case SyntaxKind.CloseParenToken:
				return ")";
			case SyntaxKind.MinusToken:
				return "-";
			case SyntaxKind.PlusToken:
				return "+";
			case SyntaxKind.EqualsToken:
				return "=";
			case SyntaxKind.OpenBraceToken:
				return "{";
			case SyntaxKind.CloseBraceToken:
				return "}";
			case SyntaxKind.OpenBracketToken:
				return "[";
			case SyntaxKind.CloseBracketToken:
				return "]";
			case SyntaxKind.BarToken:
				return "|";
			case SyntaxKind.BackslashToken:
				return "\\";
			case SyntaxKind.ColonToken:
				return ":";
			case SyntaxKind.SemicolonToken:
				return ";";
			case SyntaxKind.DoubleQuoteToken:
				return "\"";
			case SyntaxKind.SingleQuoteToken:
				return "'";
			case SyntaxKind.LessThanToken:
				return "<";
			case SyntaxKind.CommaToken:
				return ",";
			case SyntaxKind.GreaterThanToken:
				return ">";
			case SyntaxKind.DotToken:
				return ".";
			case SyntaxKind.QuestionToken:
				return "?";
			case SyntaxKind.HashToken:
				return "#";
			case SyntaxKind.SlashToken:
				return "/";
			case SyntaxKind.DotDotToken:
				return "..";
			case SyntaxKind.SlashGreaterThanToken:
				return "/>";
			case SyntaxKind.LessThanSlashToken:
				return "</";
			case SyntaxKind.XmlCommentStartToken:
				return "<!--";
			case SyntaxKind.XmlCommentEndToken:
				return "-->";
			case SyntaxKind.XmlCDataStartToken:
				return "<![CDATA[";
			case SyntaxKind.XmlCDataEndToken:
				return "]]>";
			case SyntaxKind.XmlProcessingInstructionStartToken:
				return "<?";
			case SyntaxKind.XmlProcessingInstructionEndToken:
				return "?>";
			case SyntaxKind.BarBarToken:
				return "||";
			case SyntaxKind.AmpersandAmpersandToken:
				return "&&";
			case SyntaxKind.MinusMinusToken:
				return "--";
			case SyntaxKind.PlusPlusToken:
				return "++";
			case SyntaxKind.ColonColonToken:
				return "::";
			case SyntaxKind.QuestionQuestionToken:
				return "??";
			case SyntaxKind.MinusGreaterThanToken:
				return "->";
			case SyntaxKind.ExclamationEqualsToken:
				return "!=";
			case SyntaxKind.EqualsEqualsToken:
				return "==";
			case SyntaxKind.EqualsGreaterThanToken:
				return "=>";
			case SyntaxKind.LessThanEqualsToken:
				return "<=";
			case SyntaxKind.LessThanLessThanToken:
				return "<<";
			case SyntaxKind.LessThanLessThanEqualsToken:
				return "<<=";
			case SyntaxKind.GreaterThanEqualsToken:
				return ">=";
			case SyntaxKind.GreaterThanGreaterThanToken:
				return ">>";
			case SyntaxKind.GreaterThanGreaterThanEqualsToken:
				return ">>=";
			case SyntaxKind.SlashEqualsToken:
				return "/=";
			case SyntaxKind.AsteriskEqualsToken:
				return "*=";
			case SyntaxKind.BarEqualsToken:
				return "|=";
			case SyntaxKind.AmpersandEqualsToken:
				return "&=";
			case SyntaxKind.PlusEqualsToken:
				return "+=";
			case SyntaxKind.MinusEqualsToken:
				return "-=";
			case SyntaxKind.CaretEqualsToken:
				return "^=";
			case SyntaxKind.PercentEqualsToken:
				return "%=";
			case SyntaxKind.QuestionQuestionEqualsToken:
				return "??=";
			case SyntaxKind.BoolKeyword:
				return "bool";
			case SyntaxKind.ByteKeyword:
				return "byte";
			case SyntaxKind.SByteKeyword:
				return "sbyte";
			case SyntaxKind.ShortKeyword:
				return "short";
			case SyntaxKind.UShortKeyword:
				return "ushort";
			case SyntaxKind.IntKeyword:
				return "int";
			case SyntaxKind.UIntKeyword:
				return "uint";
			case SyntaxKind.LongKeyword:
				return "long";
			case SyntaxKind.ULongKeyword:
				return "ulong";
			case SyntaxKind.DoubleKeyword:
				return "double";
			case SyntaxKind.FloatKeyword:
				return "float";
			case SyntaxKind.DecimalKeyword:
				return "decimal";
			case SyntaxKind.StringKeyword:
				return "string";
			case SyntaxKind.CharKeyword:
				return "char";
			case SyntaxKind.VoidKeyword:
				return "void";
			case SyntaxKind.ObjectKeyword:
				return "object";
			case SyntaxKind.TypeOfKeyword:
				return "typeof";
			case SyntaxKind.SizeOfKeyword:
				return "sizeof";
			case SyntaxKind.NullKeyword:
				return "null";
			case SyntaxKind.TrueKeyword:
				return "true";
			case SyntaxKind.FalseKeyword:
				return "false";
			case SyntaxKind.IfKeyword:
				return "if";
			case SyntaxKind.ElseKeyword:
				return "else";
			case SyntaxKind.WhileKeyword:
				return "while";
			case SyntaxKind.ForKeyword:
				return "for";
			case SyntaxKind.ForEachKeyword:
				return "foreach";
			case SyntaxKind.DoKeyword:
				return "do";
			case SyntaxKind.SwitchKeyword:
				return "switch";
			case SyntaxKind.CaseKeyword:
				return "case";
			case SyntaxKind.DefaultKeyword:
				return "default";
			case SyntaxKind.TryKeyword:
				return "try";
			case SyntaxKind.CatchKeyword:
				return "catch";
			case SyntaxKind.FinallyKeyword:
				return "finally";
			case SyntaxKind.LockKeyword:
				return "lock";
			case SyntaxKind.GotoKeyword:
				return "goto";
			case SyntaxKind.BreakKeyword:
				return "break";
			case SyntaxKind.ContinueKeyword:
				return "continue";
			case SyntaxKind.ReturnKeyword:
				return "return";
			case SyntaxKind.ThrowKeyword:
				return "throw";
			case SyntaxKind.PublicKeyword:
				return "public";
			case SyntaxKind.PrivateKeyword:
				return "private";
			case SyntaxKind.InternalKeyword:
				return "internal";
			case SyntaxKind.ProtectedKeyword:
				return "protected";
			case SyntaxKind.StaticKeyword:
				return "static";
			case SyntaxKind.ReadOnlyKeyword:
				return "readonly";
			case SyntaxKind.SealedKeyword:
				return "sealed";
			case SyntaxKind.ConstKeyword:
				return "const";
			case SyntaxKind.FixedKeyword:
				return "fixed";
			case SyntaxKind.StackAllocKeyword:
				return "stackalloc";
			case SyntaxKind.VolatileKeyword:
				return "volatile";
			case SyntaxKind.NewKeyword:
				return "new";
			case SyntaxKind.OverrideKeyword:
				return "override";
			case SyntaxKind.AbstractKeyword:
				return "abstract";
			case SyntaxKind.VirtualKeyword:
				return "virtual";
			case SyntaxKind.EventKeyword:
				return "event";
			case SyntaxKind.ExternKeyword:
				return "extern";
			case SyntaxKind.RefKeyword:
				return "ref";
			case SyntaxKind.OutKeyword:
				return "out";
			case SyntaxKind.InKeyword:
				return "in";
			case SyntaxKind.IsKeyword:
				return "is";
			case SyntaxKind.AsKeyword:
				return "as";
			case SyntaxKind.ParamsKeyword:
				return "params";
			case SyntaxKind.ArgListKeyword:
				return "__arglist";
			case SyntaxKind.MakeRefKeyword:
				return "__makeref";
			case SyntaxKind.RefTypeKeyword:
				return "__reftype";
			case SyntaxKind.RefValueKeyword:
				return "__refvalue";
			case SyntaxKind.ThisKeyword:
				return "this";
			case SyntaxKind.BaseKeyword:
				return "base";
			case SyntaxKind.NamespaceKeyword:
				return "namespace";
			case SyntaxKind.UsingKeyword:
				return "using";
			case SyntaxKind.ClassKeyword:
				return "class";
			case SyntaxKind.StructKeyword:
				return "struct";
			case SyntaxKind.InterfaceKeyword:
				return "interface";
			case SyntaxKind.EnumKeyword:
				return "enum";
			case SyntaxKind.DelegateKeyword:
				return "delegate";
			case SyntaxKind.CheckedKeyword:
				return "checked";
			case SyntaxKind.UncheckedKeyword:
				return "unchecked";
			case SyntaxKind.UnsafeKeyword:
				return "unsafe";
			case SyntaxKind.OperatorKeyword:
				return "operator";
			case SyntaxKind.ExplicitKeyword:
				return "explicit";
			case SyntaxKind.ImplicitKeyword:
				return "implicit";
			case SyntaxKind.YieldKeyword:
				return "yield";
			case SyntaxKind.PartialKeyword:
				return "partial";
			case SyntaxKind.AliasKeyword:
				return "alias";
			case SyntaxKind.GlobalKeyword:
				return "global";
			case SyntaxKind.AssemblyKeyword:
				return "assembly";
			case SyntaxKind.ModuleKeyword:
				return "module";
			case SyntaxKind.TypeKeyword:
				return "type";
			case SyntaxKind.FieldKeyword:
				return "field";
			case SyntaxKind.MethodKeyword:
				return "method";
			case SyntaxKind.ParamKeyword:
				return "param";
			case SyntaxKind.PropertyKeyword:
				return "property";
			case SyntaxKind.TypeVarKeyword:
				return "typevar";
			case SyntaxKind.GetKeyword:
				return "get";
			case SyntaxKind.SetKeyword:
				return "set";
			case SyntaxKind.AddKeyword:
				return "add";
			case SyntaxKind.RemoveKeyword:
				return "remove";
			case SyntaxKind.WhereKeyword:
				return "where";
			case SyntaxKind.FromKeyword:
				return "from";
			case SyntaxKind.GroupKeyword:
				return "group";
			case SyntaxKind.JoinKeyword:
				return "join";
			case SyntaxKind.IntoKeyword:
				return "into";
			case SyntaxKind.LetKeyword:
				return "let";
			case SyntaxKind.ByKeyword:
				return "by";
			case SyntaxKind.SelectKeyword:
				return "select";
			case SyntaxKind.OrderByKeyword:
				return "orderby";
			case SyntaxKind.OnKeyword:
				return "on";
			case SyntaxKind.EqualsKeyword:
				return "equals";
			case SyntaxKind.AscendingKeyword:
				return "ascending";
			case SyntaxKind.DescendingKeyword:
				return "descending";
			case SyntaxKind.NameOfKeyword:
				return "nameof";
			case SyntaxKind.AsyncKeyword:
				return "async";
			case SyntaxKind.AwaitKeyword:
				return "await";
			case SyntaxKind.WhenKeyword:
				return "when";
			case SyntaxKind.OrKeyword:
				return "or";
			case SyntaxKind.AndKeyword:
				return "and";
			case SyntaxKind.NotKeyword:
				return "not";
			case SyntaxKind.DataKeyword:
				return "data";
			case SyntaxKind.WithKeyword:
				return "with";
			case SyntaxKind.InitKeyword:
				return "init";
			case SyntaxKind.RecordKeyword:
				return "record";
			case SyntaxKind.ManagedKeyword:
				return "managed";
			case SyntaxKind.UnmanagedKeyword:
				return "unmanaged";
			case SyntaxKind.ElifKeyword:
				return "elif";
			case SyntaxKind.EndIfKeyword:
				return "endif";
			case SyntaxKind.RegionKeyword:
				return "region";
			case SyntaxKind.EndRegionKeyword:
				return "endregion";
			case SyntaxKind.DefineKeyword:
				return "define";
			case SyntaxKind.UndefKeyword:
				return "undef";
			case SyntaxKind.WarningKeyword:
				return "warning";
			case SyntaxKind.ErrorKeyword:
				return "error";
			case SyntaxKind.LineKeyword:
				return "line";
			case SyntaxKind.PragmaKeyword:
				return "pragma";
			case SyntaxKind.HiddenKeyword:
				return "hidden";
			case SyntaxKind.ChecksumKeyword:
				return "checksum";
			case SyntaxKind.DisableKeyword:
				return "disable";
			case SyntaxKind.RestoreKeyword:
				return "restore";
			case SyntaxKind.ReferenceKeyword:
				return "r";
			case SyntaxKind.InterpolatedStringStartToken:
				return "$\"";
			case SyntaxKind.InterpolatedStringEndToken:
				return "\"";
			case SyntaxKind.InterpolatedVerbatimStringStartToken:
				return "$@\"";
			case SyntaxKind.LoadKeyword:
				return "load";
			case SyntaxKind.NullableKeyword:
				return "nullable";
			case SyntaxKind.EnableKeyword:
				return "enable";
			case SyntaxKind.WarningsKeyword:
				return "warnings";
			case SyntaxKind.AnnotationsKeyword:
				return "annotations";
			case SyntaxKind.VarKeyword:
				return "var";
			case SyntaxKind.UnderscoreToken:
				return "_";
			}
			return string.Empty;
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x000D6451 File Offset: 0x000D4651
		public static bool IsTypeParameterVarianceKeyword(SyntaxKind kind)
		{
			return kind == SyntaxKind.OutKeyword || kind == SyntaxKind.InKeyword;
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x000D6465 File Offset: 0x000D4665
		public static bool IsDocumentationCommentTrivia(SyntaxKind kind)
		{
			return kind == SyntaxKind.SingleLineDocumentationCommentTrivia || kind == SyntaxKind.MultiLineDocumentationCommentTrivia;
		}

		// Token: 0x0200074B RID: 1867
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private sealed class SyntaxKindEqualityComparer : IEqualityComparer<SyntaxKind>
		{
			// Token: 0x0600867E RID: 34430 RVA: 0x001E86F9 File Offset: 0x001E68F9
			public bool Equals(SyntaxKind x, SyntaxKind y)
			{
				return x == y;
			}

			// Token: 0x0600867F RID: 34431 RVA: 0x001E86FF File Offset: 0x001E68FF
			public int GetHashCode(SyntaxKind obj)
			{
				return (int)obj;
			}
		}
	}
}
