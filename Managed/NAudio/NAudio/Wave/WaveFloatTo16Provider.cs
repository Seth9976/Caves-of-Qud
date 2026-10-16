using System;
using NAudio.Utils;

namespace NAudio.Wave
{
	// Token: 0x020001D3 RID: 467
	public class WaveFloatTo16Provider : IWaveProvider
	{
		// Token: 0x06000A27 RID: 2599 RVA: 0x0001DAF8 File Offset: 0x0001BCF8
		public WaveFloatTo16Provider(IWaveProvider sourceProvider)
		{
			if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
			{
				throw new ArgumentException("Input wave provider must be IEEE float", "sourceProvider");
			}
			if (sourceProvider.WaveFormat.BitsPerSample != 32)
			{
				throw new ArgumentException("Input wave provider must be 32 bit", "sourceProvider");
			}
			this.waveFormat = new WaveFormat(sourceProvider.WaveFormat.SampleRate, 16, sourceProvider.WaveFormat.Channels);
			this.sourceProvider = sourceProvider;
			this.volume = 1f;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0001DB80 File Offset: 0x0001BD80
		public int Read(byte[] destBuffer, int offset, int numBytes)
		{
			int num = numBytes * 2;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			int num2 = this.sourceProvider.Read(this.sourceBuffer, 0, num);
			WaveBuffer waveBuffer = new WaveBuffer(this.sourceBuffer);
			WaveBuffer waveBuffer2 = new WaveBuffer(destBuffer);
			int num3 = num2 / 4;
			int num4 = offset / 2;
			for (int i = 0; i < num3; i++)
			{
				float num5 = waveBuffer.FloatBuffer[i] * this.volume;
				if (num5 > 1f)
				{
					num5 = 1f;
				}
				if (num5 < -1f)
				{
					num5 = -1f;
				}
				waveBuffer2.ShortBuffer[num4++] = (short)(num5 * 32767f);
			}
			return num3 * 2;
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0001DC34 File Offset: 0x0001BE34
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x0001DC3C File Offset: 0x0001BE3C
		// (set) Token: 0x06000A2B RID: 2603 RVA: 0x0001DC46 File Offset: 0x0001BE46
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

		// Token: 0x04000B44 RID: 2884
		private readonly IWaveProvider sourceProvider;

		// Token: 0x04000B45 RID: 2885
		private readonly WaveFormat waveFormat;

		// Token: 0x04000B46 RID: 2886
		private volatile float volume;

		// Token: 0x04000B47 RID: 2887
		private byte[] sourceBuffer;
	}
}
