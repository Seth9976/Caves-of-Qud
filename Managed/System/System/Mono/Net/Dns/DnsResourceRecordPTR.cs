using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000C5 RID: 197
	internal class DnsResourceRecordPTR : DnsResourceRecord
	{
		// Token: 0x060003CC RID: 972 RVA: 0x0000B620 File Offset: 0x00009820
		internal DnsResourceRecordPTR(DnsResourceRecord rr)
		{
			base.CopyFrom(rr);
			int offset = rr.Data.Offset;
			this.dname = DnsPacket.ReadName(rr.Data.Array, ref offset);
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000B664 File Offset: 0x00009864
		public string DName
		{
			get
			{
				return this.dname;
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000B66C File Offset: 0x0000986C
		public override string ToString()
		{
			return base.ToString() + " DNAME: " + this.dname.ToString();
		}

		// Token: 0x04000343 RID: 835
		private string dname;
	}
}
