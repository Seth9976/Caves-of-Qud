using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000077 RID: 119
	internal sealed class WhileBinder : LoopBinder
	{
		// Token: 0x06000AC7 RID: 2759 RVA: 0x00050DB4 File Offset: 0x0004EFB4
		public WhileBinder(Binder enclosing, StatementSyntax syntax)
			: base(enclosing)
		{
			this._syntax = syntax;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00050DC4 File Offset: 0x0004EFC4
		internal override BoundWhileStatement BindWhileParts(BindingDiagnosticBag diagnostics, Binder originalBinder)
		{
			WhileStatementSyntax whileStatementSyntax = (WhileStatementSyntax)this._syntax;
			BoundExpression boundExpression = originalBinder.BindBooleanExpression(whileStatementSyntax.Condition, diagnostics);
			BoundStatement boundStatement = originalBinder.BindPossibleEmbeddedStatement(whileStatementSyntax.Statement, diagnostics);
			return new BoundWhileStatement(whileStatementSyntax, this.Locals, boundExpression, boundStatement, this.BreakLabel, this.ContinueLabel, false);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00050E14 File Offset: 0x0004F014
		internal override BoundDoStatement BindDoParts(BindingDiagnosticBag diagnostics, Binder originalBinder)
		{
			DoStatementSyntax doStatementSyntax = (DoStatementSyntax)this._syntax;
			BoundExpression boundExpression = originalBinder.BindBooleanExpression(doStatementSyntax.Condition, diagnostics);
			BoundStatement boundStatement = originalBinder.BindPossibleEmbeddedStatement(doStatementSyntax.Statement, diagnostics);
			return new BoundDoStatement(doStatementSyntax, this.Locals, boundExpression, boundStatement, this.BreakLabel, this.ContinueLabel, false);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00050E64 File Offset: 0x0004F064
		protected override ImmutableArray<LocalSymbol> BuildLocals()
		{
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
			SyntaxKind syntaxKind = this._syntax.Kind();
			ExpressionSyntax expressionSyntax;
			if (syntaxKind != SyntaxKind.WhileStatement)
			{
				if (syntaxKind != SyntaxKind.DoStatement)
				{
					throw ExceptionUtilities.UnexpectedValue(this._syntax.Kind());
				}
				expressionSyntax = ((DoStatementSyntax)this._syntax).Condition;
			}
			else
			{
				expressionSyntax = ((WhileStatementSyntax)this._syntax).Condition;
			}
			ExpressionVariableFinder.FindExpressionVariables(this, instance, expressionSyntax, null);
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00050EE0 File Offset: 0x0004F0E0
		internal override ImmutableArray<LocalSymbol> GetDeclaredLocalsForScope(SyntaxNode scopeDesignator)
		{
			if (this._syntax == scopeDesignator)
			{
				return this.Locals;
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00050EF7 File Offset: 0x0004F0F7
		internal override ImmutableArray<LocalFunctionSymbol> GetDeclaredLocalFunctionsForScope(CSharpSyntaxNode scopeDesignator)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x00050EFE File Offset: 0x0004F0FE
		internal override SyntaxNode ScopeDesignator
		{
			get
			{
				return this._syntax;
			}
		}

		// Token: 0x04000476 RID: 1142
		private readonly StatementSyntax _syntax;
	}
}
