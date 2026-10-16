using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x0200018A RID: 394
	internal class Stereo16SampleChunkConverter : ISampleChunkConverter
	{
		// Token: 0x060007F3 RID: 2035 RVA: 0x00016A7F File Offset: 0x00014C7F
		public bool Supports(WaveFormat waveFormat)
		{
			return waveFormat.Encoding == WaveFormatEncoding.Pcm && waveFormat.BitsPerSample == 16 && waveFormat.Channels == 2;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00016AA0 File Offset: 0x00014CA0
		public void LoadNextChunk(IWaveProvider source, int samplePairsRequired)
		{
			int num = samplePairsRequired * 4;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			this.sourceWaveBuffer = new WaveBuffer(this.sourceBuffer);
			this.sourceSamples = source.Read(this.sourceBuffer, 0, num) / 2;
			this.sourceSample = 0;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00016AF4 File Offset: 0x00014CF4
		public bool GetNextSample(out float sampleLeft, out float sampleRight)
		{
			if (this.sourceSample < this.sourceSamples)
			{
				sampleLeft = (float)this.sourceWaveBuffer.ShortBuffer[this.sourceSample++] / 32768f;
				sampleRight = (float)this.sourceWaveBuffer.ShortBuffer[this.sourceSample++] / 32768f;
				return true;
			}
			sampleLeft = 0f;
			sampleRight = 0f;
			return false;
		}

		// Token: 0x0400096C RID: 2412
		private int sourceSample;

		// Token: 0x0400096D RID: 2413
		private byte[] sourceBuffer;

		// Token: 0x0400096E RID: 2414
		private WaveBuffer sourceWaveBuffer;

		// Token: 0x0400096F RID: 2415
		private int sourceSamples;
	}
}
