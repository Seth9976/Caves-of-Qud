using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x020002A4 RID: 676
	internal sealed class PdbHeap : Heap
	{
		// Token: 0x06001118 RID: 4376 RVA: 0x000369A3 File Offset: 0x00034BA3
		public PdbHeap(byte[] data)
			: base(data)
		{
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x000372D2 File Offset: 0x000354D2
		public bool HasTable(Table table)
		{
			return (this.TypeSystemTables & (1L << (int)table)) != 0L;
		}

		// Token: 0x04000686 RID: 1670
		public byte[] Id;

		// Token: 0x04000687 RID: 1671
		public uint EntryPoint;

		// Token: 0x04000688 RID: 1672
		public long TypeSystemTables;

		// Token: 0x04000689 RID: 1673
		public uint[] TypeSystemTableRows;
	}
}
