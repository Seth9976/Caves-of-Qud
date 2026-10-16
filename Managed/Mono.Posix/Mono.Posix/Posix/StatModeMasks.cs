using System;

namespace Mono.Posix
{
	// Token: 0x0200009D RID: 157
	[Obsolete("Use Mono.Unix.Native.FilePermissions")]
	public enum StatModeMasks
	{
		// Token: 0x0400053A RID: 1338
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_IFMT")]
		TypeMask = 61440,
		// Token: 0x0400053B RID: 1339
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_RWXU")]
		OwnerMask = 448,
		// Token: 0x0400053C RID: 1340
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_RWXG")]
		GroupMask = 56,
		// Token: 0x0400053D RID: 1341
		[Obsolete("Use Mono.Unix.Native.FilePermissions.S_RWXO")]
		OthersMask = 7
	}
}
