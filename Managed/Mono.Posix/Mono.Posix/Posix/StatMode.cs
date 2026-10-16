using System;

namespace Mono.Posix
{
	// Token: 0x0200009E RID: 158
	[Flags]
	[Obsolete("Use Mono.Unix.Native.FilePermissions")]
	public enum StatMode
	{
		// Token: 0x0400053F RID: 1343
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFSOCK")]
		Socket = 49152,
		// Token: 0x04000540 RID: 1344
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFLNK")]
		SymLink = 40960,
		// Token: 0x04000541 RID: 1345
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFREG")]
		Regular = 32768,
		// Token: 0x04000542 RID: 1346
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFBLK")]
		BlockDevice = 24576,
		// Token: 0x04000543 RID: 1347
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFDIR")]
		Directory = 16384,
		// Token: 0x04000544 RID: 1348
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFCHR")]
		CharDevice = 8192,
		// Token: 0x04000545 RID: 1349
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFIFO")]
		FIFO = 4096,
		// Token: 0x04000546 RID: 1350
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_ISUID")]
		SUid = 2048,
		// Token: 0x04000547 RID: 1351
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_ISGID")]
		SGid = 1024,
		// Token: 0x04000548 RID: 1352
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_ISVTX")]
		Sticky = 512,
		// Token: 0x04000549 RID: 1353
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IRUSR")]
		OwnerRead = 256,
		// Token: 0x0400054A RID: 1354
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IWUSR")]
		OwnerWrite = 128,
		// Token: 0x0400054B RID: 1355
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IXUSR")]
		OwnerExecute = 64,
		// Token: 0x0400054C RID: 1356
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IRGRP")]
		GroupRead = 32,
		// Token: 0x0400054D RID: 1357
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IWGRP")]
		GroupWrite = 16,
		// Token: 0x0400054E RID: 1358
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IXGRP")]
		GroupExecute = 8,
		// Token: 0x0400054F RID: 1359
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IROTH")]
		OthersRead = 4,
		// Token: 0x04000550 RID: 1360
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IWOTH")]
		OthersWrite = 2,
		// Token: 0x04000551 RID: 1361
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IXOTH")]
		OthersExecute = 1
	}
}
