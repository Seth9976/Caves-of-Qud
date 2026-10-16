using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000D7 RID: 215
	internal struct SepCodSym
	{
		// Token: 0x040003E8 RID: 1000
		internal uint parent;

		// Token: 0x040003E9 RID: 1001
		internal uint end;

		// Token: 0x040003EA RID: 1002
		internal uint length;

		// Token: 0x040003EB RID: 1003
		internal uint scf;

		// Token: 0x040003EC RID: 1004
		internal uint off;

		// Token: 0x040003ED RID: 1005
		internal uint offParent;

		// Token: 0x040003EE RID: 1006
		internal ushort sec;

		// Token: 0x040003EF RID: 1007
		internal ushort secParent;
	}
}
