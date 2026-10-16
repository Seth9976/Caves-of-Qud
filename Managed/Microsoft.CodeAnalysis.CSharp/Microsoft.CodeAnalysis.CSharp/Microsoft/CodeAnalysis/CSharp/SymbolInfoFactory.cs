using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000111 RID: 273
	internal static class SymbolInfoFactory
	{
		// Token: 0x06001644 RID: 5700 RVA: 0x000776E8 File Offset: 0x000758E8
		internal static SymbolInfo Create(ImmutableArray<Symbol> symbols, LookupResultKind resultKind, bool isDynamic)
		{
			if (isDynamic)
			{
				if (symbols.Length == 1)
				{
					return new SymbolInfo(symbols[0].GetPublicSymbol(), CandidateReason.LateBound);
				}
				return new SymbolInfo(symbols.GetPublicSymbols(), CandidateReason.LateBound);
			}
			else
			{
				if (resultKind != LookupResultKind.Viable)
				{
					return new SymbolInfo(symbols.GetPublicSymbols(), (symbols.Length > 0) ? resultKind.ToCandidateReason() : CandidateReason.None);
				}
				if (symbols.Length > 0)
				{
					return new SymbolInfo(symbols[0].GetPublicSymbol());
				}
				return SymbolInfo.None;
			}
		}
	}
}
