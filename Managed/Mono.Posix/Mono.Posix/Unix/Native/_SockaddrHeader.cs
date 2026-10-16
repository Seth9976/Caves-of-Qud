using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200006A RID: 106
	[Map]
	internal struct _SockaddrHeader
	{
		// Token: 0x0400048B RID: 1163
		internal SockaddrType type;

		// Token: 0x0400048C RID: 1164
		internal UnixAddressFamily sa_family;
	}
}
