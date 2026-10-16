using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200006C RID: 108
	public class RfcLdapOID : Asn1OctetString
	{
		// Token: 0x060003AD RID: 941 RVA: 0x00011F0E File Offset: 0x0001010E
		public RfcLdapOID(string s)
			: base(s)
		{
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00011F17 File Offset: 0x00010117
		[CLSCompliant(false)]
		public RfcLdapOID(sbyte[] s)
			: base(s)
		{
		}
	}
}
