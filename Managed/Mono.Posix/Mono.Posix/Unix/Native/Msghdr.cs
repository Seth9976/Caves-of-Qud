using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000071 RID: 113
	[CLSCompliant(false)]
	public sealed class Msghdr
	{
		// Token: 0x040004A0 RID: 1184
		public Sockaddr msg_name;

		// Token: 0x040004A1 RID: 1185
		public Iovec[] msg_iov;

		// Token: 0x040004A2 RID: 1186
		public int msg_iovlen;

		// Token: 0x040004A3 RID: 1187
		public byte[] msg_control;

		// Token: 0x040004A4 RID: 1188
		public long msg_controllen;

		// Token: 0x040004A5 RID: 1189
		public MessageFlags msg_flags;
	}
}
