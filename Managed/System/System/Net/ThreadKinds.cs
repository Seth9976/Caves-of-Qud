using System;

namespace System.Net
{
	// Token: 0x02000632 RID: 1586
	[Flags]
	internal enum ThreadKinds
	{
		// Token: 0x04001D5E RID: 7518
		Unknown = 0,
		// Token: 0x04001D5F RID: 7519
		User = 1,
		// Token: 0x04001D60 RID: 7520
		System = 2,
		// Token: 0x04001D61 RID: 7521
		Sync = 4,
		// Token: 0x04001D62 RID: 7522
		Async = 8,
		// Token: 0x04001D63 RID: 7523
		Timer = 16,
		// Token: 0x04001D64 RID: 7524
		CompletionPort = 32,
		// Token: 0x04001D65 RID: 7525
		Worker = 64,
		// Token: 0x04001D66 RID: 7526
		Finalization = 128,
		// Token: 0x04001D67 RID: 7527
		Other = 256,
		// Token: 0x04001D68 RID: 7528
		OwnerMask = 3,
		// Token: 0x04001D69 RID: 7529
		SyncMask = 12,
		// Token: 0x04001D6A RID: 7530
		SourceMask = 496,
		// Token: 0x04001D6B RID: 7531
		SafeSources = 352,
		// Token: 0x04001D6C RID: 7532
		ThreadPool = 96
	}
}
