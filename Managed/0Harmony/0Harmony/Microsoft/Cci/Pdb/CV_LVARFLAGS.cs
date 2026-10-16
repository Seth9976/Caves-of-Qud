using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003A2 RID: 930
	[Flags]
	internal enum CV_LVARFLAGS : ushort
	{
		// Token: 0x04000D61 RID: 3425
		fIsParam = 1,
		// Token: 0x04000D62 RID: 3426
		fAddrTaken = 2,
		// Token: 0x04000D63 RID: 3427
		fCompGenx = 4,
		// Token: 0x04000D64 RID: 3428
		fIsAggregate = 8,
		// Token: 0x04000D65 RID: 3429
		fIsAggregated = 16,
		// Token: 0x04000D66 RID: 3430
		fIsAliased = 32,
		// Token: 0x04000D67 RID: 3431
		fIsAlias = 64
	}
}
