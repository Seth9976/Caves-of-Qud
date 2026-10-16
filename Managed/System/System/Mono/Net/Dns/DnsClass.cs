using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000B7 RID: 183
	internal enum DnsClass : ushort
	{
		// Token: 0x040002BF RID: 703
		Internet = 1,
		// Token: 0x040002C0 RID: 704
		IN = 1,
		// Token: 0x040002C1 RID: 705
		CSNET,
		// Token: 0x040002C2 RID: 706
		CS = 2,
		// Token: 0x040002C3 RID: 707
		CHAOS,
		// Token: 0x040002C4 RID: 708
		CH = 3,
		// Token: 0x040002C5 RID: 709
		Hesiod,
		// Token: 0x040002C6 RID: 710
		HS = 4
	}
}
