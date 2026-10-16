using System;

namespace AiUnity.NLog.Core.Targets
{
	// Token: 0x0200022E RID: 558
	[Flags]
	public enum Win32FileAttributes
	{
		// Token: 0x04000D47 RID: 3399
		Nothing = 0,
		// Token: 0x04000D48 RID: 3400
		ReadOnly = 1,
		// Token: 0x04000D49 RID: 3401
		Hidden = 2,
		// Token: 0x04000D4A RID: 3402
		System = 4,
		// Token: 0x04000D4B RID: 3403
		Archive = 32,
		// Token: 0x04000D4C RID: 3404
		Device = 64,
		// Token: 0x04000D4D RID: 3405
		Normal = 128,
		// Token: 0x04000D4E RID: 3406
		Temporary = 256,
		// Token: 0x04000D4F RID: 3407
		SparseFile = 512,
		// Token: 0x04000D50 RID: 3408
		ReparsePoint = 1024,
		// Token: 0x04000D51 RID: 3409
		Compressed = 2048,
		// Token: 0x04000D52 RID: 3410
		NotContentIndexed = 8192,
		// Token: 0x04000D53 RID: 3411
		Encrypted = 16384,
		// Token: 0x04000D54 RID: 3412
		WriteThrough = -2147483648,
		// Token: 0x04000D55 RID: 3413
		NoBuffering = 536870912,
		// Token: 0x04000D56 RID: 3414
		DeleteOnClose = 67108864,
		// Token: 0x04000D57 RID: 3415
		PosixSemantics = 16777216
	}
}
