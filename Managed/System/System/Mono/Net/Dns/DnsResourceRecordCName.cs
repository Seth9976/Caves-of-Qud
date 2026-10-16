using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000C3 RID: 195
	internal class DnsResourceRecordCName : DnsResourceRecord
	{
		// Token: 0x060003C6 RID: 966 RVA: 0x0000B53C File Offset: 0x0000973C
		internal DnsResourceRecordCName(DnsResourceRecord rr)
		{
			base.CopyFrom(rr);
			int offset = rr.Data.Offset;
			this.cname = DnsPacket.ReadName(rr.Data.Array, ref offset);
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000B580 File Offset: 0x00009780
		public string CName
		{
			get
			{
				return this.cname;
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000B588 File Offset: 0x00009788
		public override string ToString()
		{
			return base.ToString() + " CNAME: " + this.cname.ToString();
		}

		// Token: 0x04000341 RID: 833
		private string cname;
	}
}
