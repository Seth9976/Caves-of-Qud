using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000355 RID: 853
	[Flags]
	internal enum LeafPointerAttr : uint
	{
		// Token: 0x04000C2A RID: 3114
		ptrtype = 31U,
		// Token: 0x04000C2B RID: 3115
		ptrmode = 224U,
		// Token: 0x04000C2C RID: 3116
		isflat32 = 256U,
		// Token: 0x04000C2D RID: 3117
		isvolatile = 512U,
		// Token: 0x04000C2E RID: 3118
		isconst = 1024U,
		// Token: 0x04000C2F RID: 3119
		isunaligned = 2048U,
		// Token: 0x04000C30 RID: 3120
		isrestrict = 4096U
	}
}
