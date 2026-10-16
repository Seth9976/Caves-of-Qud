using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000052 RID: 82
	[Map]
	internal enum SockaddrType
	{
		// Token: 0x04000413 RID: 1043
		Invalid,
		// Token: 0x04000414 RID: 1044
		SockaddrStorage,
		// Token: 0x04000415 RID: 1045
		SockaddrUn,
		// Token: 0x04000416 RID: 1046
		Sockaddr,
		// Token: 0x04000417 RID: 1047
		SockaddrIn,
		// Token: 0x04000418 RID: 1048
		SockaddrIn6,
		// Token: 0x04000419 RID: 1049
		MustBeWrapped = 32768
	}
}
