using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x02000096 RID: 150
	public class PartitionEntryCountResponse : LdapExtendedResponse
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x000137D4 File Offset: 0x000119D4
		public virtual int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000137DC File Offset: 0x000119DC
		public PartitionEntryCountResponse(RfcLdapMessage rfcMessage)
			: base(rfcMessage)
		{
			if (this.ResultCode != 0)
			{
				this.count = -1;
				return;
			}
			sbyte[] value = this.Value;
			if (value == null)
			{
				throw new IOException("No returned value");
			}
			LBERDecoder lberdecoder = new LBERDecoder();
			if (lberdecoder == null)
			{
				throw new IOException("Decoding error");
			}
			Asn1Integer asn1Integer = (Asn1Integer)lberdecoder.decode(value);
			if (asn1Integer == null)
			{
				throw new IOException("Decoding error");
			}
			this.count = asn1Integer.intValue();
		}

		// Token: 0x040002B0 RID: 688
		private int count;
	}
}
