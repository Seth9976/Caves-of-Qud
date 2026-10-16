using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000750 RID: 1872
	internal struct Win32_IP_ADAPTER_UNICAST_ADDRESS
	{
		// Token: 0x0400235E RID: 9054
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x0400235F RID: 9055
		public IntPtr Next;

		// Token: 0x04002360 RID: 9056
		public Win32_SOCKET_ADDRESS Address;

		// Token: 0x04002361 RID: 9057
		public PrefixOrigin PrefixOrigin;

		// Token: 0x04002362 RID: 9058
		public SuffixOrigin SuffixOrigin;

		// Token: 0x04002363 RID: 9059
		public DuplicateAddressDetectionState DadState;

		// Token: 0x04002364 RID: 9060
		public uint ValidLifetime;

		// Token: 0x04002365 RID: 9061
		public uint PreferredLifetime;

		// Token: 0x04002366 RID: 9062
		public uint LeaseLifetime;

		// Token: 0x04002367 RID: 9063
		public byte OnLinkPrefixLength;
	}
}
