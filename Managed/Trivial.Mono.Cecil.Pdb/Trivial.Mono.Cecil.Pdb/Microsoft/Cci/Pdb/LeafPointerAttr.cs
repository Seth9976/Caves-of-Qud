using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000099 RID: 153
	[Flags]
	internal enum LeafPointerAttr : uint
	{
		// Token: 0x040002E6 RID: 742
		ptrtype = 31U,
		// Token: 0x040002E7 RID: 743
		ptrmode = 224U,
		// Token: 0x040002E8 RID: 744
		isflat32 = 256U,
		// Token: 0x040002E9 RID: 745
		isvolatile = 512U,
		// Token: 0x040002EA RID: 746
		isconst = 1024U,
		// Token: 0x040002EB RID: 747
		isunaligned = 2048U,
		// Token: 0x040002EC RID: 748
		isrestrict = 4096U
	}
}
