using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200006E RID: 110
	public class RfcLdapString : Asn1OctetString
	{
		// Token: 0x060003B6 RID: 950 RVA: 0x00012017 File Offset: 0x00010217
		public RfcLdapString(string s)
			: base(s)
		{
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00012020 File Offset: 0x00010220
		[CLSCompliant(false)]
		public RfcLdapString(sbyte[] ba)
			: base(ba)
		{
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00012029 File Offset: 0x00010229
		[CLSCompliant(false)]
		public RfcLdapString(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}
	}
}
