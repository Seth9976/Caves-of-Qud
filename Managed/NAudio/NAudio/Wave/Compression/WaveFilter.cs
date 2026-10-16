using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Compression
{
	// Token: 0x02000172 RID: 370
	[StructLayout(0)]
	public class WaveFilter
	{
		// Token: 0x04000831 RID: 2097
		public int StructureSize = Marshal.SizeOf(typeof(WaveFilter));

		// Token: 0x04000832 RID: 2098
		public int FilterTag;

		// Token: 0x04000833 RID: 2099
		public int Filter;

		// Token: 0x04000834 RID: 2100
		[MarshalAs(30, SizeConst = 5)]
		public int[] Reserved;
	}
}
