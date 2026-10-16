using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000069 RID: 105
	public class RfcIntermediateResponse : Asn1Sequence, RfcResponse
	{
		// Token: 0x06000393 RID: 915 RVA: 0x00011A18 File Offset: 0x0000FC18
		[CLSCompliant(false)]
		public RfcIntermediateResponse(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
			this.m_responseNameIndex = (this.m_responseValueIndex = 0);
			int i;
			if (base.size() >= 3)
			{
				i = 3;
			}
			else
			{
				i = 0;
			}
			while (i < base.size())
			{
				Asn1Tagged asn1Tagged = (Asn1Tagged)base.get_Renamed(i);
				int tag = asn1Tagged.getIdentifier().Tag;
				if (tag != 0)
				{
					if (tag == 1)
					{
						base.set_Renamed(i, asn1Tagged.taggedValue());
						this.m_responseValueIndex = i;
					}
				}
				else
				{
					base.set_Renamed(i, new RfcLdapOID(((Asn1OctetString)asn1Tagged.taggedValue()).byteValue()));
					this.m_responseNameIndex = i;
				}
				i++;
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00011AB9 File Offset: 0x0000FCB9
		public Asn1Enumerated getResultCode()
		{
			if (base.size() > 3)
			{
				return (Asn1Enumerated)base.get_Renamed(0);
			}
			return null;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00011AD2 File Offset: 0x0000FCD2
		public RfcLdapDN getMatchedDN()
		{
			if (base.size() > 3)
			{
				return new RfcLdapDN(((Asn1OctetString)base.get_Renamed(1)).byteValue());
			}
			return null;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00011AF5 File Offset: 0x0000FCF5
		public RfcLdapString getErrorMessage()
		{
			if (base.size() > 3)
			{
				return new RfcLdapString(((Asn1OctetString)base.get_Renamed(2)).byteValue());
			}
			return null;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00011B18 File Offset: 0x0000FD18
		public RfcReferral getReferral()
		{
			if (base.size() <= 3)
			{
				return null;
			}
			return (RfcReferral)base.get_Renamed(3);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00011B31 File Offset: 0x0000FD31
		public RfcLdapOID getResponseName()
		{
			if (this.m_responseNameIndex < 0)
			{
				return null;
			}
			return (RfcLdapOID)base.get_Renamed(this.m_responseNameIndex);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00011B4F File Offset: 0x0000FD4F
		public Asn1OctetString getResponse()
		{
			if (this.m_responseValueIndex == 0)
			{
				return null;
			}
			return (Asn1OctetString)base.get_Renamed(this.m_responseValueIndex);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00011B6C File Offset: 0x0000FD6C
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 25);
		}

		// Token: 0x04000243 RID: 579
		public const int TAG_RESPONSE_NAME = 0;

		// Token: 0x04000244 RID: 580
		public const int TAG_RESPONSE = 1;

		// Token: 0x04000245 RID: 581
		private int m_referralIndex;

		// Token: 0x04000246 RID: 582
		private int m_responseNameIndex;

		// Token: 0x04000247 RID: 583
		private int m_responseValueIndex;
	}
}
