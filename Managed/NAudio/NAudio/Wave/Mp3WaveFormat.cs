using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	// Token: 0x02000199 RID: 409
	[StructLayout(0, Pack = 2)]
	public class Mp3WaveFormat : WaveFormat
	{
		// Token: 0x0600083F RID: 2111 RVA: 0x000179F8 File Offset: 0x00015BF8
		public Mp3WaveFormat(int sampleRate, int channels, int blockSize, int bitRate)
		{
			this.waveFormatTag = WaveFormatEncoding.MpegLayer3;
			this.channels = (short)channels;
			this.averageBytesPerSecond = bitRate / 8;
			this.bitsPerSample = 0;
			this.blockAlign = 1;
			this.sampleRate = sampleRate;
			this.extraSize = 12;
			this.id = Mp3WaveFormatId.Mpeg;
			this.flags = Mp3WaveFormatFlags.PaddingIso;
			this.blockSize = (ushort)blockSize;
			this.framesPerBlock = 1;
			this.codecDelay = 0;
		}

		// Token: 0x04000992 RID: 2450
		private const short Mp3WaveFormatExtraBytes = 12;

		// Token: 0x04000993 RID: 2451
		public Mp3WaveFormatId id;

		// Token: 0x04000994 RID: 2452
		public Mp3WaveFormatFlags flags;

		// Token: 0x04000995 RID: 2453
		public ushort blockSize;

		// Token: 0x04000996 RID: 2454
		public ushort framesPerBlock;

		// Token: 0x04000997 RID: 2455
		public ushort codecDelay;
	}
}
