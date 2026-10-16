using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000055 RID: 85
	public class RfcAddResponse : RfcLdapResult
	{
		// Token: 0x0600032C RID: 812 RVA: 0x000102D1 File Offset: 0x0000E4D1
		[CLSCompliant(false)]
		public RfcAddResponse(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000102DC File Offset: 0x0000E4DC
		public RfcAddResponse(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
			: base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000102E9 File Offset: 0x0000E4E9
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 9);
		}
	}
}
