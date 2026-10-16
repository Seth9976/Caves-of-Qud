using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x0200007B RID: 123
	public class SampleToWaveProvider24 : IWaveProvider
	{
		// Token: 0x06000291 RID: 657 RVA: 0x0000881C File Offset: 0x00006A1C
		public SampleToWaveProvider24(ISampleProvider sourceProvider)
		{
			if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
			{
				throw new ArgumentException("Input source provider must be IEEE float", "sourceProvider");
			}
			if (sourceProvider.WaveFormat.BitsPerSample != 32)
			{
				throw new ArgumentException("Input source provider must be 32 bit", "sourceProvider");
			}
			this.waveFormat = new WaveFormat(sourceProvider.WaveFormat.SampleRate, 24, sourceProvider.WaveFormat.Channels);
			this.sourceProvider = sourceProvider;
			this.volume = 1f;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000088A4 File Offset: 0x00006AA4
		public int Read(byte[] destBuffer, int offset, int numBytes)
		{
			int num = numBytes / 3;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			int num2 = this.sourceProvider.Read(this.sourceBuffer, 0, num);
			int num3 = offset;
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
				int num5 = (int)((double)num4 * 8388607.0);
				destBuffer[num3++] = (byte)num5;
				destBuffer[num3++] = (byte)(num5 >> 8);
				destBuffer[num3++] = (byte)(num5 >> 16);
			}
			return num2 * 3;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00008952 File Offset: 0x00006B52
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000895A File Offset: 0x00006B5A
		// (set) Token: 0x06000295 RID: 661 RVA: 0x00008964 File Offset: 0x00006B64
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

		// Token: 0x04000395 RID: 917
		private readonly ISampleProvider sourceProvider;

		// Token: 0x04000396 RID: 918
		private readonly WaveFormat waveFormat;

		// Token: 0x04000397 RID: 919
		private volatile float volume;

		// Token: 0x04000398 RID: 920
		private float[] sourceBuffer;
	}
}
