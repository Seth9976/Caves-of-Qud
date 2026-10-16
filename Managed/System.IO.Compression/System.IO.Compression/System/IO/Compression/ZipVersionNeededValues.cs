using System;

namespace System.IO.Compression
{
	// Token: 0x0200003E RID: 62
	internal enum ZipVersionNeededValues : ushort
	{
		// Token: 0x04000212 RID: 530
		Default = 10,
		// Token: 0x04000213 RID: 531
		ExplicitDirectory = 20,
		// Token: 0x04000214 RID: 532
		Deflate = 20,
		// Token: 0x04000215 RID: 533
		Deflate64,
		// Token: 0x04000216 RID: 534
		Zip64 = 45
	}
}
