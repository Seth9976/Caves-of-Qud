using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000044 RID: 68
	[Map]
	[Flags]
	[CLSCompliant(false)]
	public enum MountFlags : ulong
	{
		// Token: 0x04000340 RID: 832
		ST_RDONLY = 1UL,
		// Token: 0x04000341 RID: 833
		ST_NOSUID = 2UL,
		// Token: 0x04000342 RID: 834
		ST_NODEV = 4UL,
		// Token: 0x04000343 RID: 835
		ST_NOEXEC = 8UL,
		// Token: 0x04000344 RID: 836
		ST_SYNCHRONOUS = 16UL,
		// Token: 0x04000345 RID: 837
		ST_REMOUNT = 32UL,
		// Token: 0x04000346 RID: 838
		ST_MANDLOCK = 64UL,
		// Token: 0x04000347 RID: 839
		ST_WRITE = 128UL,
		// Token: 0x04000348 RID: 840
		ST_APPEND = 256UL,
		// Token: 0x04000349 RID: 841
		ST_IMMUTABLE = 512UL,
		// Token: 0x0400034A RID: 842
		ST_NOATIME = 1024UL,
		// Token: 0x0400034B RID: 843
		ST_NODIRATIME = 2048UL,
		// Token: 0x0400034C RID: 844
		ST_BIND = 4096UL
	}
}
