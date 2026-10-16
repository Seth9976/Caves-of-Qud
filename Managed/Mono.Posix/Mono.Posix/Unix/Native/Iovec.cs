using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200005C RID: 92
	[Map("struct iovec")]
	public struct Iovec
	{
		// Token: 0x04000449 RID: 1097
		public IntPtr iov_base;

		// Token: 0x0400044A RID: 1098
		[CLSCompliant(false)]
		public ulong iov_len;
	}
}
