using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003EF RID: 1007
	[Flags]
	internal enum FRAMEDATA_FLAGS : uint
	{
		// Token: 0x04000EE8 RID: 3816
		fHasSEH = 1U,
		// Token: 0x04000EE9 RID: 3817
		fHasEH = 2U,
		// Token: 0x04000EEA RID: 3818
		fIsFunctionStart = 4U
	}
}
