using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200074F RID: 1871
	internal struct Win32_IP_ADAPTER_WINS_SERVER_ADDRESS
	{
		// Token: 0x0400235B RID: 9051
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x0400235C RID: 9052
		public IntPtr Next;

		// Token: 0x0400235D RID: 9053
		public Win32_SOCKET_ADDRESS Address;
	}
}
