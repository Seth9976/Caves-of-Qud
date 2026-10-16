using System;

namespace Mono.Btls
{
	// Token: 0x020000F0 RID: 240
	internal enum MonoBtlsSslError
	{
		// Token: 0x040003E5 RID: 997
		None,
		// Token: 0x040003E6 RID: 998
		Ssl,
		// Token: 0x040003E7 RID: 999
		WantRead,
		// Token: 0x040003E8 RID: 1000
		WantWrite,
		// Token: 0x040003E9 RID: 1001
		WantX509Lookup,
		// Token: 0x040003EA RID: 1002
		Syscall,
		// Token: 0x040003EB RID: 1003
		ZeroReturn,
		// Token: 0x040003EC RID: 1004
		WantConnect,
		// Token: 0x040003ED RID: 1005
		WantAccept,
		// Token: 0x040003EE RID: 1006
		WantChannelIdLookup,
		// Token: 0x040003EF RID: 1007
		PendingSession = 11,
		// Token: 0x040003F0 RID: 1008
		PendingCertificate,
		// Token: 0x040003F1 RID: 1009
		WantPrivateKeyOperation
	}
}
