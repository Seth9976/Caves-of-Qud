using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000066 RID: 102
	[Flags]
	internal enum FRAMEDATA_FLAGS : uint
	{
		// Token: 0x040001DA RID: 474
		fHasSEH = 1U,
		// Token: 0x040001DB RID: 475
		fHasEH = 2U,
		// Token: 0x040001DC RID: 476
		fIsFunctionStart = 4U
	}
}
