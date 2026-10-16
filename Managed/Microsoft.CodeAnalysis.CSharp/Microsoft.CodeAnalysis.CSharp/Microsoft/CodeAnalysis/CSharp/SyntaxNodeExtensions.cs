using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000236 RID: 566
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class SyntaxNodeExtensions
	{
		// Token: 0x06002EEC RID: 12012 RVA: 0x000F155A File Offset: 0x000EF75A
		public static TNode WithAnnotations<[global::System.Runtime.CompilerServices.Nullable(0)] TNode>(this TNode node, params SyntaxAnnotation[] annotations) where TNode : CSharpSyntaxNode
		{
			return (TNode)((object)node.Green.SetAnnotations(annotations).CreateRed());
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x000F1578 File Offset: 0x000EF778
		public static bool IsAnonymousFunction(this SyntaxNode syntax)
		{
			SyntaxKind syntaxKind = syntax.Kind();
			return syntaxKind - SyntaxKind.AnonymousMethodExpression <= 2;
		}

		// Token: 0x06002EEE RID: 12014 RVA: 0x000F159C File Offset: 0x000EF79C
		public static bool IsQuery(this SyntaxNode syntax)
		{
			switch (syntax.Kind())
			{
			case SyntaxKind.QueryExpression:
			case SyntaxKind.FromClause:
			case SyntaxKind.LetClause:
			case SyntaxKind.JoinClause:
			case SyntaxKind.JoinIntoClause:
			case SyntaxKind.WhereClause:
			case SyntaxKind.OrderByClause:
			case SyntaxKind.SelectClause:
			case SyntaxKind.GroupClause:
			case SyntaxKind.QueryContinuation:
				return true;
			}
			return false;
		}

		// Token: 0x06002EEF RID: 12015 RVA: 0x000F15F8 File Offset: 0x000EF7F8
		internal static bool CanHaveAssociatedLocalBinder(this SyntaxNode syntax)
		{
			SyntaxKind syntaxKind = syntax.Kind();
			if (syntaxKind <= SyntaxKind.Attribute)
			{
				if (syntaxKind <= SyntaxKind.EqualsValueClause)
				{
					if (syntaxKind != SyntaxKind.ArgumentList && syntaxKind - SyntaxKind.AnonymousMethodExpression > 2 && syntaxKind != SyntaxKind.EqualsValueClause)
					{
						goto IL_00D0;
					}
				}
				else if (syntaxKind <= SyntaxKind.CatchClause)
				{
					if (syntaxKind != SyntaxKind.SwitchSection && syntaxKind != SyntaxKind.CatchClause)
					{
						goto IL_00D0;
					}
				}
				else if (syntaxKind != SyntaxKind.CatchFilterClause && syntaxKind != SyntaxKind.Attribute)
				{
					goto IL_00D0;
				}
			}
			else if (syntaxKind <= SyntaxKind.ArrowExpressionClause)
			{
				if (syntaxKind != SyntaxKind.ConstructorDeclaration && syntaxKind - SyntaxKind.BaseConstructorInitializer > 1 && syntaxKind != SyntaxKind.ArrowExpressionClause)
				{
					goto IL_00D0;
				}
			}
			else if (syntaxKind <= SyntaxKind.RecordDeclaration)
			{
				if (syntaxKind - SyntaxKind.SwitchExpression > 1)
				{
					if (syntaxKind != SyntaxKind.RecordDeclaration)
					{
						goto IL_00D0;
					}
					return ((RecordDeclarationSyntax)syntax).ParameterList != null;
				}
			}
			else if (syntaxKind != SyntaxKind.PrimaryConstructorBaseType)
			{
				if (syntaxKind != SyntaxKind.RecordStructDeclaration)
				{
					goto IL_00D0;
				}
				return false;
			}
			return true;
			IL_00D0:
			return syntax is StatementSyntax || (syntax as ExpressionSyntax).IsValidScopeDesignator();
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x000F16EC File Offset: 0x000EF8EC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static bool IsValidScopeDesignator(this ExpressionSyntax expression)
		{
			CSharpSyntaxNode csharpSyntaxNode = ((expression != null) ? expression.Parent : null);
			SyntaxKind? syntaxKind = ((csharpSyntaxNode != null) ? new SyntaxKind?(csharpSyntaxNode.Kind()) : default(SyntaxKind?));
			if (syntaxKind != null)
			{
				SyntaxKind valueOrDefault = syntaxKind.GetValueOrDefault();
				if (valueOrDefault > SyntaxKind.ForStatement)
				{
					if (valueOrDefault != SyntaxKind.ForEachStatement)
					{
						if (valueOrDefault == SyntaxKind.SwitchStatement)
						{
							return ((SwitchStatementSyntax)csharpSyntaxNode).Expression == expression;
						}
						if (valueOrDefault != SyntaxKind.ForEachVariableStatement)
						{
							return false;
						}
					}
					return ((CommonForEachStatementSyntax)csharpSyntaxNode).Expression == expression;
				}
				if (valueOrDefault - SyntaxKind.SimpleLambdaExpression <= 1)
				{
					return ((LambdaExpressionSyntax)csharpSyntaxNode).Body == expression;
				}
				if (valueOrDefault == SyntaxKind.ForStatement)
				{
					ForStatementSyntax forStatementSyntax = (ForStatementSyntax)csharpSyntaxNode;
					return forStatementSyntax.Condition == expression || forStatementSyntax.Incrementors.FirstOrDefault() == expression;
				}
			}
			return false;
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x000F17C0 File Offset: 0x000EF9C0
		internal static bool IsLegalCSharp73SpanStackAllocPosition(this SyntaxNode node)
		{
			if (node.Parent.IsKind(SyntaxKind.CastExpression))
			{
				node = node.Parent;
			}
			while (node.Parent.IsKind(SyntaxKind.ConditionalExpression))
			{
				node = node.Parent;
			}
			SyntaxNode parent = node.Parent;
			if (parent == null)
			{
				return false;
			}
			SyntaxKind syntaxKind = parent.Kind();
			if (syntaxKind == SyntaxKind.SimpleAssignmentExpression)
			{
				return parent.Parent.IsKind(SyntaxKind.ExpressionStatement);
			}
			if (syntaxKind == SyntaxKind.EqualsValueClause)
			{
				SyntaxNode parent2 = parent.Parent;
				return parent2.IsKind(SyntaxKind.VariableDeclarator) && parent2.Parent.IsKind(SyntaxKind.VariableDeclaration);
			}
			return false;
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x000F185F File Offset: 0x000EFA5F
		internal static CSharpSyntaxNode AnonymousFunctionBody(this SyntaxNode lambda)
		{
			return ((AnonymousFunctionExpressionSyntax)lambda).Body;
		}

		// Token: 0x06002EF3 RID: 12019 RVA: 0x000F186C File Offset: 0x000EFA6C
		internal static SyntaxToken ExtractAnonymousTypeMemberName(this ExpressionSyntax input)
		{
			for (;;)
			{
				SyntaxKind syntaxKind = input.Kind();
				if (syntaxKind == SyntaxKind.IdentifierName)
				{
					goto IL_0021;
				}
				if (syntaxKind != SyntaxKind.SimpleMemberAccessExpression)
				{
					if (syntaxKind != SyntaxKind.ConditionalAccessExpression)
					{
						break;
					}
					input = ((ConditionalAccessExpressionSyntax)input).WhenNotNull;
					if (input.Kind() == SyntaxKind.MemberBindingExpression)
					{
						goto Block_3;
					}
				}
				else
				{
					input = ((MemberAccessExpressionSyntax)input).Name;
				}
			}
			return default(SyntaxToken);
			IL_0021:
			return ((IdentifierNameSyntax)input).Identifier;
			Block_3:
			return ((MemberBindingExpressionSyntax)input).Name.Identifier;
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x000F18EC File Offset: 0x000EFAEC
		internal static RefKind GetRefKind(this TypeSyntax syntax)
		{
			RefKind refKind;
			syntax.SkipRef(out refKind);
			return refKind;
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x000F1903 File Offset: 0x000EFB03
		internal static TypeSyntax SkipRef(this TypeSyntax syntax)
		{
			if (syntax.Kind() == SyntaxKind.RefType)
			{
				syntax = ((RefTypeSyntax)syntax).Type;
			}
			return syntax;
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x000F1920 File Offset: 0x000EFB20
		internal static TypeSyntax SkipRef(this TypeSyntax syntax, out RefKind refKind)
		{
			refKind = RefKind.None;
			if (syntax.Kind() == SyntaxKind.RefType)
			{
				RefTypeSyntax refTypeSyntax = (RefTypeSyntax)syntax;
				refKind = ((refTypeSyntax.ReadOnlyKeyword.Kind() == SyntaxKind.ReadOnlyKeyword) ? RefKind.In : RefKind.Ref);
				syntax = refTypeSyntax.Type;
			}
			return syntax;
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x000F1965 File Offset: 0x000EFB65
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static ExpressionSyntax CheckAndUnwrapRefExpression(this ExpressionSyntax syntax, [global::System.Runtime.CompilerServices.Nullable(1)] BindingDiagnosticBag diagnostics, out RefKind refKind)
		{
			refKind = RefKind.None;
			if (syntax != null && syntax.Kind() == SyntaxKind.RefExpression)
			{
				refKind = RefKind.Ref;
				syntax = ((RefExpressionSyntax)syntax).Expression;
				syntax.CheckDeconstructionCompatibleArgument(diagnostics);
			}
			return syntax;
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x000F1992 File Offset: 0x000EFB92
		internal static void CheckDeconstructionCompatibleArgument(this ExpressionSyntax expression, BindingDiagnosticBag diagnostics)
		{
			if (SyntaxNodeExtensions.IsDeconstructionCompatibleArgument(expression))
			{
				diagnostics.Add(ErrorCode.ERR_VarInvocationLvalueReserved, expression.GetLocation());
			}
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x000F19B0 File Offset: 0x000EFBB0
		private static bool IsDeconstructionCompatibleArgument(ExpressionSyntax expression)
		{
			if (expression.Kind() == SyntaxKind.InvocationExpression)
			{
				ExpressionSyntax expression2 = ((InvocationExpressionSyntax)expression).Expression;
				return expression2.Kind() == SyntaxKind.IdentifierName && ((IdentifierNameSyntax)expression2).IsVar;
			}
			return false;
		}
	}
}
