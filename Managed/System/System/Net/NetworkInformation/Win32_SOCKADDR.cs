using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000751 RID: 1873
	internal struct Win32_SOCKADDR
	{
		// Token: 0x04002368 RID: 9064
		public ushort AddressFamily;

		// Token: 0x04002369 RID: 9065
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
		public byte[] AddressData;
	}
}
