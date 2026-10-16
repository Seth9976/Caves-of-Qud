using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x0200004B RID: 75
	[Flags]
	internal enum CV_PUBSYMFLAGS : uint
	{
		// Token: 0x0400012E RID: 302
		fNone = 0U,
		// Token: 0x0400012F RID: 303
		fCode = 1U,
		// Token: 0x04000130 RID: 304
		fFunction = 2U,
		// Token: 0x04000131 RID: 305
		fManaged = 4U,
		// Token: 0x04000132 RID: 306
		fMSIL = 8U
	}
}
