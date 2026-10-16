using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200023E RID: 574
	public interface IErrorTypeSymbol : INamedTypeSymbol, ITypeSymbol, INamespaceOrTypeSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001622 RID: 5666
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ISymbol> CandidateSymbols
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001623 RID: 5667
		CandidateReason CandidateReason { get; }
	}
}
