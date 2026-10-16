using System;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000DD RID: 221
	public class Asn1SetOf : Asn1Structured
	{
		// Token: 0x0600056D RID: 1389 RVA: 0x00017627 File Offset: 0x00015827
		public Asn1SetOf()
			: base(Asn1SetOf.ID)
		{
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00017634 File Offset: 0x00015834
		public Asn1SetOf(int size)
			: base(Asn1SetOf.ID, size)
		{
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00017642 File Offset: 0x00015842
		public Asn1SetOf(Asn1Set set_Renamed)
			: base(Asn1SetOf.ID, set_Renamed.toArray(), set_Renamed.size())
		{
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001765B File Offset: 0x0001585B
		[CLSCompliant(false)]
		public override string ToString()
		{
			return base.toString("SET OF: { ");
		}

		// Token: 0x040004B8 RID: 1208
		public const int TAG = 17;

		// Token: 0x040004B9 RID: 1209
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, true, 17);
	}
}
