using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200004C RID: 76
	[Map]
	[Flags]
	[CLSCompliant(false)]
	public enum UnixSocketFlags
	{
		// Token: 0x04000385 RID: 901
		SOCK_CLOEXEC = 524288,
		// Token: 0x04000386 RID: 902
		SOCK_NONBLOCK = 2048
	}
}
