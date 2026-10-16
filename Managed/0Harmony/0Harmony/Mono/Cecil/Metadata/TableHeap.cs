using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x020002AF RID: 687
	internal sealed class TableHeap : Heap
	{
		// Token: 0x170004A8 RID: 1192
		public TableInformation this[Table table]
		{
			get
			{
				return this.Tables[(int)table];
			}
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0003756E File Offset: 0x0003576E
		public TableHeap(byte[] data)
			: base(data)
		{
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00037584 File Offset: 0x00035784
		public bool HasTable(Table table)
		{
			return (this.Valid & (1L << (int)table)) != 0L;
		}

		// Token: 0x040006E1 RID: 1761
		public long Valid;

		// Token: 0x040006E2 RID: 1762
		public long Sorted;

		// Token: 0x040006E3 RID: 1763
		public readonly TableInformation[] Tables = new TableInformation[58];
	}
}
