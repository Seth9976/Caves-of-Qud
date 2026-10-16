using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000DC RID: 220
	public class Asn1Set : Asn1Structured
	{
		// Token: 0x06000568 RID: 1384 RVA: 0x000175D9 File Offset: 0x000157D9
		public Asn1Set()
			: base(Asn1Set.ID)
		{
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x000175E6 File Offset: 0x000157E6
		public Asn1Set(int size)
			: base(Asn1Set.ID, size)
		{
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000175F4 File Offset: 0x000157F4
		[CLSCompliant(false)]
		public Asn1Set(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(Asn1Set.ID)
		{
			base.decodeStructured(dec, in_Renamed, len);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001760A File Offset: 0x0001580A
		[CLSCompliant(false)]
		public override string ToString()
		{
			return base.toString("SET: { ");
		}

		// Token: 0x040004B6 RID: 1206
		public const int TAG = 17;

		// Token: 0x040004B7 RID: 1207
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, true, 17);
	}
}
