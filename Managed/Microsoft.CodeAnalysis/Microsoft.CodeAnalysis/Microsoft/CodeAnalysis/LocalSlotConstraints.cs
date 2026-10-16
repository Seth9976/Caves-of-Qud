using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200019D RID: 413
	[Flags]
	internal enum LocalSlotConstraints : byte
	{
		// Token: 0x0400051A RID: 1306
		None = 0,
		// Token: 0x0400051B RID: 1307
		ByRef = 1,
		// Token: 0x0400051C RID: 1308
		Pinned = 2
	}
}
