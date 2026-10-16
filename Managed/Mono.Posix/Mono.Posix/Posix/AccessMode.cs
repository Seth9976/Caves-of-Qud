using System;

namespace Mono.Posix
{
	// Token: 0x0200009A RID: 154
	[Flags]
	[CLSCompliant(false)]
	[Obsolete("Use Mono.Unix.Native.AccessModes")]
	public enum AccessMode
	{
		// Token: 0x04000517 RID: 1303
		R_OK = 1,
		// Token: 0x04000518 RID: 1304
		W_OK = 2,
		// Token: 0x04000519 RID: 1305
		X_OK = 4,
		// Token: 0x0400051A RID: 1306
		F_OK = 8
	}
}
