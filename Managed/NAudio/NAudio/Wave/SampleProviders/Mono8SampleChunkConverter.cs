using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000188 RID: 392
	internal class Mono8SampleChunkConverter : ISampleChunkConverter
	{
		// Token: 0x060007EB RID: 2027 RVA: 0x00016902 File Offset: 0x00014B02
		public bool Supports(WaveFormat waveFormat)
		{
			return waveFormat.Encoding == WaveFormatEncoding.Pcm && waveFormat.BitsPerSample == 8 && waveFormat.Channels == 1;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00016924 File Offset: 0x00014B24
		public void LoadNextChunk(IWaveProvider source, int samplePairsRequired)
		{
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, samplePairsRequired);
			this.sourceBytes = source.Read(this.sourceBuffer, 0, samplePairsRequired);
			this.offset = 0;
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00016960 File Offset: 0x00014B60
		public bool GetNextSample(out float sampleLeft, out float sampleRight)
		{
			if (this.offset < this.sourceBytes)
			{
				sampleLeft = (float)this.sourceBuffer[this.offset] / 256f;
				this.offset++;
				sampleRight = sampleLeft;
				return true;
			}
			sampleLeft = 0f;
			sampleRight = 0f;
			return false;
		}

		// Token: 0x04000965 RID: 2405
		private int offset;

		// Token: 0x04000966 RID: 2406
		private byte[] sourceBuffer;

		// Token: 0x04000967 RID: 2407
		private int sourceBytes;
	}
}
