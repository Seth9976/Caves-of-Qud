using System;

namespace Mono.Cecil.PE
{
	// Token: 0x0200028D RID: 653
	internal struct DataDirectory
	{
		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x00034080 File Offset: 0x00032280
		public bool IsZero
		{
			get
			{
				return this.VirtualAddress == 0U && this.Size == 0U;
			}
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00034095 File Offset: 0x00032295
		public DataDirectory(uint rva, uint size)
		{
			this.VirtualAddress = rva;
			this.Size = size;
		}

		// Token: 0x040005F2 RID: 1522
		public readonly uint VirtualAddress;

		// Token: 0x040005F3 RID: 1523
		public readonly uint Size;
	}
}
