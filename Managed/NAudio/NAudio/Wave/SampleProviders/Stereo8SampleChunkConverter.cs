using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x0200018C RID: 396
	internal class Stereo8SampleChunkConverter : ISampleChunkConverter
	{
		// Token: 0x060007FB RID: 2043 RVA: 0x00016C9F File Offset: 0x00014E9F
		public bool Supports(WaveFormat waveFormat)
		{
			return waveFormat.Encoding == WaveFormatEncoding.Pcm && waveFormat.BitsPerSample == 8 && waveFormat.Channels == 2;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00016CC0 File Offset: 0x00014EC0
		public void LoadNextChunk(IWaveProvider source, int samplePairsRequired)
		{
			int num = samplePairsRequired * 2;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			this.sourceBytes = source.Read(this.sourceBuffer, 0, num);
			this.offset = 0;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00016D00 File Offset: 0x00014F00
		public bool GetNextSample(out float sampleLeft, out float sampleRight)
		{
			if (this.offset < this.sourceBytes)
			{
				sampleLeft = (float)this.sourceBuffer[this.offset++] / 256f;
				sampleRight = (float)this.sourceBuffer[this.offset++] / 256f;
				return true;
			}
			sampleLeft = 0f;
			sampleRight = 0f;
			return false;
		}

		// Token: 0x04000973 RID: 2419
		private int offset;

		// Token: 0x04000974 RID: 2420
		private byte[] sourceBuffer;

		// Token: 0x04000975 RID: 2421
		private int sourceBytes;
	}
}
