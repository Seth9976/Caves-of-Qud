using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.WaveFormats
{
	// Token: 0x020001A3 RID: 419
	[StructLayout(0, Pack = 2)]
	internal class WmaWaveFormat : WaveFormat
	{
		// Token: 0x0600085D RID: 2141 RVA: 0x00017F32 File Offset: 0x00016132
		public WmaWaveFormat(int sampleRate, int bitsPerSample, int channels)
			: base(sampleRate, bitsPerSample, channels)
		{
			this.wValidBitsPerSample = (short)bitsPerSample;
			if (channels == 1)
			{
				this.dwChannelMask = 1;
			}
			else if (channels == 2)
			{
				this.dwChannelMask = 3;
			}
			this.waveFormatTag = WaveFormatEncoding.WindowsMediaAudio;
		}

		// Token: 0x04000A4C RID: 2636
		private short wValidBitsPerSample;

		// Token: 0x04000A4D RID: 2637
		private int dwChannelMask;

		// Token: 0x04000A4E RID: 2638
		private int dwReserved1;

		// Token: 0x04000A4F RID: 2639
		private int dwReserved2;

		// Token: 0x04000A50 RID: 2640
		private short wEncodeOptions;

		// Token: 0x04000A51 RID: 2641
		private short wReserved3;
	}
}
