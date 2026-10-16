using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200001B RID: 27
	internal sealed class EmbeddedStatementBinder : LocalScopeBinder
	{
		// Token: 0x06000554 RID: 1364 RVA: 0x000352CB File Offset: 0x000334CB
		public EmbeddedStatementBinder(Binder enclosing, StatementSyntax statement)
			: base(enclosing, enclosing.Flags)
		{
			this._statement = statement;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000352E4 File Offset: 0x000334E4
		protected override ImmutableArray<LocalSymbol> BuildLocals()
		{
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
			base.BuildLocals(this, this._statement, instance);
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0003530C File Offset: 0x0003350C
		protected override ImmutableArray<LocalFunctionSymbol> BuildLocalFunctions()
		{
			ArrayBuilder<LocalFunctionSymbol> arrayBuilder = null;
			base.BuildLocalFunctions(this._statement, ref arrayBuilder);
			if (arrayBuilder == null)
			{
				return ImmutableArray<LocalFunctionSymbol>.Empty;
			}
			return arrayBuilder.ToImmutableAndFree();
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00035338 File Offset: 0x00033538
		internal override bool IsLocalFunctionsScopeBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0003533C File Offset: 0x0003353C
		protected override ImmutableArray<LabelSymbol> BuildLabels()
		{
			ArrayBuilder<LabelSymbol> arrayBuilder = null;
			LocalScopeBinder.BuildLabels((MethodSymbol)this.ContainingMemberOrLambda, this._statement, ref arrayBuilder);
			if (arrayBuilder == null)
			{
				return ImmutableArray<LabelSymbol>.Empty;
			}
			return arrayBuilder.ToImmutableAndFree();
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00035372 File Offset: 0x00033572
		internal override bool IsLabelsScopeBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00035375 File Offset: 0x00033575
		internal override ImmutableArray<LocalSymbol> GetDeclaredLocalsForScope(SyntaxNode scopeDesignator)
		{
			if (this.ScopeDesignator == scopeDesignator)
			{
				return this.Locals;
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0003538C File Offset: 0x0003358C
		internal override SyntaxNode ScopeDesignator
		{
			get
			{
				return this._statement;
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00035394 File Offset: 0x00033594
		internal override ImmutableArray<LocalFunctionSymbol> GetDeclaredLocalFunctionsForScope(CSharpSyntaxNode scopeDesignator)
		{
			if (this.ScopeDesignator == scopeDesignator)
			{
				return this.LocalFunctions;
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x04000050 RID: 80
		private readonly StatementSyntax _statement;
	}
}
