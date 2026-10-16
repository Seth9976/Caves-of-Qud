using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200001D RID: 29
	internal sealed class ExpressionListVariableBinder : LocalScopeBinder
	{
		// Token: 0x06000566 RID: 1382 RVA: 0x00035685 File Offset: 0x00033885
		internal ExpressionListVariableBinder(SeparatedSyntaxList<ExpressionSyntax> expressions, Binder next)
			: base(next)
		{
			this._expressions = expressions;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00035698 File Offset: 0x00033898
		protected override ImmutableArray<LocalSymbol> BuildLocals()
		{
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
			ExpressionVariableFinder.FindExpressionVariables(this, instance, this._expressions);
			return instance.ToImmutableAndFree();
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x000356BE File Offset: 0x000338BE
		internal override SyntaxNode ScopeDesignator
		{
			get
			{
				return this._expressions[0];
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x000356CC File Offset: 0x000338CC
		internal override ImmutableArray<LocalSymbol> GetDeclaredLocalsForScope(SyntaxNode scopeDesignator)
		{
			if (this.ScopeDesignator == scopeDesignator)
			{
				return this.Locals;
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000356E3 File Offset: 0x000338E3
		internal override ImmutableArray<LocalFunctionSymbol> GetDeclaredLocalFunctionsForScope(CSharpSyntaxNode scopeDesignator)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x04000055 RID: 85
		private readonly SeparatedSyntaxList<ExpressionSyntax> _expressions;
	}
}
