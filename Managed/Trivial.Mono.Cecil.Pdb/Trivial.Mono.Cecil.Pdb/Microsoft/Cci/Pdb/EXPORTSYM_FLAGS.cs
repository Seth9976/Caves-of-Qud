using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000062 RID: 98
	[Flags]
	internal enum EXPORTSYM_FLAGS : ushort
	{
		// Token: 0x040001BC RID: 444
		fConstant = 1,
		// Token: 0x040001BD RID: 445
		fData = 2,
		// Token: 0x040001BE RID: 446
		fPrivate = 4,
		// Token: 0x040001BF RID: 447
		fNoName = 8,
		// Token: 0x040001C0 RID: 448
		fOrdinal = 16,
		// Token: 0x040001C1 RID: 449
		fForwarder = 32
	}
}
