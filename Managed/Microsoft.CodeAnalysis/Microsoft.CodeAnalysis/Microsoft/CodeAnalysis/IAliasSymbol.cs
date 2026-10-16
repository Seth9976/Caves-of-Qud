using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000239 RID: 569
	[NullableContext(1)]
	public interface IAliasSymbol : ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600160B RID: 5643
		INamespaceOrTypeSymbol Target { get; }
	}
}
