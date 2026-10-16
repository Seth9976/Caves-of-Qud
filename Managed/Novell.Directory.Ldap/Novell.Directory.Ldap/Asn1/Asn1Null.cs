using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000D6 RID: 214
	public class Asn1Null : Asn1Object
	{
		// Token: 0x06000545 RID: 1349 RVA: 0x000172AB File Offset: 0x000154AB
		public Asn1Null()
			: base(Asn1Null.ID)
		{
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000172B8 File Offset: 0x000154B8
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x000172C2 File Offset: 0x000154C2
		public override string ToString()
		{
			return base.ToString() + "NULL: \"\"";
		}

		// Token: 0x040004AB RID: 1195
		public const int TAG = 5;

		// Token: 0x040004AC RID: 1196
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, false, 5);
	}
}
