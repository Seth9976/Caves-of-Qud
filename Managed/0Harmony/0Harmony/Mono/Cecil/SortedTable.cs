using System;
using System.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020001C4 RID: 452
	internal abstract class SortedTable<TRow> : MetadataTable<TRow>, IComparer<TRow> where TRow : struct
	{
		// Token: 0x06000905 RID: 2309 RVA: 0x00020D14 File Offset: 0x0001EF14
		public sealed override void Sort()
		{
			MergeSort<TRow>.Sort(this.rows, 0, this.length, this);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00020D29 File Offset: 0x0001EF29
		protected static int Compare(uint x, uint y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x <= y)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06000907 RID: 2311
		public abstract int Compare(TRow x, TRow y);
	}
}
