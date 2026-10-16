using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000247 RID: 583
	[NullableContext(1)]
	public interface INamespaceOrTypeSymbol : ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06001691 RID: 5777
		[return: Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ISymbol> GetMembers();

		// Token: 0x06001692 RID: 5778
		[return: Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ISymbol> GetMembers(string name);

		// Token: 0x06001693 RID: 5779
		[return: Nullable(new byte[] { 0, 1 })]
		ImmutableArray<INamedTypeSymbol> GetTypeMembers();

		// Token: 0x06001694 RID: 5780
		[return: Nullable(new byte[] { 0, 1 })]
		ImmutableArray<INamedTypeSymbol> GetTypeMembers(string name);

		// Token: 0x06001695 RID: 5781
		[return: Nullable(new byte[] { 0, 1 })]
		ImmutableArray<INamedTypeSymbol> GetTypeMembers(string name, int arity);

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001696 RID: 5782
		bool IsNamespace { get; }

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001697 RID: 5783
		bool IsType { get; }
	}
}
