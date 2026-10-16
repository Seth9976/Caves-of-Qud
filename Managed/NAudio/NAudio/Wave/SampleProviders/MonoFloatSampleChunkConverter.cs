using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000189 RID: 393
	internal class MonoFloatSampleChunkConverter : ISampleChunkConverter
	{
		// Token: 0x060007EF RID: 2031 RVA: 0x000169BC File Offset: 0x00014BBC
		public bool Supports(WaveFormat waveFormat)
		{
			return waveFormat.Encoding == WaveFormatEncoding.IeeeFloat && waveFormat.Channels == 1;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000169D4 File Offset: 0x00014BD4
		public void LoadNextChunk(IWaveProvider source, int samplePairsRequired)
		{
			int num = samplePairsRequired * 4;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			this.sourceWaveBuffer = new WaveBuffer(this.sourceBuffer);
			this.sourceSamples = source.Read(this.sourceBuffer, 0, num) / 4;
			this.sourceSample = 0;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00016A28 File Offset: 0x00014C28
		public bool GetNextSample(out float sampleLeft, out float sampleRight)
		{
			if (this.sourceSample < this.sourceSamples)
			{
				sampleLeft = this.sourceWaveBuffer.FloatBuffer[this.sourceSample++];
				sampleRight = sampleLeft;
				return true;
			}
			sampleLeft = 0f;
			sampleRight = 0f;
			return false;
		}

		// Token: 0x04000968 RID: 2408
		private int sourceSample;

		// Token: 0x04000969 RID: 2409
		private byte[] sourceBuffer;

		// Token: 0x0400096A RID: 2410
		private WaveBuffer sourceWaveBuffer;

		// Token: 0x0400096B RID: 2411
		private int sourceSamples;
	}
}
