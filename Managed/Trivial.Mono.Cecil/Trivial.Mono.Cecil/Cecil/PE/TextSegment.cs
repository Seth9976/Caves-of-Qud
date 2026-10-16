using System;

namespace Trivial.Mono.Cecil.PE
{
	// Token: 0x020000D8 RID: 216
	internal enum TextSegment
	{
		// Token: 0x04000369 RID: 873
		ImportAddressTable,
		// Token: 0x0400036A RID: 874
		CLIHeader,
		// Token: 0x0400036B RID: 875
		Code,
		// Token: 0x0400036C RID: 876
		Resources,
		// Token: 0x0400036D RID: 877
		Data,
		// Token: 0x0400036E RID: 878
		StrongNameSignature,
		// Token: 0x0400036F RID: 879
		MetadataHeader,
		// Token: 0x04000370 RID: 880
		TableHeap,
		// Token: 0x04000371 RID: 881
		StringHeap,
		// Token: 0x04000372 RID: 882
		UserStringHeap,
		// Token: 0x04000373 RID: 883
		GuidHeap,
		// Token: 0x04000374 RID: 884
		BlobHeap,
		// Token: 0x04000375 RID: 885
		PdbHeap,
		// Token: 0x04000376 RID: 886
		DebugDirectory,
		// Token: 0x04000377 RID: 887
		ImportDirectory,
		// Token: 0x04000378 RID: 888
		ImportHintNameTable,
		// Token: 0x04000379 RID: 889
		StartupStub
	}
}
