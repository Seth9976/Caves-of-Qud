using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000013 RID: 19
	internal sealed class BlockBinder : LocalScopeBinder
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x0003315E File Offset: 0x0003135E
		public BlockBinder(Binder enclosing, BlockSyntax block)
			: this(enclosing, block, enclosing.Flags)
		{
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0003316E File Offset: 0x0003136E
		public BlockBinder(Binder enclosing, BlockSyntax block, BinderFlags additionalFlags)
			: base(enclosing, enclosing.Flags | additionalFlags)
		{
			this._block = block;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00033186 File Offset: 0x00031386
		protected override ImmutableArray<LocalSymbol> BuildLocals()
		{
			return base.BuildLocals(this._block.Statements, this);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0003319A File Offset: 0x0003139A
		protected override ImmutableArray<LocalFunctionSymbol> BuildLocalFunctions()
		{
			return base.BuildLocalFunctions(this._block.Statements);
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x000331AD File Offset: 0x000313AD
		internal override bool IsLocalFunctionsScopeBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000331B0 File Offset: 0x000313B0
		protected override ImmutableArray<LabelSymbol> BuildLabels()
		{
			ArrayBuilder<LabelSymbol> arrayBuilder = null;
			base.BuildLabels(this._block.Statements, ref arrayBuilder);
			if (arrayBuilder == null)
			{
				return ImmutableArray<LabelSymbol>.Empty;
			}
			return arrayBuilder.ToImmutableAndFree();
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x000331E1 File Offset: 0x000313E1
		internal override bool IsLabelsScopeBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000331E4 File Offset: 0x000313E4
		internal override ImmutableArray<LocalSymbol> GetDeclaredLocalsForScope(SyntaxNode scopeDesignator)
		{
			if (this.ScopeDesignator == scopeDesignator)
			{
				return this.Locals;
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x000331FB File Offset: 0x000313FB
		internal override SyntaxNode ScopeDesignator
		{
			get
			{
				return this._block;
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00033203 File Offset: 0x00031403
		internal override ImmutableArray<LocalFunctionSymbol> GetDeclaredLocalFunctionsForScope(CSharpSyntaxNode scopeDesignator)
		{
			if (this.ScopeDesignator == scopeDesignator)
			{
				return this.LocalFunctions;
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x04000044 RID: 68
		private readonly BlockSyntax _block;
	}
}
