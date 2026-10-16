using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200074D RID: 1869
	internal struct Win32_IP_ADAPTER_MULTICAST_ADDRESS
	{
		// Token: 0x04002355 RID: 9045
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x04002356 RID: 9046
		public IntPtr Next;

		// Token: 0x04002357 RID: 9047
		public Win32_SOCKET_ADDRESS Address;
	}
}
