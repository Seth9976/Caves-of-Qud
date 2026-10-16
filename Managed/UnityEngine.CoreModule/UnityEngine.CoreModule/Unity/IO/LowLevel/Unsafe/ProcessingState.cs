using System;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x0200008A RID: 138
	[NativeHeader("Runtime/File/AsyncReadManagerMetrics.h")]
	public enum ProcessingState
	{
		// Token: 0x04000217 RID: 535
		Unknown,
		// Token: 0x04000218 RID: 536
		InQueue,
		// Token: 0x04000219 RID: 537
		Reading,
		// Token: 0x0400021A RID: 538
		Completed,
		// Token: 0x0400021B RID: 539
		Failed,
		// Token: 0x0400021C RID: 540
		Canceled
	}
}
