using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000051 RID: 81
	[Map]
	[CLSCompliant(false)]
	public enum ShutdownOption
	{
		// Token: 0x0400040F RID: 1039
		SHUT_RD = 1,
		// Token: 0x04000410 RID: 1040
		SHUT_WR,
		// Token: 0x04000411 RID: 1041
		SHUT_RDWR
	}
}
