using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200003C RID: 60
	[Flags]
	[Map]
	public enum WaitOptions
	{
		// Token: 0x04000217 RID: 535
		WNOHANG = 1,
		// Token: 0x04000218 RID: 536
		WUNTRACED = 2
	}
}
