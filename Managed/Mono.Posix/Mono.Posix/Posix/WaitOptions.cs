using System;

namespace Mono.Posix
{
	// Token: 0x02000099 RID: 153
	[Flags]
	[CLSCompliant(false)]
	[Obsolete("Use Mono.Unix.Native.WaitOptions")]
	public enum WaitOptions
	{
		// Token: 0x04000514 RID: 1300
		WNOHANG = 0,
		// Token: 0x04000515 RID: 1301
		WUNTRACED = 1
	}
}
