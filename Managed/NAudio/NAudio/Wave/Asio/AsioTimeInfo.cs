using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
	// Token: 0x02000151 RID: 337
	[StructLayout(0, Pack = 4)]
	internal struct AsioTimeInfo
	{
		// Token: 0x04000778 RID: 1912
		public double speed;

		// Token: 0x04000779 RID: 1913
		public ASIO64Bit systemTime;

		// Token: 0x0400077A RID: 1914
		public ASIO64Bit samplePosition;

		// Token: 0x0400077B RID: 1915
		public double sampleRate;

		// Token: 0x0400077C RID: 1916
		public AsioTimeInfoFlags flags;

		// Token: 0x0400077D RID: 1917
		[MarshalAs(23, SizeConst = 12)]
		public string reserved;
	}
}
