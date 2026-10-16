using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200007B RID: 123
	public class RfcSaslCredentials : Asn1Sequence
	{
		// Token: 0x060003E1 RID: 993 RVA: 0x000123AC File Offset: 0x000105AC
		public RfcSaslCredentials(RfcLdapString mechanism)
			: this(mechanism, null)
		{
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000123B6 File Offset: 0x000105B6
		public RfcSaslCredentials(RfcLdapString mechanism, Asn1OctetString credentials)
			: base(2)
		{
			base.add(mechanism);
			if (credentials != null)
			{
				base.add(credentials);
			}
		}
	}
}
