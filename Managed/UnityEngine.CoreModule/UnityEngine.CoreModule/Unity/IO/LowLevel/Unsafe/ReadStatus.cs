using System;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x02000085 RID: 133
	public enum ReadStatus
	{
		// Token: 0x0400020A RID: 522
		Complete,
		// Token: 0x0400020B RID: 523
		InProgress,
		// Token: 0x0400020C RID: 524
		Failed,
		// Token: 0x0400020D RID: 525
		Truncated = 4,
		// Token: 0x0400020E RID: 526
		Canceled
	}
}
