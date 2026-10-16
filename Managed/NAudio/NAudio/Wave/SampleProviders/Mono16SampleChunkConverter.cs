using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000186 RID: 390
	internal class Mono16SampleChunkConverter : ISampleChunkConverter
	{
		// Token: 0x060007E3 RID: 2019 RVA: 0x0001674B File Offset: 0x0001494B
		public bool Supports(WaveFormat waveFormat)
		{
			return waveFormat.Encoding == WaveFormatEncoding.Pcm && waveFormat.BitsPerSample == 16 && waveFormat.Channels == 1;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001676C File Offset: 0x0001496C
		public void LoadNextChunk(IWaveProvider source, int samplePairsRequired)
		{
			int num = samplePairsRequired * 2;
			this.sourceSample = 0;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			this.sourceWaveBuffer = new WaveBuffer(this.sourceBuffer);
			this.sourceSamples = source.Read(this.sourceBuffer, 0, num) / 2;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x000167C0 File Offset: 0x000149C0
		public bool GetNextSample(out float sampleLeft, out float sampleRight)
		{
			if (this.sourceSample < this.sourceSamples)
			{
				sampleLeft = (float)this.sourceWaveBuffer.ShortBuffer[this.sourceSample++] / 32768f;
				sampleRight = sampleLeft;
				return true;
			}
			sampleLeft = 0f;
			sampleRight = 0f;
			return false;
		}

		// Token: 0x0400095E RID: 2398
		private int sourceSample;

		// Token: 0x0400095F RID: 2399
		private byte[] sourceBuffer;

		// Token: 0x04000960 RID: 2400
		private WaveBuffer sourceWaveBuffer;

		// Token: 0x04000961 RID: 2401
		private int sourceSamples;
	}
}
