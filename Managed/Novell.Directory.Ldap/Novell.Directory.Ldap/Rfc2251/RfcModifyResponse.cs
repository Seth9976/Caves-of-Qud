using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000076 RID: 118
	public class RfcModifyResponse : RfcLdapResult
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x00012376 File Offset: 0x00010576
		[CLSCompliant(false)]
		public RfcModifyResponse(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00012381 File Offset: 0x00010581
		public RfcModifyResponse(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
			: base(resultCode, matchedDN, errorMessage, referral)
		{
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001238E File Offset: 0x0001058E
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 7);
		}
	}
}
