using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000B9 RID: 185
	internal enum DnsOpCode : byte
	{
		// Token: 0x040002CA RID: 714
		Query,
		// Token: 0x040002CB RID: 715
		[Obsolete]
		IQuery,
		// Token: 0x040002CC RID: 716
		Status,
		// Token: 0x040002CD RID: 717
		Notify = 4,
		// Token: 0x040002CE RID: 718
		Update
	}
}
