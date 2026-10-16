using System;

namespace Mono.Posix
{
	// Token: 0x02000097 RID: 151
	[Flags]
	[CLSCompliant(false)]
	[Obsolete("Use Mono.Unix.Native.OpenFlags")]
	public enum OpenFlags
	{
		// Token: 0x040004FC RID: 1276
		O_RDONLY = 0,
		// Token: 0x040004FD RID: 1277
		O_WRONLY = 1,
		// Token: 0x040004FE RID: 1278
		O_RDWR = 2,
		// Token: 0x040004FF RID: 1279
		O_CREAT = 4,
		// Token: 0x04000500 RID: 1280
		O_EXCL = 8,
		// Token: 0x04000501 RID: 1281
		O_NOCTTY = 16,
		// Token: 0x04000502 RID: 1282
		O_TRUNC = 32,
		// Token: 0x04000503 RID: 1283
		O_APPEND = 64,
		// Token: 0x04000504 RID: 1284
		O_NONBLOCK = 128,
		// Token: 0x04000505 RID: 1285
		O_SYNC = 256
	}
}
