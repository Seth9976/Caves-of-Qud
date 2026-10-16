using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200074E RID: 1870
	internal struct Win32_IP_ADAPTER_GATEWAY_ADDRESS
	{
		// Token: 0x04002358 RID: 9048
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x04002359 RID: 9049
		public IntPtr Next;

		// Token: 0x0400235A RID: 9050
		public Win32_SOCKET_ADDRESS Address;
	}
}
