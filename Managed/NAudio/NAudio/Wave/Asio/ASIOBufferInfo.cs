using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
	// Token: 0x0200014C RID: 332
	[StructLayout(0, Pack = 4)]
	internal struct ASIOBufferInfo
	{
		// Token: 0x06000716 RID: 1814 RVA: 0x0001529D File Offset: 0x0001349D
		public IntPtr Buffer(int bufferIndex)
		{
			if (bufferIndex != 0)
			{
				return this.pBuffer1;
			}
			return this.pBuffer0;
		}

		// Token: 0x04000757 RID: 1879
		public bool isInput;

		// Token: 0x04000758 RID: 1880
		public int channelNum;

		// Token: 0x04000759 RID: 1881
		public IntPtr pBuffer0;

		// Token: 0x0400075A RID: 1882
		public IntPtr pBuffer1;
	}
}
