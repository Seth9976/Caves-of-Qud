using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000D2 RID: 210
	public class Asn1Enumerated : Asn1Numeric
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x00016E95 File Offset: 0x00015095
		public Asn1Enumerated(int content)
			: base(Asn1Enumerated.ID, content)
		{
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00016EA3 File Offset: 0x000150A3
		public Asn1Enumerated(long content)
			: base(Asn1Enumerated.ID, content)
		{
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00016EB1 File Offset: 0x000150B1
		[CLSCompliant(false)]
		public Asn1Enumerated(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(Asn1Enumerated.ID, (long)dec.decodeNumeric(in_Renamed, len))
		{
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00016ECB File Offset: 0x000150CB
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00016ED8 File Offset: 0x000150D8
		public override string ToString()
		{
			return base.ToString() + "ENUMERATED: " + base.longValue().ToString();
		}

		// Token: 0x0400049D RID: 1181
		public const int TAG = 10;

		// Token: 0x0400049E RID: 1182
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, false, 10);
	}
}
