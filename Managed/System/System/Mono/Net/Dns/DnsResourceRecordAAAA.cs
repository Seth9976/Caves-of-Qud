using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000C2 RID: 194
	internal class DnsResourceRecordAAAA : DnsResourceRecordIPAddress
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x0000B530 File Offset: 0x00009730
		internal DnsResourceRecordAAAA(DnsResourceRecord rr)
			: base(rr, 16)
		{
		}
	}
}
