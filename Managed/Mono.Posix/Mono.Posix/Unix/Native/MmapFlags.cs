using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000045 RID: 69
	[Map]
	[Flags]
	[CLSCompliant(false)]
	public enum MmapFlags
	{
		// Token: 0x0400034E RID: 846
		MAP_SHARED = 1,
		// Token: 0x0400034F RID: 847
		MAP_PRIVATE = 2,
		// Token: 0x04000350 RID: 848
		MAP_TYPE = 15,
		// Token: 0x04000351 RID: 849
		MAP_FIXED = 16,
		// Token: 0x04000352 RID: 850
		MAP_FILE = 0,
		// Token: 0x04000353 RID: 851
		MAP_ANONYMOUS = 32,
		// Token: 0x04000354 RID: 852
		MAP_ANON = 32,
		// Token: 0x04000355 RID: 853
		MAP_GROWSDOWN = 256,
		// Token: 0x04000356 RID: 854
		MAP_DENYWRITE = 2048,
		// Token: 0x04000357 RID: 855
		MAP_EXECUTABLE = 4096,
		// Token: 0x04000358 RID: 856
		MAP_LOCKED = 8192,
		// Token: 0x04000359 RID: 857
		MAP_NORESERVE = 16384,
		// Token: 0x0400035A RID: 858
		MAP_POPULATE = 32768,
		// Token: 0x0400035B RID: 859
		MAP_NONBLOCK = 65536
	}
}
