using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000047 RID: 71
	[Map]
	[Flags]
	[CLSCompliant(false)]
	public enum MsyncFlags
	{
		// Token: 0x04000364 RID: 868
		MS_ASYNC = 1,
		// Token: 0x04000365 RID: 869
		MS_SYNC = 4,
		// Token: 0x04000366 RID: 870
		MS_INVALIDATE = 2
	}
}
