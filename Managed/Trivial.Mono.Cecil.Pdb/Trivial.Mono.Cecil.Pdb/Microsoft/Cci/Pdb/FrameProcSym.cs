using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000067 RID: 103
	internal struct FrameProcSym
	{
		// Token: 0x040001DD RID: 477
		internal uint cbFrame;

		// Token: 0x040001DE RID: 478
		internal uint cbPad;

		// Token: 0x040001DF RID: 479
		internal uint offPad;

		// Token: 0x040001E0 RID: 480
		internal uint cbSaveRegs;

		// Token: 0x040001E1 RID: 481
		internal uint offExHdlr;

		// Token: 0x040001E2 RID: 482
		internal ushort secExHdlr;

		// Token: 0x040001E3 RID: 483
		internal uint flags;
	}
}
