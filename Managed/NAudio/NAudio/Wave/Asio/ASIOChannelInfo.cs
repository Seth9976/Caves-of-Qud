using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
	// Token: 0x0200014B RID: 331
	[StructLayout(0, Pack = 4)]
	internal struct ASIOChannelInfo
	{
		// Token: 0x04000751 RID: 1873
		public int channel;

		// Token: 0x04000752 RID: 1874
		public bool isInput;

		// Token: 0x04000753 RID: 1875
		public bool isActive;

		// Token: 0x04000754 RID: 1876
		public int channelGroup;

		// Token: 0x04000755 RID: 1877
		[MarshalAs(8)]
		public AsioSampleType type;

		// Token: 0x04000756 RID: 1878
		[MarshalAs(23, SizeConst = 32)]
		public string name;
	}
}
