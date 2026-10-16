using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000050 RID: 80
	[Flags]
	[Map]
	[CLSCompliant(false)]
	public enum MessageFlags
	{
		// Token: 0x040003FB RID: 1019
		MSG_OOB = 1,
		// Token: 0x040003FC RID: 1020
		MSG_PEEK = 2,
		// Token: 0x040003FD RID: 1021
		MSG_DONTROUTE = 4,
		// Token: 0x040003FE RID: 1022
		MSG_CTRUNC = 8,
		// Token: 0x040003FF RID: 1023
		MSG_PROXY = 16,
		// Token: 0x04000400 RID: 1024
		MSG_TRUNC = 32,
		// Token: 0x04000401 RID: 1025
		MSG_DONTWAIT = 64,
		// Token: 0x04000402 RID: 1026
		MSG_EOR = 128,
		// Token: 0x04000403 RID: 1027
		MSG_WAITALL = 256,
		// Token: 0x04000404 RID: 1028
		MSG_FIN = 512,
		// Token: 0x04000405 RID: 1029
		MSG_SYN = 1024,
		// Token: 0x04000406 RID: 1030
		MSG_CONFIRM = 2048,
		// Token: 0x04000407 RID: 1031
		MSG_RST = 4096,
		// Token: 0x04000408 RID: 1032
		MSG_ERRQUEUE = 8192,
		// Token: 0x04000409 RID: 1033
		MSG_NOSIGNAL = 16384,
		// Token: 0x0400040A RID: 1034
		MSG_MORE = 32768,
		// Token: 0x0400040B RID: 1035
		MSG_WAITFORONE = 65536,
		// Token: 0x0400040C RID: 1036
		MSG_FASTOPEN = 536870912,
		// Token: 0x0400040D RID: 1037
		MSG_CMSG_CLOEXEC = 1073741824
	}
}
