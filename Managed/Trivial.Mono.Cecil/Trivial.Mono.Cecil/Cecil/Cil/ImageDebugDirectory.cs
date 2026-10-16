using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x0200010E RID: 270
	public struct ImageDebugDirectory
	{
		// Token: 0x04000554 RID: 1364
		public const int Size = 28;

		// Token: 0x04000555 RID: 1365
		public int Characteristics;

		// Token: 0x04000556 RID: 1366
		public int TimeDateStamp;

		// Token: 0x04000557 RID: 1367
		public short MajorVersion;

		// Token: 0x04000558 RID: 1368
		public short MinorVersion;

		// Token: 0x04000559 RID: 1369
		public ImageDebugType Type;

		// Token: 0x0400055A RID: 1370
		public int SizeOfData;

		// Token: 0x0400055B RID: 1371
		public int AddressOfRawData;

		// Token: 0x0400055C RID: 1372
		public int PointerToRawData;
	}
}
