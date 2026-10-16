using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200005D RID: 93
	public class RfcAuthenticationChoice : Asn1Choice
	{
		// Token: 0x0600033B RID: 827 RVA: 0x000103C6 File Offset: 0x0000E5C6
		public RfcAuthenticationChoice(Asn1Tagged choice)
			: base(choice)
		{
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000103CF File Offset: 0x0000E5CF
		[CLSCompliant(false)]
		public RfcAuthenticationChoice(string mechanism, sbyte[] credentials)
			: base(new Asn1Tagged(new Asn1Identifier(2, true, 3), new RfcSaslCredentials(new RfcLdapString(mechanism), (credentials != null) ? new Asn1OctetString(credentials) : null), false))
		{
		}
	}
}
