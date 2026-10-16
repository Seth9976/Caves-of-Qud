using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200006E RID: 110
	internal sealed class SimpleLocalScopeBinder : LocalScopeBinder
	{
		// Token: 0x06000A72 RID: 2674 RVA: 0x0004F0E3 File Offset: 0x0004D2E3
		public SimpleLocalScopeBinder(ImmutableArray<LocalSymbol> locals, Binder next)
			: base(next)
		{
			this._locals = locals;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0004F0F3 File Offset: 0x0004D2F3
		protected override ImmutableArray<LocalSymbol> BuildLocals()
		{
			return this._locals;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0004F0FB File Offset: 0x0004D2FB
		internal override ImmutableArray<LocalSymbol> GetDeclaredLocalsForScope(SyntaxNode scopeDesignator)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0004F102 File Offset: 0x0004D302
		internal override ImmutableArray<LocalFunctionSymbol> GetDeclaredLocalFunctionsForScope(CSharpSyntaxNode scopeDesignator)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x04000461 RID: 1121
		private readonly ImmutableArray<LocalSymbol> _locals;
	}
}
