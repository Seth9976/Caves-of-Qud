using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003C0 RID: 960
	[Flags]
	internal enum CV_PUBSYMFLAGS : uint
	{
		// Token: 0x04000DE2 RID: 3554
		fNone = 0U,
		// Token: 0x04000DE3 RID: 3555
		fCode = 1U,
		// Token: 0x04000DE4 RID: 3556
		fFunction = 2U,
		// Token: 0x04000DE5 RID: 3557
		fManaged = 4U,
		// Token: 0x04000DE6 RID: 3558
		fMSIL = 8U
	}
}
