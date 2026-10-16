using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	// Token: 0x02000198 RID: 408
	[StructLayout(0, Pack = 2)]
	public class ImaAdpcmWaveFormat : WaveFormat
	{
		// Token: 0x0600083D RID: 2109 RVA: 0x0001799E File Offset: 0x00015B9E
		private ImaAdpcmWaveFormat()
		{
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x000179A8 File Offset: 0x00015BA8
		public ImaAdpcmWaveFormat(int sampleRate, int channels, int bitsPerSample)
		{
			this.waveFormatTag = WaveFormatEncoding.DviAdpcm;
			this.sampleRate = sampleRate;
			this.channels = (short)channels;
			this.bitsPerSample = (short)bitsPerSample;
			this.extraSize = 2;
			this.blockAlign = 0;
			this.averageBytesPerSecond = 0;
			this.samplesPerBlock = 0;
		}

		// Token: 0x04000991 RID: 2449
		private short samplesPerBlock;
	}
}
