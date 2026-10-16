using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003AC RID: 940
	internal struct AttrManyRegSym
	{
		// Token: 0x04000D8C RID: 3468
		internal uint typind;

		// Token: 0x04000D8D RID: 3469
		internal uint offCod;

		// Token: 0x04000D8E RID: 3470
		internal ushort segCod;

		// Token: 0x04000D8F RID: 3471
		internal ushort flags;

		// Token: 0x04000D90 RID: 3472
		internal byte count;

		// Token: 0x04000D91 RID: 3473
		internal byte[] reg;

		// Token: 0x04000D92 RID: 3474
		internal string name;
	}
}
