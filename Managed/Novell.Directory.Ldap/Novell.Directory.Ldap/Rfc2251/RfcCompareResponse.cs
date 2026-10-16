using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000061 RID: 97
	public class RfcCompareResponse : RfcLdapResult
	{
		// Token: 0x06000357 RID: 855 RVA: 0x0001068C File Offset: 0x0000E88C
		[CLSCompliant(false)]
		public RfcCompareResponse(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00010697 File Offset: 0x0000E897
		public RfcCompareResponse(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
			: base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000106A4 File Offset: 0x0000E8A4
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 15);
		}
	}
}
