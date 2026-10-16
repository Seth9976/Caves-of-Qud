using System;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000F1 RID: 241
	internal struct TableInformation
	{
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x00020382 File Offset: 0x0001E582
		public bool IsLarge
		{
			get
			{
				return this.Length > 65535U;
			}
		}

		// Token: 0x0400041A RID: 1050
		public uint Offset;

		// Token: 0x0400041B RID: 1051
		public uint Length;

		// Token: 0x0400041C RID: 1052
		public uint RowSize;
	}
}
