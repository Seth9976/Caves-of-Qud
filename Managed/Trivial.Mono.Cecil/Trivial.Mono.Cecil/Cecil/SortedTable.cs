using System;
using System.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000B5 RID: 181
	internal abstract class SortedTable<TRow> : MetadataTable<TRow>, IComparer<TRow> where TRow : struct
	{
		// Token: 0x0600078D RID: 1933 RVA: 0x00017A36 File Offset: 0x00015C36
		public sealed override void Sort()
		{
			Array.Sort<TRow>(this.rows, 0, this.length, this);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00017A4B File Offset: 0x00015C4B
		protected int Compare(uint x, uint y)
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

		// Token: 0x0600078F RID: 1935
		public abstract int Compare(TRow x, TRow y);
	}
}
