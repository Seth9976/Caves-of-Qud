using System;

namespace Mono.Cecil.PE
{
	// Token: 0x02000293 RID: 659
	internal enum TextSegment
	{
		// Token: 0x0400062F RID: 1583
		ImportAddressTable,
		// Token: 0x04000630 RID: 1584
		CLIHeader,
		// Token: 0x04000631 RID: 1585
		Code,
		// Token: 0x04000632 RID: 1586
		Resources,
		// Token: 0x04000633 RID: 1587
		Data,
		// Token: 0x04000634 RID: 1588
		StrongNameSignature,
		// Token: 0x04000635 RID: 1589
		MetadataHeader,
		// Token: 0x04000636 RID: 1590
		TableHeap,
		// Token: 0x04000637 RID: 1591
		StringHeap,
		// Token: 0x04000638 RID: 1592
		UserStringHeap,
		// Token: 0x04000639 RID: 1593
		GuidHeap,
		// Token: 0x0400063A RID: 1594
		BlobHeap,
		// Token: 0x0400063B RID: 1595
		PdbHeap,
		// Token: 0x0400063C RID: 1596
		DebugDirectory,
		// Token: 0x0400063D RID: 1597
		ImportDirectory,
		// Token: 0x0400063E RID: 1598
		ImportHintNameTable,
		// Token: 0x0400063F RID: 1599
		StartupStub
	}
}
