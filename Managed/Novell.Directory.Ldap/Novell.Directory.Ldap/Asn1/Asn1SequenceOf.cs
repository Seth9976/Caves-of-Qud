using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000DB RID: 219
	public class Asn1SequenceOf : Asn1Structured
	{
		// Token: 0x06000562 RID: 1378 RVA: 0x00017572 File Offset: 0x00015772
		public Asn1SequenceOf()
			: base(Asn1SequenceOf.ID)
		{
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001757F File Offset: 0x0001577F
		public Asn1SequenceOf(int size)
			: base(Asn1SequenceOf.ID, size)
		{
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001758D File Offset: 0x0001578D
		public Asn1SequenceOf(Asn1Sequence sequence)
			: base(Asn1SequenceOf.ID, sequence.toArray(), sequence.size())
		{
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000175A6 File Offset: 0x000157A6
		[CLSCompliant(false)]
		public Asn1SequenceOf(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(Asn1SequenceOf.ID)
		{
			base.decodeStructured(dec, in_Renamed, len);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000175BC File Offset: 0x000157BC
		[CLSCompliant(false)]
		public override string ToString()
		{
			return base.toString("SEQUENCE OF: { ");
		}

		// Token: 0x040004B4 RID: 1204
		public const int TAG = 16;

		// Token: 0x040004B5 RID: 1205
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, true, 16);
	}
}
