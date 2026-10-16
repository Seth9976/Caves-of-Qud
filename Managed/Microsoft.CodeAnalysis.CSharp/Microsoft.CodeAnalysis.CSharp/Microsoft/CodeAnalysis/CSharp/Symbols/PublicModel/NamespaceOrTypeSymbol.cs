using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005C5 RID: 1477
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class NamespaceOrTypeSymbol : Symbol, INamespaceOrTypeSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x17002406 RID: 9222
		// (get) Token: 0x06007BC8 RID: 31688
		internal abstract NamespaceOrTypeSymbol UnderlyingNamespaceOrTypeSymbol { get; }

		// Token: 0x06007BC9 RID: 31689 RVA: 0x001C0C42 File Offset: 0x001BEE42
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ISymbol> INamespaceOrTypeSymbol.GetMembers()
		{
			return this.UnderlyingNamespaceOrTypeSymbol.GetMembers().GetPublicSymbols();
		}

		// Token: 0x06007BCA RID: 31690 RVA: 0x001C0C54 File Offset: 0x001BEE54
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ISymbol> INamespaceOrTypeSymbol.GetMembers(string name)
		{
			return this.UnderlyingNamespaceOrTypeSymbol.GetMembers(name).GetPublicSymbols();
		}

		// Token: 0x06007BCB RID: 31691 RVA: 0x001C0C67 File Offset: 0x001BEE67
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		ImmutableArray<INamedTypeSymbol> INamespaceOrTypeSymbol.GetTypeMembers()
		{
			return this.UnderlyingNamespaceOrTypeSymbol.GetTypeMembers().GetPublicSymbols();
		}

		// Token: 0x06007BCC RID: 31692 RVA: 0x001C0C79 File Offset: 0x001BEE79
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		ImmutableArray<INamedTypeSymbol> INamespaceOrTypeSymbol.GetTypeMembers(string name)
		{
			return this.UnderlyingNamespaceOrTypeSymbol.GetTypeMembers(name).GetPublicSymbols();
		}

		// Token: 0x06007BCD RID: 31693 RVA: 0x001C0C8C File Offset: 0x001BEE8C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		ImmutableArray<INamedTypeSymbol> INamespaceOrTypeSymbol.GetTypeMembers(string name, int arity)
		{
			return this.UnderlyingNamespaceOrTypeSymbol.GetTypeMembers(name, arity).GetPublicSymbols();
		}

		// Token: 0x17002407 RID: 9223
		// (get) Token: 0x06007BCE RID: 31694 RVA: 0x001C0CA0 File Offset: 0x001BEEA0
		bool INamespaceOrTypeSymbol.IsNamespace
		{
			get
			{
				return this.UnderlyingSymbol.Kind == SymbolKind.Namespace;
			}
		}

		// Token: 0x17002408 RID: 9224
		// (get) Token: 0x06007BCF RID: 31695 RVA: 0x001C0CB1 File Offset: 0x001BEEB1
		bool INamespaceOrTypeSymbol.IsType
		{
			get
			{
				return this.UnderlyingSymbol.Kind != SymbolKind.Namespace;
			}
		}
	}
}
