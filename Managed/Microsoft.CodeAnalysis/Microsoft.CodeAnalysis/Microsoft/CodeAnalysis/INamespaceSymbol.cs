using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000248 RID: 584
	[NullableContext(1)]
	public interface INamespaceSymbol : INamespaceOrTypeSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06001698 RID: 5784
		IEnumerable<INamespaceOrTypeSymbol> GetMembers();

		// Token: 0x06001699 RID: 5785
		IEnumerable<INamespaceOrTypeSymbol> GetMembers(string name);

		// Token: 0x0600169A RID: 5786
		IEnumerable<INamespaceSymbol> GetNamespaceMembers();

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x0600169B RID: 5787
		bool IsGlobalNamespace { get; }

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x0600169C RID: 5788
		NamespaceKind NamespaceKind { get; }

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x0600169D RID: 5789
		[Nullable(2)]
		Compilation ContainingCompilation
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600169E RID: 5790
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<INamespaceSymbol> ConstituentNamespaces
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
