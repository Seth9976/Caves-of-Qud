using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002D2 RID: 722
	public struct ImageDebugDirectory
	{
		// Token: 0x0400094C RID: 2380
		public const int Size = 28;

		// Token: 0x0400094D RID: 2381
		public int Characteristics;

		// Token: 0x0400094E RID: 2382
		public int TimeDateStamp;

		// Token: 0x0400094F RID: 2383
		public short MajorVersion;

		// Token: 0x04000950 RID: 2384
		public short MinorVersion;

		// Token: 0x04000951 RID: 2385
		public ImageDebugType Type;

		// Token: 0x04000952 RID: 2386
		public int SizeOfData;

		// Token: 0x04000953 RID: 2387
		public int AddressOfRawData;

		// Token: 0x04000954 RID: 2388
		public int PointerToRawData;
	}
}
