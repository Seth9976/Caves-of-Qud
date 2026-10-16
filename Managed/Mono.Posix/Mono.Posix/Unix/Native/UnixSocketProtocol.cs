using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200004D RID: 77
	[Map]
	[CLSCompliant(false)]
	public enum UnixSocketProtocol
	{
		// Token: 0x04000388 RID: 904
		IPPROTO_ICMP = 1,
		// Token: 0x04000389 RID: 905
		IPPROTO_IGMP,
		// Token: 0x0400038A RID: 906
		IPPROTO_IPIP = 4,
		// Token: 0x0400038B RID: 907
		IPPROTO_TCP = 6,
		// Token: 0x0400038C RID: 908
		IPPROTO_EGP = 8,
		// Token: 0x0400038D RID: 909
		IPPROTO_PUP = 12,
		// Token: 0x0400038E RID: 910
		IPPROTO_UDP = 17,
		// Token: 0x0400038F RID: 911
		IPPROTO_IDP = 22,
		// Token: 0x04000390 RID: 912
		IPPROTO_TP = 29,
		// Token: 0x04000391 RID: 913
		IPPROTO_DCCP = 33,
		// Token: 0x04000392 RID: 914
		IPPROTO_IPV6 = 41,
		// Token: 0x04000393 RID: 915
		IPPROTO_RSVP = 46,
		// Token: 0x04000394 RID: 916
		IPPROTO_GRE,
		// Token: 0x04000395 RID: 917
		IPPROTO_ESP = 50,
		// Token: 0x04000396 RID: 918
		IPPROTO_AH,
		// Token: 0x04000397 RID: 919
		IPPROTO_MTP = 92,
		// Token: 0x04000398 RID: 920
		IPPROTO_BEETPH = 94,
		// Token: 0x04000399 RID: 921
		IPPROTO_ENCAP = 98,
		// Token: 0x0400039A RID: 922
		IPPROTO_PIM = 103,
		// Token: 0x0400039B RID: 923
		IPPROTO_COMP = 108,
		// Token: 0x0400039C RID: 924
		IPPROTO_SCTP = 132,
		// Token: 0x0400039D RID: 925
		IPPROTO_UDPLITE = 136,
		// Token: 0x0400039E RID: 926
		IPPROTO_RAW = 255,
		// Token: 0x0400039F RID: 927
		IPPROTO_IP = 1024,
		// Token: 0x040003A0 RID: 928
		SOL_SOCKET = 2048
	}
}
