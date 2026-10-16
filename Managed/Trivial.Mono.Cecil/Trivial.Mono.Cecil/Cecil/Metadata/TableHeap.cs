using System;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000EF RID: 239
	internal sealed class TableHeap : Heap
	{
		// Token: 0x170002AB RID: 683
		public TableInformation this[Table table]
		{
			get
			{
				return this.Tables[(int)table];
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001FF27 File Offset: 0x0001E127
		public TableHeap(byte[] data)
			: base(data)
		{
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0001FF3D File Offset: 0x0001E13D
		public bool HasTable(Table table)
		{
			return (this.Valid & (1L << (int)table)) != 0L;
		}

		// Token: 0x0400040D RID: 1037
		public long Valid;

		// Token: 0x0400040E RID: 1038
		public long Sorted;

		// Token: 0x0400040F RID: 1039
		public readonly TableInformation[] Tables = new TableInformation[58];
	}
}
