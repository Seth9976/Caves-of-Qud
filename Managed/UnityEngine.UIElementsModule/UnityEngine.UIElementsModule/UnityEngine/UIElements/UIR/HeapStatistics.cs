using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200062A RID: 1578
	internal struct HeapStatistics
	{
		// Token: 0x0400155F RID: 5471
		public uint numAllocs;

		// Token: 0x04001560 RID: 5472
		public uint totalSize;

		// Token: 0x04001561 RID: 5473
		public uint allocatedSize;

		// Token: 0x04001562 RID: 5474
		public uint freeSize;

		// Token: 0x04001563 RID: 5475
		public uint largestAvailableBlock;

		// Token: 0x04001564 RID: 5476
		public uint availableBlocksCount;

		// Token: 0x04001565 RID: 5477
		public uint blockCount;

		// Token: 0x04001566 RID: 5478
		public uint highWatermark;

		// Token: 0x04001567 RID: 5479
		public float fragmentation;

		// Token: 0x04001568 RID: 5480
		public HeapStatistics[] subAllocators;
	}
}
