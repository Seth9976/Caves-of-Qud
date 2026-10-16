using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003D7 RID: 983
	internal struct FrameProcSym
	{
		// Token: 0x04000E7E RID: 3710
		internal uint cbFrame;

		// Token: 0x04000E7F RID: 3711
		internal uint cbPad;

		// Token: 0x04000E80 RID: 3712
		internal uint offPad;

		// Token: 0x04000E81 RID: 3713
		internal uint cbSaveRegs;

		// Token: 0x04000E82 RID: 3714
		internal uint offExHdlr;

		// Token: 0x04000E83 RID: 3715
		internal ushort secExHdlr;

		// Token: 0x04000E84 RID: 3716
		internal uint flags;
	}
}
