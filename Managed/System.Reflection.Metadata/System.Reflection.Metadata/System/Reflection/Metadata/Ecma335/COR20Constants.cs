using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000111 RID: 273
	[NullableContext(1)]
	[Nullable(0)]
	internal static class COR20Constants
	{
		// Token: 0x040005F0 RID: 1520
		internal const int SizeOfCorHeader = 72;

		// Token: 0x040005F1 RID: 1521
		internal const uint COR20MetadataSignature = 1112167234U;

		// Token: 0x040005F2 RID: 1522
		internal const int MinimumSizeofMetadataHeader = 16;

		// Token: 0x040005F3 RID: 1523
		internal const int SizeofStorageHeader = 4;

		// Token: 0x040005F4 RID: 1524
		internal const int MinimumSizeofStreamHeader = 8;

		// Token: 0x040005F5 RID: 1525
		internal const string StringStreamName = "#Strings";

		// Token: 0x040005F6 RID: 1526
		internal const string BlobStreamName = "#Blob";

		// Token: 0x040005F7 RID: 1527
		internal const string GUIDStreamName = "#GUID";

		// Token: 0x040005F8 RID: 1528
		internal const string UserStringStreamName = "#US";

		// Token: 0x040005F9 RID: 1529
		internal const string CompressedMetadataTableStreamName = "#~";

		// Token: 0x040005FA RID: 1530
		internal const string UncompressedMetadataTableStreamName = "#-";

		// Token: 0x040005FB RID: 1531
		internal const string MinimalDeltaMetadataTableStreamName = "#JTD";

		// Token: 0x040005FC RID: 1532
		internal const string StandalonePdbStreamName = "#Pdb";

		// Token: 0x040005FD RID: 1533
		internal const int LargeStreamHeapSize = 4096;
	}
}
