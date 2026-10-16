using System;

namespace Microsoft.CodeAnalysis.RuntimeMembers
{
	// Token: 0x020002C8 RID: 712
	[Flags]
	internal enum MemberFlags : byte
	{
		// Token: 0x04000D79 RID: 3449
		Method = 1,
		// Token: 0x04000D7A RID: 3450
		Field = 2,
		// Token: 0x04000D7B RID: 3451
		Constructor = 4,
		// Token: 0x04000D7C RID: 3452
		PropertyGet = 8,
		// Token: 0x04000D7D RID: 3453
		Property = 16,
		// Token: 0x04000D7E RID: 3454
		KindMask = 31,
		// Token: 0x04000D7F RID: 3455
		Static = 32,
		// Token: 0x04000D80 RID: 3456
		Virtual = 64
	}
}
