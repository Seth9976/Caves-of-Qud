using System;

namespace Mono.Cecil.Metadata
{
	// Token: 0x020002AE RID: 686
	internal struct TableInformation
	{
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x00037551 File Offset: 0x00035751
		public bool IsLarge
		{
			get
			{
				return this.Length > 65535U;
			}
		}

		// Token: 0x040006DE RID: 1758
		public uint Offset;

		// Token: 0x040006DF RID: 1759
		public uint Length;

		// Token: 0x040006E0 RID: 1760
		public uint RowSize;
	}
}
