using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003BB RID: 955
	internal struct FrameRelSym
	{
		// Token: 0x04000DCA RID: 3530
		internal int off;

		// Token: 0x04000DCB RID: 3531
		internal uint typind;

		// Token: 0x04000DCC RID: 3532
		internal uint offCod;

		// Token: 0x04000DCD RID: 3533
		internal ushort segCod;

		// Token: 0x04000DCE RID: 3534
		internal ushort flags;

		// Token: 0x04000DCF RID: 3535
		internal string name;
	}
}
