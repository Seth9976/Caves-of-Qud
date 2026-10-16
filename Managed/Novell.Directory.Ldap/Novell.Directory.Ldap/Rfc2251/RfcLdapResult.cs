using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200006D RID: 109
	public class RfcLdapResult : Asn1Sequence, RfcResponse
	{
		// Token: 0x060003AF RID: 943 RVA: 0x00011F20 File Offset: 0x00010120
		public RfcLdapResult(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage)
			: this(resultCode, matchedDN, errorMessage, null)
		{
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00011F2C File Offset: 0x0001012C
		public RfcLdapResult(Asn1Enumerated resultCode, RfcLdapDN matchedDN, RfcLdapString errorMessage, RfcReferral referral)
			: base(4)
		{
			base.add(resultCode);
			base.add(matchedDN);
			base.add(errorMessage);
			if (referral != null)
			{
				base.add(referral);
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00011F58 File Offset: 0x00010158
		[CLSCompliant(false)]
		public RfcLdapResult(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
			if (base.size() > 3)
			{
				Asn1Tagged asn1Tagged = (Asn1Tagged)base.get_Renamed(3);
				if (asn1Tagged.getIdentifier().Tag == 3)
				{
					sbyte[] array = ((Asn1OctetString)asn1Tagged.taggedValue()).byteValue();
					MemoryStream memoryStream = new MemoryStream(SupportClass.ToByteArray(array));
					base.set_Renamed(3, new RfcReferral(dec, memoryStream, array.Length));
				}
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00011FC0 File Offset: 0x000101C0
		public Asn1Enumerated getResultCode()
		{
			return (Asn1Enumerated)base.get_Renamed(0);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00011FCE File Offset: 0x000101CE
		public RfcLdapDN getMatchedDN()
		{
			return new RfcLdapDN(((Asn1OctetString)base.get_Renamed(1)).byteValue());
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00011FE6 File Offset: 0x000101E6
		public RfcLdapString getErrorMessage()
		{
			return new RfcLdapString(((Asn1OctetString)base.get_Renamed(2)).byteValue());
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00011FFE File Offset: 0x000101FE
		public RfcReferral getReferral()
		{
			if (base.size() <= 3)
			{
				return null;
			}
			return (RfcReferral)base.get_Renamed(3);
		}

		// Token: 0x0400024B RID: 587
		public const int REFERRAL = 3;
	}
}
