using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x0200003B RID: 59
	[Flags]
	internal enum CV_LVARFLAGS : ushort
	{
		// Token: 0x040000D2 RID: 210
		fIsParam = 1,
		// Token: 0x040000D3 RID: 211
		fAddrTaken = 2,
		// Token: 0x040000D4 RID: 212
		fCompGenx = 4,
		// Token: 0x040000D5 RID: 213
		fIsAggregate = 8,
		// Token: 0x040000D6 RID: 214
		fIsAggregated = 16,
		// Token: 0x040000D7 RID: 215
		fIsAliased = 32,
		// Token: 0x040000D8 RID: 216
		fIsAlias = 64
	}
}
