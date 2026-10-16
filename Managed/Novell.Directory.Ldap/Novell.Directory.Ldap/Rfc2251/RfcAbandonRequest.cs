using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000053 RID: 83
	internal class RfcAbandonRequest : RfcMessageID, RfcRequest
	{
		// Token: 0x06000322 RID: 802 RVA: 0x00010231 File Offset: 0x0000E431
		public RfcAbandonRequest(int msgId)
			: base(msgId)
		{
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0001023A File Offset: 0x0000E43A
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, false, 16);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00010245 File Offset: 0x0000E445
		public RfcRequest dupRequest(string base_Renamed, string filter, bool reference)
		{
			throw new LdapException("NO_DUP_REQUEST", new object[] { "Abandon" }, 92, null);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00010262 File Offset: 0x0000E462
		public string getRequestDN()
		{
			return null;
		}
	}
}
