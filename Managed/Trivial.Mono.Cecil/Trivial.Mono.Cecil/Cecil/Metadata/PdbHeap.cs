using System;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000E2 RID: 226
	internal sealed class PdbHeap : Heap
	{
		// Token: 0x0600096E RID: 2414 RVA: 0x0001F8A7 File Offset: 0x0001DAA7
		public PdbHeap(byte[] data)
			: base(data)
		{
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0001FAE0 File Offset: 0x0001DCE0
		public bool HasTable(Table table)
		{
			return (this.TypeSystemTables & (1L << (int)table)) != 0L;
		}

		// Token: 0x040003B4 RID: 948
		public byte[] Id;

		// Token: 0x040003B5 RID: 949
		public uint EntryPoint;

		// Token: 0x040003B6 RID: 950
		public long TypeSystemTables;

		// Token: 0x040003B7 RID: 951
		public uint[] TypeSystemTableRows;
	}
}
