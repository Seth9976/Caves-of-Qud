using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000756 RID: 1878
	internal struct Win32_MIB_UDPSTATS
	{
		// Token: 0x0400237E RID: 9086
		public uint InDatagrams;

		// Token: 0x0400237F RID: 9087
		public uint NoPorts;

		// Token: 0x04002380 RID: 9088
		public uint InErrors;

		// Token: 0x04002381 RID: 9089
		public uint OutDatagrams;

		// Token: 0x04002382 RID: 9090
		public int NumAddrs;
	}
}
