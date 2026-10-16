using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000C1 RID: 193
	internal class DnsResourceRecordA : DnsResourceRecordIPAddress
	{
		// Token: 0x060003C4 RID: 964 RVA: 0x0000B526 File Offset: 0x00009726
		internal DnsResourceRecordA(DnsResourceRecord rr)
			: base(rr, 4)
		{
		}
	}
}
