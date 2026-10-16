using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000065 RID: 101
	internal struct FrameData
	{
		// Token: 0x040001D0 RID: 464
		internal uint ulRvaStart;

		// Token: 0x040001D1 RID: 465
		internal uint cbBlock;

		// Token: 0x040001D2 RID: 466
		internal uint cbLocals;

		// Token: 0x040001D3 RID: 467
		internal uint cbParams;

		// Token: 0x040001D4 RID: 468
		internal uint cbStkMax;

		// Token: 0x040001D5 RID: 469
		internal uint frameFunc;

		// Token: 0x040001D6 RID: 470
		internal ushort cbProlog;

		// Token: 0x040001D7 RID: 471
		internal ushort cbSavedRegs;

		// Token: 0x040001D8 RID: 472
		internal uint flags;
	}
}
