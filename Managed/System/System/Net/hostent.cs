using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020005FE RID: 1534
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct hostent
	{
		// Token: 0x04001C41 RID: 7233
		public IntPtr h_name;

		// Token: 0x04001C42 RID: 7234
		public IntPtr h_aliases;

		// Token: 0x04001C43 RID: 7235
		public short h_addrtype;

		// Token: 0x04001C44 RID: 7236
		public short h_length;

		// Token: 0x04001C45 RID: 7237
		public IntPtr h_addr_list;
	}
}
