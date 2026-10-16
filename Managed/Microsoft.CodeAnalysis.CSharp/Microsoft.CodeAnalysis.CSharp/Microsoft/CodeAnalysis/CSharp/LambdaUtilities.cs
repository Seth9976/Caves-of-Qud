using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000230 RID: 560
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class LambdaUtilities
	{
		// Token: 0x06002EBE RID: 11966 RVA: 0x000F0460 File Offset: 0x000EE660
		public static bool IsLambda(SyntaxNode node)
		{
			SyntaxKind syntaxKind = node.Kind();
			if (syntaxKind - SyntaxKind.AnonymousMethodExpression > 2)
			{
				switch (syntaxKind)
				{
				case SyntaxKind.FromClause:
					return !node.Parent.IsKind(SyntaxKind.QueryExpression);
				case SyntaxKind.LetClause:
				case SyntaxKind.JoinClause:
				case SyntaxKind.WhereClause:
				case SyntaxKind.AscendingOrdering:
				case SyntaxKind.DescendingOrdering:
				case SyntaxKind.GroupClause:
					return true;
				case SyntaxKind.JoinIntoClause:
				case SyntaxKind.OrderByClause:
					break;
				case SyntaxKind.SelectClause:
				{
					SelectClauseSyntax selectClauseSyntax = (SelectClauseSyntax)node;
					return !LambdaUtilities.IsReducedSelectOrGroupByClause(selectClauseSyntax, selectClauseSyntax.Expression);
				}
				default:
					if (syntaxKind == SyntaxKind.LocalFunctionStatement)
					{
						return true;
					}
					break;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x000F04E8 File Offset: 0x000EE6E8
		public static bool IsNotLambda(SyntaxNode node)
		{
			return !LambdaUtilities.IsLambda(node);
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x000F04F4 File Offset: 0x000EE6F4
		public static SyntaxNode GetLambda(SyntaxNode lambdaBody)
		{
			SyntaxNode syntaxNode = lambdaBody.Parent;
			if (syntaxNode.IsKind(SyntaxKind.ArrowExpressionClause))
			{
				syntaxNode = syntaxNode.Parent;
			}
			return syntaxNode;
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x000F0520 File Offset: 0x000EE720
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static SyntaxNode TryGetCorrespondingLambdaBody(SyntaxNode oldBody, SyntaxNode newLambda)
		{
			SyntaxKind syntaxKind = newLambda.Kind();
			if (syntaxKind - SyntaxKind.AnonymousMethodExpression > 2)
			{
				switch (syntaxKind)
				{
				case SyntaxKind.FromClause:
					return ((FromClauseSyntax)newLambda).Expression;
				case SyntaxKind.LetClause:
					return ((LetClauseSyntax)newLambda).Expression;
				case SyntaxKind.JoinClause:
				{
					JoinClauseSyntax joinClauseSyntax = (JoinClauseSyntax)oldBody.Parent;
					JoinClauseSyntax joinClauseSyntax2 = (JoinClauseSyntax)newLambda;
					if (joinClauseSyntax.LeftExpression != oldBody)
					{
						return joinClauseSyntax2.RightExpression;
					}
					return joinClauseSyntax2.LeftExpression;
				}
				case SyntaxKind.JoinIntoClause:
				case SyntaxKind.OrderByClause:
					break;
				case SyntaxKind.WhereClause:
					return ((WhereClauseSyntax)newLambda).Condition;
				case SyntaxKind.AscendingOrdering:
				case SyntaxKind.DescendingOrdering:
					return ((OrderingSyntax)newLambda).Expression;
				case SyntaxKind.SelectClause:
				{
					SelectClauseSyntax selectClauseSyntax = (SelectClauseSyntax)newLambda;
					if (!LambdaUtilities.IsReducedSelectOrGroupByClause(selectClauseSyntax, selectClauseSyntax.Expression))
					{
						return selectClauseSyntax.Expression;
					}
					return null;
				}
				case SyntaxKind.GroupClause:
				{
					GroupClauseSyntax groupClauseSyntax = (GroupClauseSyntax)oldBody.Parent;
					GroupClauseSyntax groupClauseSyntax2 = (GroupClauseSyntax)newLambda;
					if (groupClauseSyntax.GroupExpression != oldBody)
					{
						return groupClauseSyntax2.ByExpression;
					}
					if (!LambdaUtilities.IsReducedSelectOrGroupByClause(groupClauseSyntax2, groupClauseSyntax2.GroupExpression))
					{
						return groupClauseSyntax2.GroupExpression;
					}
					return null;
				}
				default:
					if (syntaxKind == SyntaxKind.LocalFunctionStatement)
					{
						return LambdaUtilities.GetLocalFunctionBody((LocalFunctionStatementSyntax)newLambda);
					}
					break;
				}
				throw ExceptionUtilities.UnexpectedValue(newLambda.Kind());
			}
			return ((AnonymousFunctionExpressionSyntax)newLambda).Body;
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x000F0658 File Offset: 0x000EE858
		public static SyntaxNode GetNestedFunctionBody(SyntaxNode nestedFunction)
		{
			AnonymousFunctionExpressionSyntax anonymousFunctionExpressionSyntax = nestedFunction as AnonymousFunctionExpressionSyntax;
			CSharpSyntaxNode csharpSyntaxNode;
			if (anonymousFunctionExpressionSyntax == null)
			{
				LocalFunctionStatementSyntax localFunctionStatementSyntax = nestedFunction as LocalFunctionStatementSyntax;
				if (localFunctionStatementSyntax == null)
				{
					throw ExceptionUtilities.UnexpectedValue(nestedFunction);
				}
				csharpSyntaxNode = localFunctionStatementSyntax.Body ?? localFunctionStatementSyntax.ExpressionBody.Expression;
			}
			else
			{
				csharpSyntaxNode = anonymousFunctionExpressionSyntax.Body;
			}
			return csharpSyntaxNode;
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x000F06A4 File Offset: 0x000EE8A4
		public static bool IsNotLambdaBody(SyntaxNode node)
		{
			return !LambdaUtilities.IsLambdaBody(node, false);
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x000F06B0 File Offset: 0x000EE8B0
		public static bool IsLambdaBody(SyntaxNode node, bool allowReducedLambdas = false)
		{
			SyntaxNode syntaxNode = ((node != null) ? node.Parent : null);
			if (syntaxNode == null)
			{
				return false;
			}
			SyntaxKind syntaxKind = syntaxNode.Kind();
			if (syntaxKind <= SyntaxKind.GroupClause)
			{
				if (syntaxKind - SyntaxKind.AnonymousMethodExpression <= 2)
				{
					return ((AnonymousFunctionExpressionSyntax)syntaxNode).Body == node;
				}
				switch (syntaxKind)
				{
				case SyntaxKind.FromClause:
				{
					FromClauseSyntax fromClauseSyntax = (FromClauseSyntax)syntaxNode;
					return fromClauseSyntax.Expression == node && fromClauseSyntax.Parent is QueryBodySyntax;
				}
				case SyntaxKind.LetClause:
					return ((LetClauseSyntax)syntaxNode).Expression == node;
				case SyntaxKind.JoinClause:
				{
					JoinClauseSyntax joinClauseSyntax = (JoinClauseSyntax)syntaxNode;
					return joinClauseSyntax.LeftExpression == node || joinClauseSyntax.RightExpression == node;
				}
				case SyntaxKind.WhereClause:
					return ((WhereClauseSyntax)syntaxNode).Condition == node;
				case SyntaxKind.AscendingOrdering:
				case SyntaxKind.DescendingOrdering:
					return ((OrderingSyntax)syntaxNode).Expression == node;
				case SyntaxKind.SelectClause:
				{
					SelectClauseSyntax selectClauseSyntax = (SelectClauseSyntax)syntaxNode;
					return selectClauseSyntax.Expression == node && (allowReducedLambdas || !LambdaUtilities.IsReducedSelectOrGroupByClause(selectClauseSyntax, selectClauseSyntax.Expression));
				}
				case SyntaxKind.GroupClause:
				{
					GroupClauseSyntax groupClauseSyntax = (GroupClauseSyntax)syntaxNode;
					return (groupClauseSyntax.GroupExpression == node && (allowReducedLambdas || !LambdaUtilities.IsReducedSelectOrGroupByClause(groupClauseSyntax, groupClauseSyntax.GroupExpression))) || groupClauseSyntax.ByExpression == node;
				}
				}
			}
			else
			{
				if (syntaxKind == SyntaxKind.LocalFunctionStatement)
				{
					return ((LocalFunctionStatementSyntax)syntaxNode).Body == node;
				}
				if (syntaxKind == SyntaxKind.ArrowExpressionClause)
				{
					ArrowExpressionClauseSyntax arrowExpressionClauseSyntax = (ArrowExpressionClauseSyntax)syntaxNode;
					return arrowExpressionClauseSyntax.Expression == node && arrowExpressionClauseSyntax.Parent is LocalFunctionStatementSyntax;
				}
			}
			return false;
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x000F0844 File Offset: 0x000EEA44
		private static bool IsReducedSelectOrGroupByClause(SelectOrGroupClauseSyntax selectOrGroupClause, ExpressionSyntax selectOrGroupExpression)
		{
			if (!selectOrGroupExpression.IsKind(SyntaxKind.IdentifierName))
			{
				return false;
			}
			SyntaxToken identifier = ((IdentifierNameSyntax)selectOrGroupExpression).Identifier;
			CSharpSyntaxNode parent = selectOrGroupClause.Parent.Parent;
			QueryBodySyntax queryBodySyntax;
			SyntaxToken syntaxToken;
			if (parent.IsKind(SyntaxKind.QueryExpression))
			{
				QueryExpressionSyntax queryExpressionSyntax = (QueryExpressionSyntax)parent;
				queryBodySyntax = queryExpressionSyntax.Body;
				syntaxToken = queryExpressionSyntax.FromClause.Identifier;
			}
			else
			{
				QueryContinuationSyntax queryContinuationSyntax = (QueryContinuationSyntax)parent;
				syntaxToken = queryContinuationSyntax.Identifier;
				queryBodySyntax = queryContinuationSyntax.Body;
			}
			if (!SyntaxFactory.AreEquivalent(syntaxToken, identifier))
			{
				return false;
			}
			if (selectOrGroupClause.IsKind(SyntaxKind.SelectClause) && queryBodySyntax.Clauses.Count == 0)
			{
				return false;
			}
			foreach (QueryClauseSyntax queryClauseSyntax in queryBodySyntax.Clauses)
			{
				if (!queryClauseSyntax.IsKind(SyntaxKind.WhereClause) && !queryClauseSyntax.IsKind(SyntaxKind.OrderByClause))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x000F0921 File Offset: 0x000EEB21
		public static bool IsLambdaBodyStatementOrExpression(SyntaxNode node)
		{
			return LambdaUtilities.IsLambdaBody(node, false);
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x000F092A File Offset: 0x000EEB2A
		public static bool IsLambdaBodyStatementOrExpression(SyntaxNode node, out SyntaxNode lambdaBody)
		{
			lambdaBody = node;
			return LambdaUtilities.IsLambdaBody(node, false);
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x000F0938 File Offset: 0x000EEB38
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool TryGetLambdaBodies([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode node, [NotNullWhen(true)] out SyntaxNode lambdaBody1, out SyntaxNode lambdaBody2)
		{
			lambdaBody1 = null;
			lambdaBody2 = null;
			SyntaxKind syntaxKind = node.Kind();
			if (syntaxKind - SyntaxKind.AnonymousMethodExpression > 2)
			{
				switch (syntaxKind)
				{
				case SyntaxKind.FromClause:
					if (node.Parent.IsKind(SyntaxKind.QueryExpression))
					{
						return false;
					}
					lambdaBody1 = ((FromClauseSyntax)node).Expression;
					return true;
				case SyntaxKind.LetClause:
					lambdaBody1 = ((LetClauseSyntax)node).Expression;
					return true;
				case SyntaxKind.JoinClause:
				{
					JoinClauseSyntax joinClauseSyntax = (JoinClauseSyntax)node;
					lambdaBody1 = joinClauseSyntax.LeftExpression;
					lambdaBody2 = joinClauseSyntax.RightExpression;
					return true;
				}
				case SyntaxKind.JoinIntoClause:
				case SyntaxKind.OrderByClause:
					break;
				case SyntaxKind.WhereClause:
					lambdaBody1 = ((WhereClauseSyntax)node).Condition;
					return true;
				case SyntaxKind.AscendingOrdering:
				case SyntaxKind.DescendingOrdering:
					lambdaBody1 = ((OrderingSyntax)node).Expression;
					return true;
				case SyntaxKind.SelectClause:
				{
					SelectClauseSyntax selectClauseSyntax = (SelectClauseSyntax)node;
					if (LambdaUtilities.IsReducedSelectOrGroupByClause(selectClauseSyntax, selectClauseSyntax.Expression))
					{
						return false;
					}
					lambdaBody1 = selectClauseSyntax.Expression;
					return true;
				}
				case SyntaxKind.GroupClause:
				{
					GroupClauseSyntax groupClauseSyntax = (GroupClauseSyntax)node;
					if (LambdaUtilities.IsReducedSelectOrGroupByClause(groupClauseSyntax, groupClauseSyntax.GroupExpression))
					{
						lambdaBody1 = groupClauseSyntax.ByExpression;
					}
					else
					{
						lambdaBody1 = groupClauseSyntax.GroupExpression;
						lambdaBody2 = groupClauseSyntax.ByExpression;
					}
					return true;
				}
				default:
					if (syntaxKind == SyntaxKind.LocalFunctionStatement)
					{
						lambdaBody1 = LambdaUtilities.GetLocalFunctionBody((LocalFunctionStatementSyntax)node);
						return true;
					}
					break;
				}
				return false;
			}
			lambdaBody1 = ((AnonymousFunctionExpressionSyntax)node).Body;
			return true;
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x000F0A7C File Offset: 0x000EEC7C
		public static bool AreEquivalentIgnoringLambdaBodies(SyntaxNode oldNode, SyntaxNode newNode)
		{
			IEnumerable<SyntaxToken> enumerable = oldNode.DescendantTokens((SyntaxNode node) => node == oldNode || !LambdaUtilities.IsLambdaBodyStatementOrExpression(node), false);
			IEnumerable<SyntaxToken> enumerable2 = newNode.DescendantTokens((SyntaxNode node) => node == newNode || !LambdaUtilities.IsLambdaBodyStatementOrExpression(node), false);
			return enumerable.SequenceEqual<SyntaxToken>(enumerable2, new Func<SyntaxToken, SyntaxToken, bool>(SyntaxFactory.AreEquivalent));
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x000F0AE0 File Offset: 0x000EECE0
		internal static bool IsQueryPairLambda(SyntaxNode syntax)
		{
			return syntax.IsKind(SyntaxKind.GroupClause) || syntax.IsKind(SyntaxKind.JoinClause) || syntax.IsKind(SyntaxKind.FromClause);
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x000F0B0C File Offset: 0x000EED0C
		internal static bool IsClosureScope(SyntaxNode node)
		{
			SyntaxKind syntaxKind = node.Kind();
			if (syntaxKind > SyntaxKind.CompilationUnit)
			{
				if (syntaxKind <= SyntaxKind.ArrowExpressionClause)
				{
					if (syntaxKind - SyntaxKind.ClassDeclaration > 1)
					{
						if (syntaxKind != SyntaxKind.ConstructorDeclaration && syntaxKind != SyntaxKind.ArrowExpressionClause)
						{
							goto IL_010B;
						}
						return true;
					}
				}
				else if (syntaxKind <= SyntaxKind.SwitchExpression)
				{
					if (syntaxKind == SyntaxKind.ForEachVariableStatement)
					{
						return true;
					}
					if (syntaxKind != SyntaxKind.SwitchExpression)
					{
						goto IL_010B;
					}
					return true;
				}
				else if (syntaxKind != SyntaxKind.RecordDeclaration && syntaxKind != SyntaxKind.RecordStructDeclaration)
				{
					goto IL_010B;
				}
				return true;
			}
			if (syntaxKind <= SyntaxKind.IfStatement)
			{
				if (syntaxKind != SyntaxKind.AwaitExpression)
				{
					switch (syntaxKind)
					{
					case SyntaxKind.Block:
					case SyntaxKind.ForStatement:
					case SyntaxKind.ForEachStatement:
					case SyntaxKind.UsingStatement:
						return true;
					case SyntaxKind.LocalDeclarationStatement:
					case SyntaxKind.VariableDeclaration:
					case SyntaxKind.VariableDeclarator:
					case SyntaxKind.EqualsValueClause:
					case SyntaxKind.EmptyStatement:
					case SyntaxKind.LabeledStatement:
					case SyntaxKind.GotoStatement:
					case SyntaxKind.GotoDefaultStatement:
					case SyntaxKind.BreakStatement:
					case SyntaxKind.ContinueStatement:
					case SyntaxKind.YieldBreakStatement:
						goto IL_010B;
					case SyntaxKind.ExpressionStatement:
					case SyntaxKind.GotoCaseStatement:
					case SyntaxKind.ReturnStatement:
					case SyntaxKind.YieldReturnStatement:
					case SyntaxKind.ThrowStatement:
					case SyntaxKind.WhileStatement:
					case SyntaxKind.DoStatement:
					case SyntaxKind.FixedStatement:
						break;
					default:
						if (syntaxKind - SyntaxKind.LockStatement > 1)
						{
							goto IL_010B;
						}
						break;
					}
					return true;
				}
				return true;
			}
			else if (syntaxKind != SyntaxKind.SwitchStatement && syntaxKind != SyntaxKind.CatchClause && syntaxKind != SyntaxKind.CompilationUnit)
			{
				goto IL_010B;
			}
			return true;
			IL_010B:
			if (node.Parent != null)
			{
				SyntaxKind syntaxKind2 = node.Parent.Kind();
				if (syntaxKind2 == SyntaxKind.EqualsValueClause)
				{
					return true;
				}
				if (syntaxKind2 == SyntaxKind.ForStatement)
				{
					if (((ForStatementSyntax)node.Parent).Incrementors.FirstOrDefault() == node)
					{
						return true;
					}
				}
			}
			return LambdaUtilities.IsLambdaBody(node, false) || (node is ExpressionSyntax && node.Parent != null && node.Parent.Parent == null);
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x000F0C94 File Offset: 0x000EEE94
		internal static int GetDeclaratorPosition(SyntaxNode node)
		{
			SwitchExpressionSyntax switchExpressionSyntax = node as SwitchExpressionSyntax;
			if (switchExpressionSyntax == null)
			{
				return node.SpanStart;
			}
			return switchExpressionSyntax.SwitchKeyword.SpanStart;
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x000F0CC0 File Offset: 0x000EEEC0
		private static SyntaxNode GetLocalFunctionBody(LocalFunctionStatementSyntax localFunctionStatementSyntax)
		{
			return localFunctionStatementSyntax.Body ?? localFunctionStatementSyntax.ExpressionBody.Expression;
		}
	}
}
