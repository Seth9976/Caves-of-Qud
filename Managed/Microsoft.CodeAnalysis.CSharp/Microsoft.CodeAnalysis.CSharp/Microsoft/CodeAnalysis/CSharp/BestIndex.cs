using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000047 RID: 71
	internal struct BestIndex
	{
		// Token: 0x0600070D RID: 1805 RVA: 0x0003D7E4 File Offset: 0x0003B9E4
		public static BestIndex None()
		{
			return new BestIndex(BestIndexKind.None, 0, 0, 0);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0003D7EF File Offset: 0x0003B9EF
		public static BestIndex HasBest(int best)
		{
			return new BestIndex(BestIndexKind.Best, best, 0, 0);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0003D7FA File Offset: 0x0003B9FA
		public static BestIndex IsAmbiguous(int ambig1, int ambig2)
		{
			return new BestIndex(BestIndexKind.Ambiguous, 0, ambig1, ambig2);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0003D805 File Offset: 0x0003BA05
		private BestIndex(BestIndexKind kind, int best, int ambig1, int ambig2)
		{
			this.Kind = kind;
			this.Best = best;
			this.Ambiguous1 = ambig1;
			this.Ambiguous2 = ambig2;
		}

		// Token: 0x040000DC RID: 220
		internal readonly BestIndexKind Kind;

		// Token: 0x040000DD RID: 221
		internal readonly int Best;

		// Token: 0x040000DE RID: 222
		internal readonly int Ambiguous1;

		// Token: 0x040000DF RID: 223
		internal readonly int Ambiguous2;
	}
}
