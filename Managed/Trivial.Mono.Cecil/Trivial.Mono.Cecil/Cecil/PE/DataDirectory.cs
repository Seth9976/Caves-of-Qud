using System;

namespace Trivial.Mono.Cecil.PE
{
	// Token: 0x020000D2 RID: 210
	internal struct DataDirectory
	{
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0001D058 File Offset: 0x0001B258
		public bool IsZero
		{
			get
			{
				return this.VirtualAddress == 0U && this.Size == 0U;
			}
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0001D06D File Offset: 0x0001B26D
		public DataDirectory(uint rva, uint size)
		{
			this.VirtualAddress = rva;
			this.Size = size;
		}

		// Token: 0x04000332 RID: 818
		public readonly uint VirtualAddress;

		// Token: 0x04000333 RID: 819
		public readonly uint Size;
	}
}
