using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200074C RID: 1868
	internal struct Win32_IP_ADAPTER_DNS_SERVER_ADDRESS
	{
		// Token: 0x04002352 RID: 9042
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x04002353 RID: 9043
		public IntPtr Next;

		// Token: 0x04002354 RID: 9044
		public Win32_SOCKET_ADDRESS Address;
	}
}
