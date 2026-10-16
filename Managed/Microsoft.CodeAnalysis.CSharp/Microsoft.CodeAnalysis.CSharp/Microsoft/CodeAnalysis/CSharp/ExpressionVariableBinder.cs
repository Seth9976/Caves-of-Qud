using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200001E RID: 30
	internal sealed class ExpressionVariableBinder : LocalScopeBinder
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x000356EA File Offset: 0x000338EA
		internal override SyntaxNode ScopeDesignator { get; }

		// Token: 0x0600056C RID: 1388 RVA: 0x000356F2 File Offset: 0x000338F2
		internal ExpressionVariableBinder(SyntaxNode scopeDesignator, Binder next)
			: base(next)
		{
			this.ScopeDesignator = scopeDesignator;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00035704 File Offset: 0x00033904
		protected override ImmutableArray<LocalSymbol> BuildLocals()
		{
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
			ExpressionVariableFinder.FindExpressionVariables(this, instance, (CSharpSyntaxNode)this.ScopeDesignator, this.GetBinder((CSharpSyntaxNode)this.ScopeDesignator));
			return instance.ToImmutableAndFree();
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00035740 File Offset: 0x00033940
		internal override ImmutableArray<LocalSymbol> GetDeclaredLocalsForScope(SyntaxNode scopeDesignator)
		{
			if (this.ScopeDesignator == scopeDesignator)
			{
				return this.Locals;
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00035757 File Offset: 0x00033957
		internal override ImmutableArray<LocalFunctionSymbol> GetDeclaredLocalFunctionsForScope(CSharpSyntaxNode scopeDesignator)
		{
			throw ExceptionUtilities.Unreachable;
		}
	}
}
