using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000074 RID: 116
	public class RfcModifyDNResponse : RfcLdapResult
	{
		// Token: 0x060003CD RID: 973 RVA: 0x000122E7 File Offset: 0x000104E7
		[CLSCompliant(false)]
		public RfcModifyDNResponse(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000122F2 File Offset: 0x000104F2
		public RfcModifyDNResponse(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
			: base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x060003CF RID: 975 RVA: 0x000122FF File Offset: 0x000104FF
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 13);
		}
	}
}
