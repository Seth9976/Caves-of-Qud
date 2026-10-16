using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000140 RID: 320
	public abstract class DataFlowAnalysis
	{
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000CF7 RID: 3319
		[Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<ISymbol> VariablesDeclared
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000CF8 RID: 3320
		[Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<ISymbol> DataFlowsIn
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000CF9 RID: 3321
		[Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<ISymbol> DataFlowsOut
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000CFA RID: 3322
		[Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<ISymbol> DefinitelyAssignedOnEntry
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000CFB RID: 3323
		[Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<ISymbol> DefinitelyAssignedOnExit
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000CFC RID: 3324
		[Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<ISymbol> AlwaysAssigned
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000CFD RID: 3325
		[Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<ISymbol> ReadInside
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000CFE RID: 3326
		[Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<ISymbol> WrittenInside
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000CFF RID: 3327
		[Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<ISymbol> ReadOutside
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000D00 RID: 3328
		[Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<ISymbol> WrittenOutside
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000D01 RID: 3329
		[Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<ISymbol> Captured
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000D02 RID: 3330
		[Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<ISymbol> CapturedInside
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000D03 RID: 3331
		[Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<ISymbol> CapturedOutside
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000D04 RID: 3332
		[Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<ISymbol> UnsafeAddressTaken
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000D05 RID: 3333
		[Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<IMethodSymbol> UsedLocalFunctions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000D06 RID: 3334
		public abstract bool Succeeded { get; }
	}
}
