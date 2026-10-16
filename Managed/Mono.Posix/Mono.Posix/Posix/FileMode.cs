using System;

namespace Mono.Posix
{
	// Token: 0x02000098 RID: 152
	[Flags]
	[CLSCompliant(false)]
	[Obsolete("Use Mono.Unix.Native.FilePermissions")]
	public enum FileMode
	{
		// Token: 0x04000507 RID: 1287
		S_ISUID = 2048,
		// Token: 0x04000508 RID: 1288
		S_ISGID = 1024,
		// Token: 0x04000509 RID: 1289
		S_ISVTX = 512,
		// Token: 0x0400050A RID: 1290
		S_IRUSR = 256,
		// Token: 0x0400050B RID: 1291
		S_IWUSR = 128,
		// Token: 0x0400050C RID: 1292
		S_IXUSR = 64,
		// Token: 0x0400050D RID: 1293
		S_IRGRP = 32,
		// Token: 0x0400050E RID: 1294
		S_IWGRP = 16,
		// Token: 0x0400050F RID: 1295
		S_IXGRP = 8,
		// Token: 0x04000510 RID: 1296
		S_IROTH = 4,
		// Token: 0x04000511 RID: 1297
		S_IWOTH = 2,
		// Token: 0x04000512 RID: 1298
		S_IXOTH = 1
	}
}
