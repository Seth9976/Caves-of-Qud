using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000B9 RID: 185
	internal enum BoundNullCoalescingOperatorResultKind
	{
		// Token: 0x04000555 RID: 1365
		NoCommonType,
		// Token: 0x04000556 RID: 1366
		LeftType,
		// Token: 0x04000557 RID: 1367
		LeftUnwrappedType,
		// Token: 0x04000558 RID: 1368
		RightType,
		// Token: 0x04000559 RID: 1369
		LeftUnwrappedRightType,
		// Token: 0x0400055A RID: 1370
		RightDynamicType
	}
}
