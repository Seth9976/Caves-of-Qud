using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003DF RID: 991
	[Flags]
	internal enum EXPORTSYM_FLAGS : ushort
	{
		// Token: 0x04000EAB RID: 3755
		fConstant = 1,
		// Token: 0x04000EAC RID: 3756
		fData = 2,
		// Token: 0x04000EAD RID: 3757
		fPrivate = 4,
		// Token: 0x04000EAE RID: 3758
		fNoName = 8,
		// Token: 0x04000EAF RID: 3759
		fOrdinal = 16,
		// Token: 0x04000EB0 RID: 3760
		fForwarder = 32
	}
}
