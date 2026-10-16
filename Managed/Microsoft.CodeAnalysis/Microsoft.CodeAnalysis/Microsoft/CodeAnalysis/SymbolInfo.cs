using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000156 RID: 342
	[NullableContext(2)]
	[Nullable(0)]
	public struct SymbolInfo : IEquatable<SymbolInfo>
	{
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x0002786A File Offset: 0x00025A6A
		public readonly ISymbol Symbol { get; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x00027872 File Offset: 0x00025A72
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ISymbol> CandidateSymbols
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._candidateSymbols.NullToEmpty<ISymbol>();
			}
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0002787F File Offset: 0x00025A7F
		[return: Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<ISymbol> GetAllSymbols()
		{
			if (this.Symbol != null)
			{
				return ImmutableArray.Create<ISymbol>(this.Symbol);
			}
			return this._candidateSymbols;
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x0002789B File Offset: 0x00025A9B
		public readonly CandidateReason CandidateReason { get; }

		// Token: 0x06000DC2 RID: 3522 RVA: 0x000278A3 File Offset: 0x00025AA3
		[NullableContext(1)]
		internal SymbolInfo(ISymbol symbol)
		{
			this = new SymbolInfo(symbol, ImmutableArray<ISymbol>.Empty, CandidateReason.None);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x000278B2 File Offset: 0x00025AB2
		[NullableContext(1)]
		internal SymbolInfo(ISymbol symbol, CandidateReason reason)
		{
			this = new SymbolInfo(symbol, ImmutableArray<ISymbol>.Empty, reason);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x000278C1 File Offset: 0x00025AC1
		internal SymbolInfo([Nullable(new byte[] { 0, 1 })] ImmutableArray<ISymbol> candidateSymbols, CandidateReason candidateReason)
		{
			this = new SymbolInfo(null, candidateSymbols, candidateReason);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x000278CC File Offset: 0x00025ACC
		internal SymbolInfo(ISymbol symbol, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ISymbol> candidateSymbols, CandidateReason candidateReason)
		{
			this = default(SymbolInfo);
			this.Symbol = symbol;
			this._candidateSymbols = (candidateSymbols.IsDefault ? ImmutableArray.Create<ISymbol>() : candidateSymbols);
			this.CandidateReason = candidateReason;
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x000278FA File Offset: 0x00025AFA
		public override bool Equals(object obj)
		{
			return obj is SymbolInfo && this.Equals((SymbolInfo)obj);
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00027914 File Offset: 0x00025B14
		public bool Equals(SymbolInfo other)
		{
			return object.Equals(this.Symbol, other.Symbol) && this._candidateSymbols.IsDefault == other._candidateSymbols.IsDefault && this.CandidateReason == other.CandidateReason && (this._candidateSymbols.IsDefault || this._candidateSymbols.SequenceEqual<ISymbol, ISymbol>(other._candidateSymbols, null));
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00027980 File Offset: 0x00025B80
		public override int GetHashCode()
		{
			return Hash.Combine<ISymbol>(this.Symbol, Hash.Combine(Hash.CombineValues<ISymbol>(this._candidateSymbols, 4), (int)this.CandidateReason));
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x000279A4 File Offset: 0x00025BA4
		internal bool IsEmpty
		{
			get
			{
				return this.Symbol == null && this.CandidateSymbols.Length == 0;
			}
		}

		// Token: 0x040003C4 RID: 964
		internal static readonly SymbolInfo None = new SymbolInfo(null, ImmutableArray<ISymbol>.Empty, CandidateReason.None);

		// Token: 0x040003C5 RID: 965
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<ISymbol> _candidateSymbols;
	}
}
