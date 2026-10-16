using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200004B RID: 75
	[Map]
	[CLSCompliant(false)]
	public enum UnixSocketType
	{
		// Token: 0x0400037D RID: 893
		SOCK_STREAM = 1,
		// Token: 0x0400037E RID: 894
		SOCK_DGRAM,
		// Token: 0x0400037F RID: 895
		SOCK_RAW,
		// Token: 0x04000380 RID: 896
		SOCK_RDM,
		// Token: 0x04000381 RID: 897
		SOCK_SEQPACKET,
		// Token: 0x04000382 RID: 898
		SOCK_DCCP,
		// Token: 0x04000383 RID: 899
		SOCK_PACKET = 10
	}
}
