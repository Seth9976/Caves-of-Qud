using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200007D RID: 125
	public class RfcSearchResultDone : RfcLdapResult
	{
		// Token: 0x060003E8 RID: 1000 RVA: 0x000124A9 File Offset: 0x000106A9
		[CLSCompliant(false)]
		public RfcSearchResultDone(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x000124B4 File Offset: 0x000106B4
		public RfcSearchResultDone(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
			: base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000124C1 File Offset: 0x000106C1
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 5);
		}
	}
}
