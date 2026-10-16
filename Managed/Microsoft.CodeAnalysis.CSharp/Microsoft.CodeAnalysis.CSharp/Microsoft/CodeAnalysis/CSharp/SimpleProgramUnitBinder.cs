using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000070 RID: 112
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SimpleProgramUnitBinder : LocalScopeBinder
	{
		// Token: 0x06000A7F RID: 2687 RVA: 0x0004F283 File Offset: 0x0004D483
		public SimpleProgramUnitBinder(Binder enclosing, SimpleProgramBinder scope)
			: base(enclosing, enclosing.Flags)
		{
			this._scope = scope;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0004F299 File Offset: 0x0004D499
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		protected override ImmutableArray<LocalSymbol> BuildLocals()
		{
			return this._scope.Locals;
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0004F2A6 File Offset: 0x0004D4A6
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		protected override ImmutableArray<LocalFunctionSymbol> BuildLocalFunctions()
		{
			return this._scope.LocalFunctions;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0004F2B3 File Offset: 0x0004D4B3
		internal override bool IsLocalFunctionsScopeBinder
		{
			get
			{
				return this._scope.IsLocalFunctionsScopeBinder;
			}
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0004F2C0 File Offset: 0x0004D4C0
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		protected override ImmutableArray<LabelSymbol> BuildLabels()
		{
			return ImmutableArray<LabelSymbol>.Empty;
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0004F2C7 File Offset: 0x0004D4C7
		internal override bool IsLabelsScopeBinder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0004F2CA File Offset: 0x0004D4CA
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<LocalSymbol> GetDeclaredLocalsForScope(SyntaxNode scopeDesignator)
		{
			return this._scope.GetDeclaredLocalsForScope(scopeDesignator);
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0004F2D8 File Offset: 0x0004D4D8
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override SyntaxNode ScopeDesignator
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._scope.ScopeDesignator;
			}
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0004F2E5 File Offset: 0x0004D4E5
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<LocalFunctionSymbol> GetDeclaredLocalFunctionsForScope(CSharpSyntaxNode scopeDesignator)
		{
			return this._scope.GetDeclaredLocalFunctionsForScope(scopeDesignator);
		}

		// Token: 0x04000463 RID: 1123
		private readonly SimpleProgramBinder _scope;
	}
}
