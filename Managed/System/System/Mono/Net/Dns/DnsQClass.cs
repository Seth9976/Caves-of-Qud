using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000BB RID: 187
	internal enum DnsQClass : ushort
	{
		// Token: 0x040002D3 RID: 723
		Internet = 1,
		// Token: 0x040002D4 RID: 724
		IN = 1,
		// Token: 0x040002D5 RID: 725
		CSNET,
		// Token: 0x040002D6 RID: 726
		CS = 2,
		// Token: 0x040002D7 RID: 727
		CHAOS,
		// Token: 0x040002D8 RID: 728
		CH = 3,
		// Token: 0x040002D9 RID: 729
		Hesiod,
		// Token: 0x040002DA RID: 730
		HS = 4,
		// Token: 0x040002DB RID: 731
		None = 254,
		// Token: 0x040002DC RID: 732
		Any
	}
}
