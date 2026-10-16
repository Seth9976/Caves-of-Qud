using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200074B RID: 1867
	internal struct Win32_IP_ADAPTER_ANYCAST_ADDRESS
	{
		// Token: 0x0400234F RID: 9039
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x04002350 RID: 9040
		public IntPtr Next;

		// Token: 0x04002351 RID: 9041
		public Win32_SOCKET_ADDRESS Address;
	}
}
