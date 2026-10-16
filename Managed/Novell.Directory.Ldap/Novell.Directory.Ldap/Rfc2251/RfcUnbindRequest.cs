using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000081 RID: 129
	public class RfcUnbindRequest : Asn1Null, RfcRequest
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x00012531 File Offset: 0x00010731
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, false, 2);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001253B File Offset: 0x0001073B
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			throw new LdapException("NO_DUP_REQUEST", new object[] { "unbind" }, 92, null);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00012558 File Offset: 0x00010758
		public string getRequestDN()
		{
			return null;
		}
	}
}
