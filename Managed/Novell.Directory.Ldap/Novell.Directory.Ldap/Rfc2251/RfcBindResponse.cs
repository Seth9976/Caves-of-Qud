using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200005F RID: 95
	public class RfcBindResponse : Asn1Sequence, RfcResponse
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600034A RID: 842 RVA: 0x000104D4 File Offset: 0x0000E6D4
		public virtual Asn1OctetString ServerSaslCreds
		{
			get
			{
				if (base.size() == 5)
				{
					return (Asn1OctetString)((Asn1Tagged)base.get_Renamed(4)).taggedValue();
				}
				if (base.size() == 4)
				{
					Asn1Object asn1Object = base.get_Renamed(3);
					if (asn1Object is Asn1Tagged)
					{
						return (Asn1OctetString)((Asn1Tagged)asn1Object).taggedValue();
					}
				}
				return null;
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001052C File Offset: 0x0000E72C
		[CLSCompliant(false)]
		public RfcBindResponse(Asn1Decoder dec, Stream in_Renamed, int len)
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

		// Token: 0x0600034C RID: 844 RVA: 0x00010594 File Offset: 0x0000E794
		public Asn1Enumerated getResultCode()
		{
			return (Asn1Enumerated)base.get_Renamed(0);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x000105A2 File Offset: 0x0000E7A2
		public RfcLdapDN getMatchedDN()
		{
			return new RfcLdapDN(((Asn1OctetString)base.get_Renamed(1)).byteValue());
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000105BA File Offset: 0x0000E7BA
		public RfcLdapString getErrorMessage()
		{
			return new RfcLdapString(((Asn1OctetString)base.get_Renamed(2)).byteValue());
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000105D4 File Offset: 0x0000E7D4
		public RfcReferral getReferral()
		{
			if (base.size() > 3)
			{
				Asn1Object asn1Object = base.get_Renamed(3);
				if (asn1Object is RfcReferral)
				{
					return (RfcReferral)asn1Object;
				}
			}
			return null;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00010602 File Offset: 0x0000E802
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 1);
		}
	}
}
