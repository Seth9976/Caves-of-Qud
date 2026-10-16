using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	// Token: 0x02000177 RID: 375
	[StructLayout(0)]
	internal class WaveHeader
	{
		// Token: 0x040008FA RID: 2298
		public IntPtr dataBuffer;

		// Token: 0x040008FB RID: 2299
		public int bufferLength;

		// Token: 0x040008FC RID: 2300
		public int bytesRecorded;

		// Token: 0x040008FD RID: 2301
		public IntPtr userData;

		// Token: 0x040008FE RID: 2302
		public WaveHeaderFlags flags;

		// Token: 0x040008FF RID: 2303
		public int loops;

		// Token: 0x04000900 RID: 2304
		public IntPtr next;

		// Token: 0x04000901 RID: 2305
		public IntPtr reserved;
	}
}
