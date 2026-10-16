using System;
using System.Collections.Generic;

namespace FuzzySharp.Utils
{
	// Token: 0x020001AB RID: 427
	public class MinHeap<T> : Heap<T>
	{
		// Token: 0x06001039 RID: 4153 RVA: 0x00064FB9 File Offset: 0x000631B9
		public MinHeap()
			: this(Comparer<T>.Default)
		{
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00064FC6 File Offset: 0x000631C6
		public MinHeap(Comparer<T> comparer)
			: base(comparer)
		{
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x00064FCF File Offset: 0x000631CF
		public MinHeap(IEnumerable<T> collection)
			: base(collection)
		{
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00064FD8 File Offset: 0x000631D8
		public MinHeap(IEnumerable<T> collection, Comparer<T> comparer)
			: base(collection, comparer)
		{
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x00064FE2 File Offset: 0x000631E2
		protected override bool Dominates(T x, T y)
		{
			return base.Comparer.Compare(x, y) <= 0;
		}
	}
}
