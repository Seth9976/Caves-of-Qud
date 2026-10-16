using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x0200018D RID: 397
	internal class StereoFloatSampleChunkConverter : ISampleChunkConverter
	{
		// Token: 0x060007FF RID: 2047 RVA: 0x00016D76 File Offset: 0x00014F76
		public bool Supports(WaveFormat waveFormat)
		{
			return waveFormat.Encoding == WaveFormatEncoding.IeeeFloat && waveFormat.Channels == 2;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00016D8C File Offset: 0x00014F8C
		public void LoadNextChunk(IWaveProvider source, int samplePairsRequired)
		{
			int num = samplePairsRequired * 8;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			this.sourceWaveBuffer = new WaveBuffer(this.sourceBuffer);
			this.sourceSamples = source.Read(this.sourceBuffer, 0, num) / 4;
			this.sourceSample = 0;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00016DE0 File Offset: 0x00014FE0
		public bool GetNextSample(out float sampleLeft, out float sampleRight)
		{
			if (this.sourceSample < this.sourceSamples)
			{
				sampleLeft = this.sourceWaveBuffer.FloatBuffer[this.sourceSample++];
				sampleRight = this.sourceWaveBuffer.FloatBuffer[this.sourceSample++];
				return true;
			}
			sampleLeft = 0f;
			sampleRight = 0f;
			return false;
		}

		// Token: 0x04000976 RID: 2422
		private int sourceSample;

		// Token: 0x04000977 RID: 2423
		private byte[] sourceBuffer;

		// Token: 0x04000978 RID: 2424
		private WaveBuffer sourceWaveBuffer;

		// Token: 0x04000979 RID: 2425
		private int sourceSamples;
	}
}
