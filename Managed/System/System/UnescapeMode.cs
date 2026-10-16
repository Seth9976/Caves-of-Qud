using System;

namespace System
{
	// Token: 0x0200015E RID: 350
	[Flags]
	internal enum UnescapeMode
	{
		// Token: 0x0400065D RID: 1629
		CopyOnly = 0,
		// Token: 0x0400065E RID: 1630
		Escape = 1,
		// Token: 0x0400065F RID: 1631
		Unescape = 2,
		// Token: 0x04000660 RID: 1632
		EscapeUnescape = 3,
		// Token: 0x04000661 RID: 1633
		V1ToStringFlag = 4,
		// Token: 0x04000662 RID: 1634
		UnescapeAll = 8,
		// Token: 0x04000663 RID: 1635
		UnescapeAllOrThrow = 24
	}
}
