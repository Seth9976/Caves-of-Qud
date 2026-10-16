using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000065 RID: 101
	public class RfcDelResponse : RfcLdapResult
	{
		// Token: 0x0600036D RID: 877 RVA: 0x000108A2 File Offset: 0x0000EAA2
		[CLSCompliant(false)]
		public RfcDelResponse(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000108AD File Offset: 0x0000EAAD
		public RfcDelResponse(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
			: base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x0600036F RID: 879 RVA: 0x000108BA File Offset: 0x0000EABA
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 11);
		}
	}
}
