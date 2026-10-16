using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x0200018B RID: 395
	internal class Stereo24SampleChunkConverter : ISampleChunkConverter
	{
		// Token: 0x060007F7 RID: 2039 RVA: 0x00016B74 File Offset: 0x00014D74
		public bool Supports(WaveFormat waveFormat)
		{
			return waveFormat.Encoding == WaveFormatEncoding.Pcm && waveFormat.BitsPerSample == 24 && waveFormat.Channels == 2;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00016B94 File Offset: 0x00014D94
		public void LoadNextChunk(IWaveProvider source, int samplePairsRequired)
		{
			int num = samplePairsRequired * 6;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			this.sourceBytes = source.Read(this.sourceBuffer, 0, num);
			this.offset = 0;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00016BD4 File Offset: 0x00014DD4
		public bool GetNextSample(out float sampleLeft, out float sampleRight)
		{
			if (this.offset < this.sourceBytes)
			{
				sampleLeft = (float)(((int)((sbyte)this.sourceBuffer[this.offset + 2]) << 16) | ((int)this.sourceBuffer[this.offset + 1] << 8) | (int)this.sourceBuffer[this.offset]) / 8388608f;
				this.offset += 3;
				sampleRight = (float)(((int)((sbyte)this.sourceBuffer[this.offset + 2]) << 16) | ((int)this.sourceBuffer[this.offset + 1] << 8) | (int)this.sourceBuffer[this.offset]) / 8388608f;
				this.offset += 3;
				return true;
			}
			sampleLeft = 0f;
			sampleRight = 0f;
			return false;
		}

		// Token: 0x04000970 RID: 2416
		private int offset;

		// Token: 0x04000971 RID: 2417
		private byte[] sourceBuffer;

		// Token: 0x04000972 RID: 2418
		private int sourceBytes;
	}
}
