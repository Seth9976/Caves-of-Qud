using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x0200007A RID: 122
	public class SampleToWaveProvider16 : IWaveProvider
	{
		// Token: 0x0600028C RID: 652 RVA: 0x000086D8 File Offset: 0x000068D8
		public SampleToWaveProvider16(ISampleProvider sourceProvider)
		{
			if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
			{
				throw new ArgumentException("Input source provider must be IEEE float", "sourceProvider");
			}
			if (sourceProvider.WaveFormat.BitsPerSample != 32)
			{
				throw new ArgumentException("Input source provider must be 32 bit", "sourceProvider");
			}
			this.waveFormat = new WaveFormat(sourceProvider.WaveFormat.SampleRate, 16, sourceProvider.WaveFormat.Channels);
			this.sourceProvider = sourceProvider;
			this.volume = 1f;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00008760 File Offset: 0x00006960
		public int Read(byte[] destBuffer, int offset, int numBytes)
		{
			int num = numBytes / 2;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			int num2 = this.sourceProvider.Read(this.sourceBuffer, 0, num);
			WaveBuffer waveBuffer = new WaveBuffer(destBuffer);
			int num3 = offset / 2;
			for (int i = 0; i < num2; i++)
			{
				float num4 = this.sourceBuffer[i] * this.volume;
				if (num4 > 1f)
				{
					num4 = 1f;
				}
				if (num4 < -1f)
				{
					num4 = -1f;
				}
				waveBuffer.ShortBuffer[num3++] = (short)(num4 * 32767f);
			}
			return num2 * 2;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600028E RID: 654 RVA: 0x000087FE File Offset: 0x000069FE
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00008806 File Offset: 0x00006A06
		// (set) Token: 0x06000290 RID: 656 RVA: 0x00008810 File Offset: 0x00006A10
		public float Volume
		{
			get
			{
				return this.volume;
			}
			set
			{
				this.volume = value;
			}
		}

		// Token: 0x04000391 RID: 913
		private readonly ISampleProvider sourceProvider;

		// Token: 0x04000392 RID: 914
		private readonly WaveFormat waveFormat;

		// Token: 0x04000393 RID: 915
		private volatile float volume;

		// Token: 0x04000394 RID: 916
		private float[] sourceBuffer;
	}
}
