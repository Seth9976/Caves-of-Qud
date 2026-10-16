using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003F0 RID: 1008
	internal struct FrameData
	{
		// Token: 0x04000EEB RID: 3819
		internal uint ulRvaStart;

		// Token: 0x04000EEC RID: 3820
		internal uint cbBlock;

		// Token: 0x04000EED RID: 3821
		internal uint cbLocals;

		// Token: 0x04000EEE RID: 3822
		internal uint cbParams;

		// Token: 0x04000EEF RID: 3823
		internal uint cbStkMax;

		// Token: 0x04000EF0 RID: 3824
		internal uint frameFunc;

		// Token: 0x04000EF1 RID: 3825
		internal ushort cbProlog;

		// Token: 0x04000EF2 RID: 3826
		internal ushort cbSavedRegs;

		// Token: 0x04000EF3 RID: 3827
		internal uint flags;
	}
}
