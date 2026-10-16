using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000106 RID: 262
	internal enum HeapSizeFlag : byte
	{
		// Token: 0x040005A1 RID: 1441
		StringHeapLarge = 1,
		// Token: 0x040005A2 RID: 1442
		GuidHeapLarge,
		// Token: 0x040005A3 RID: 1443
		BlobHeapLarge = 4,
		// Token: 0x040005A4 RID: 1444
		EncDeltas = 32,
		// Token: 0x040005A5 RID: 1445
		DeletedMarks = 128
	}
}
