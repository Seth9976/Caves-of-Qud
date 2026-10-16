using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000749 RID: 1865
	internal struct Win32_IP_ADDR_STRING
	{
		// Token: 0x04002347 RID: 9031
		public IntPtr Next;

		// Token: 0x04002348 RID: 9032
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		public string IpAddress;

		// Token: 0x04002349 RID: 9033
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		public string IpMask;

		// Token: 0x0400234A RID: 9034
		public uint Context;
	}
}
