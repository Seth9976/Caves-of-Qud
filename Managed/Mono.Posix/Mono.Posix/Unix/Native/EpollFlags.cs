using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200005D RID: 93
	[Flags]
	[Map]
	public enum EpollFlags
	{
		// Token: 0x0400044C RID: 1100
		EPOLL_CLOEXEC = 2000000,
		// Token: 0x0400044D RID: 1101
		EPOLL_NONBLOCK = 4000
	}
}
