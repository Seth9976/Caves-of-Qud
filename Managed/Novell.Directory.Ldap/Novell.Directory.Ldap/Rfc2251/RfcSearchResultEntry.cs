using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200007E RID: 126
	public class RfcSearchResultEntry : Asn1Sequence
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x000124CB File Offset: 0x000106CB
		public virtual Asn1OctetString ObjectName
		{
			get
			{
				return (Asn1OctetString)base.get_Renamed(0);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x000124D9 File Offset: 0x000106D9
		public virtual Asn1Sequence Attributes
		{
			get
			{
				return (Asn1Sequence)base.get_Renamed(1);
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000124E7 File Offset: 0x000106E7
		[CLSCompliant(false)]
		public RfcSearchResultEntry(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000124F2 File Offset: 0x000106F2
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 4);
		}
	}
}
