using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000042 RID: 66
	[Map]
	[Flags]
	public enum PollEvents : short
	{
		// Token: 0x04000331 RID: 817
		POLLIN = 1,
		// Token: 0x04000332 RID: 818
		POLLPRI = 2,
		// Token: 0x04000333 RID: 819
		POLLOUT = 4,
		// Token: 0x04000334 RID: 820
		POLLERR = 8,
		// Token: 0x04000335 RID: 821
		POLLHUP = 16,
		// Token: 0x04000336 RID: 822
		POLLNVAL = 32,
		// Token: 0x04000337 RID: 823
		POLLRDNORM = 64,
		// Token: 0x04000338 RID: 824
		POLLRDBAND = 128,
		// Token: 0x04000339 RID: 825
		POLLWRNORM = 256,
		// Token: 0x0400033A RID: 826
		POLLWRBAND = 512
	}
}
