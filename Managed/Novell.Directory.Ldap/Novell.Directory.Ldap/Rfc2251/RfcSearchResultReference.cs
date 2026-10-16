using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200007F RID: 127
	public class RfcSearchResultReference : Asn1SequenceOf
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x000124FC File Offset: 0x000106FC
		[CLSCompliant(false)]
		public RfcSearchResultReference(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00012507 File Offset: 0x00010707
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 19);
		}
	}
}
