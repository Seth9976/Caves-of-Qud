using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004C0 RID: 1216
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class AliasSymbolFromResolvedTarget : AliasSymbol
	{
		// Token: 0x060067E5 RID: 26597 RVA: 0x001871D9 File Offset: 0x001853D9
		internal AliasSymbolFromResolvedTarget(NamespaceOrTypeSymbol target, string aliasName, [global::System.Runtime.CompilerServices.Nullable(2)] Symbol containingSymbol, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Location> locations, bool isExtern)
			: base(aliasName, containingSymbol, locations, isExtern)
		{
			this._aliasTarget = target;
		}

		// Token: 0x17001A18 RID: 6680
		// (get) Token: 0x060067E6 RID: 26598 RVA: 0x001871EE File Offset: 0x001853EE
		public override NamespaceOrTypeSymbol Target
		{
			get
			{
				return this._aliasTarget;
			}
		}

		// Token: 0x060067E7 RID: 26599 RVA: 0x001871F6 File Offset: 0x001853F6
		internal override NamespaceOrTypeSymbol GetAliasTarget([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ConsList<TypeSymbol> basesBeingResolved)
		{
			return this._aliasTarget;
		}

		// Token: 0x17001A19 RID: 6681
		// (get) Token: 0x060067E8 RID: 26600 RVA: 0x001871FE File Offset: 0x001853FE
		internal override bool RequiresCompletion
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040019C6 RID: 6598
		private readonly NamespaceOrTypeSymbol _aliasTarget;
	}
}
