using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000D4 RID: 212
	public class Asn1Integer : Asn1Numeric
	{
		// Token: 0x06000539 RID: 1337 RVA: 0x000170E0 File Offset: 0x000152E0
		public Asn1Integer(int content)
			: base(Asn1Integer.ID, content)
		{
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000170EE File Offset: 0x000152EE
		public Asn1Integer(long content)
			: base(Asn1Integer.ID, content)
		{
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x000170FC File Offset: 0x000152FC
		[CLSCompliant(false)]
		public Asn1Integer(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(Asn1Integer.ID, (long)dec.decodeNumeric(in_Renamed, len))
		{
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00017116 File Offset: 0x00015316
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00017120 File Offset: 0x00015320
		public override string ToString()
		{
			return base.ToString() + "INTEGER: " + base.longValue().ToString();
		}

		// Token: 0x040004A7 RID: 1191
		public const int TAG = 2;

		// Token: 0x040004A8 RID: 1192
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, false, 2);
	}
}
