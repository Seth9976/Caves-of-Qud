using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200003D RID: 61
	[Flags]
	[Map]
	[CLSCompliant(false)]
	public enum AccessModes
	{
		// Token: 0x0400021A RID: 538
		R_OK = 1,
		// Token: 0x0400021B RID: 539
		W_OK = 2,
		// Token: 0x0400021C RID: 540
		X_OK = 4,
		// Token: 0x0400021D RID: 541
		F_OK = 8
	}
}
