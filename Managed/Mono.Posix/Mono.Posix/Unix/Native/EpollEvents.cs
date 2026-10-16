using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200005E RID: 94
	[Flags]
	[Map]
	[CLSCompliant(false)]
	public enum EpollEvents : uint
	{
		// Token: 0x0400044F RID: 1103
		EPOLLIN = 1U,
		// Token: 0x04000450 RID: 1104
		EPOLLPRI = 2U,
		// Token: 0x04000451 RID: 1105
		EPOLLOUT = 4U,
		// Token: 0x04000452 RID: 1106
		EPOLLRDNORM = 64U,
		// Token: 0x04000453 RID: 1107
		EPOLLRDBAND = 128U,
		// Token: 0x04000454 RID: 1108
		EPOLLWRNORM = 256U,
		// Token: 0x04000455 RID: 1109
		EPOLLWRBAND = 512U,
		// Token: 0x04000456 RID: 1110
		EPOLLMSG = 1024U,
		// Token: 0x04000457 RID: 1111
		EPOLLERR = 8U,
		// Token: 0x04000458 RID: 1112
		EPOLLHUP = 16U,
		// Token: 0x04000459 RID: 1113
		EPOLLRDHUP = 8192U,
		// Token: 0x0400045A RID: 1114
		EPOLLONESHOT = 1073741824U,
		// Token: 0x0400045B RID: 1115
		EPOLLET = 2147483648U
	}
}
