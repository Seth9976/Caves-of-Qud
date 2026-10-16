using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000187 RID: 391
	internal class Mono24SampleChunkConverter : ISampleChunkConverter
	{
		// Token: 0x060007E7 RID: 2023 RVA: 0x0001681E File Offset: 0x00014A1E
		public bool Supports(WaveFormat waveFormat)
		{
			return waveFormat.Encoding == WaveFormatEncoding.Pcm && waveFormat.BitsPerSample == 24 && waveFormat.Channels == 1;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00016840 File Offset: 0x00014A40
		public void LoadNextChunk(IWaveProvider source, int samplePairsRequired)
		{
			int num = samplePairsRequired * 3;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			this.sourceBytes = source.Read(this.sourceBuffer, 0, num);
			this.offset = 0;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00016880 File Offset: 0x00014A80
		public bool GetNextSample(out float sampleLeft, out float sampleRight)
		{
			if (this.offset < this.sourceBytes)
			{
				sampleLeft = (float)(((int)((sbyte)this.sourceBuffer[this.offset + 2]) << 16) | ((int)this.sourceBuffer[this.offset + 1] << 8) | (int)this.sourceBuffer[this.offset]) / 8388608f;
				this.offset += 3;
				sampleRight = sampleLeft;
				return true;
			}
			sampleLeft = 0f;
			sampleRight = 0f;
			return false;
		}

		// Token: 0x04000962 RID: 2402
		private int offset;

		// Token: 0x04000963 RID: 2403
		private byte[] sourceBuffer;

		// Token: 0x04000964 RID: 2404
		private int sourceBytes;
	}
}
