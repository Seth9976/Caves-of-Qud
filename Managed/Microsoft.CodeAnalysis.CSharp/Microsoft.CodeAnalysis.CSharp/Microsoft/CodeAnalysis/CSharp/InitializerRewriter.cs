using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001F8 RID: 504
	internal static class InitializerRewriter
	{
		// Token: 0x060022AF RID: 8879 RVA: 0x000B6D24 File Offset: 0x000B4F24
		internal static BoundTypeOrInstanceInitializers RewriteConstructor(ImmutableArray<BoundInitializer> boundInitializers, MethodSymbol method)
		{
			SourceMemberMethodSymbol sourceMemberMethodSymbol = method as SourceMemberMethodSymbol;
			return new BoundTypeOrInstanceInitializers((sourceMemberMethodSymbol != null) ? sourceMemberMethodSymbol.SyntaxNode : method.GetNonNullSyntaxNode(), boundInitializers.SelectAsArray<BoundInitializer, BoundStatement>(new Func<BoundInitializer, BoundStatement>(InitializerRewriter.RewriteInitializersAsStatements)), false);
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x000B6D64 File Offset: 0x000B4F64
		internal static BoundTypeOrInstanceInitializers RewriteScriptInitializer(ImmutableArray<BoundInitializer> boundInitializers, SynthesizedInteractiveInitializerMethod method, out bool hasTrailingExpression)
		{
			ArrayBuilder<BoundStatement> instance = ArrayBuilder<BoundStatement>.GetInstance(boundInitializers.Length);
			bool flag = method.ResultType != null;
			BoundStatement boundStatement = null;
			BoundExpression boundExpression = null;
			foreach (BoundInitializer boundInitializer in boundInitializers)
			{
				if (flag && boundInitializer == boundInitializers.Last<BoundInitializer>() && boundInitializer.Kind == BoundKind.GlobalStatementInitializer && method.DeclaringCompilation.IsSubmissionSyntaxTree(boundInitializer.SyntaxTree))
				{
					boundStatement = ((BoundGlobalStatementInitializer)boundInitializer).Statement;
					BoundExpression trailingScriptExpression = InitializerRewriter.GetTrailingScriptExpression(boundStatement);
					if (trailingScriptExpression != null && trailingScriptExpression.Type != null && !trailingScriptExpression.Type.IsVoidType())
					{
						boundExpression = trailingScriptExpression;
						continue;
					}
				}
				instance.Add(InitializerRewriter.RewriteInitializersAsStatements(boundInitializer));
			}
			if (flag && boundExpression != null)
			{
				instance.Add(new BoundReturnStatement(boundStatement.Syntax, RefKind.None, boundExpression, false));
				hasTrailingExpression = true;
			}
			else
			{
				hasTrailingExpression = false;
			}
			return new BoundTypeOrInstanceInitializers(method.GetNonNullSyntaxNode(), instance.ToImmutableAndFree(), false);
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x000B6E4C File Offset: 0x000B504C
		internal static BoundExpression GetTrailingScriptExpression(BoundStatement statement)
		{
			if (statement.Kind != BoundKind.ExpressionStatement || !((ExpressionStatementSyntax)statement.Syntax).SemicolonToken.IsMissing)
			{
				return null;
			}
			return ((BoundExpressionStatement)statement).Expression;
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x000B6E8C File Offset: 0x000B508C
		private static BoundStatement RewriteFieldInitializer(BoundFieldEqualsValue fieldInit)
		{
			SyntaxNode syntaxNode = fieldInit.Syntax;
			EqualsValueClauseSyntax equalsValueClauseSyntax = syntaxNode as EqualsValueClauseSyntax;
			syntaxNode = ((equalsValueClauseSyntax != null) ? equalsValueClauseSyntax.Value : null) ?? syntaxNode;
			BoundThisReference boundThisReference = (fieldInit.Field.IsStatic ? null : new BoundThisReference(syntaxNode, fieldInit.Field.ContainingType));
			BoundStatement boundStatement = new BoundExpressionStatement(syntaxNode, new BoundAssignmentOperator(syntaxNode, new BoundFieldAccess(syntaxNode, boundThisReference, fieldInit.Field, null, false), fieldInit.Value, fieldInit.Field.Type, false, false)
			{
				WasCompilerGenerated = true
			}, false)
			{
				WasCompilerGenerated = (!fieldInit.Locals.IsEmpty || fieldInit.WasCompilerGenerated)
			};
			if (!fieldInit.Locals.IsEmpty)
			{
				boundStatement = new BoundBlock(syntaxNode, fieldInit.Locals, ImmutableArray.Create<BoundStatement>(boundStatement), false)
				{
					WasCompilerGenerated = fieldInit.WasCompilerGenerated
				};
			}
			return boundStatement;
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x000B6F64 File Offset: 0x000B5164
		private static BoundStatement RewriteInitializersAsStatements(BoundInitializer initializer)
		{
			BoundKind kind = initializer.Kind;
			if (kind == BoundKind.FieldEqualsValue)
			{
				return InitializerRewriter.RewriteFieldInitializer((BoundFieldEqualsValue)initializer);
			}
			if (kind != BoundKind.GlobalStatementInitializer)
			{
				throw ExceptionUtilities.UnexpectedValue(initializer.Kind);
			}
			return ((BoundGlobalStatementInitializer)initializer).Statement;
		}
	}
}
