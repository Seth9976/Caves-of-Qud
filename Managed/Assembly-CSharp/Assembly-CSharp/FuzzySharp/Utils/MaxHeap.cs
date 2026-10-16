using System;
using System.Collections.Generic;

namespace FuzzySharp.Utils
{
	// Token: 0x020001AA RID: 426
	public class MaxHeap<T> : Heap<T>
	{
		// Token: 0x06001034 RID: 4148 RVA: 0x00064F7B File Offset: 0x0006317B
		public MaxHeap()
			: this(Comparer<T>.Default)
		{
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x00064F88 File Offset: 0x00063188
		public MaxHeap(Comparer<T> comparer)
			: base(comparer)
		{
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00064F91 File Offset: 0x00063191
		public MaxHeap(IEnumerable<T> collection, Comparer<T> comparer)
			: base(collection, comparer)
		{
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00064F9B File Offset: 0x0006319B
		public MaxHeap(IEnumerable<T> collection)
			: base(collection)
		{
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00064FA4 File Offset: 0x000631A4
		protected override bool Dominates(T x, T y)
		{
			return base.Comparer.Compare(x, y) >= 0;
		}
	}
}
