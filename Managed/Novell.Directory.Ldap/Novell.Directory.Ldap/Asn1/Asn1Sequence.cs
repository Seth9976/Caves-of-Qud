using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000DA RID: 218
	public class Asn1Sequence : Asn1Structured
	{
		// Token: 0x0600055C RID: 1372 RVA: 0x00017513 File Offset: 0x00015713
		public Asn1Sequence()
			: base(Asn1Sequence.ID, 10)
		{
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00017522 File Offset: 0x00015722
		public Asn1Sequence(int size)
			: base(Asn1Sequence.ID, size)
		{
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00017530 File Offset: 0x00015730
		public Asn1Sequence(Asn1Object[] newContent, int size)
			: base(Asn1Sequence.ID, newContent, size)
		{
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001753F File Offset: 0x0001573F
		[CLSCompliant(false)]
		public Asn1Sequence(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(Asn1Sequence.ID)
		{
			base.decodeStructured(dec, in_Renamed, len);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00017555 File Offset: 0x00015755
		[CLSCompliant(false)]
		public override string ToString()
		{
			return base.toString("SEQUENCE: { ");
		}

		// Token: 0x040004B2 RID: 1202
		public const int TAG = 16;

		// Token: 0x040004B3 RID: 1203
		private static readonly Asn1Identifier ID = new Asn1Identifier(0, true, 16);
	}
}
