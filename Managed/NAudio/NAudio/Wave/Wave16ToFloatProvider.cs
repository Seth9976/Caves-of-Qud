using System;
using NAudio.Utils;

namespace NAudio.Wave
{
	// Token: 0x020001D4 RID: 468
	public class Wave16ToFloatProvider : IWaveProvider
	{
		// Token: 0x06000A2C RID: 2604 RVA: 0x0001DC54 File Offset: 0x0001BE54
		public Wave16ToFloatProvider(IWaveProvider sourceProvider)
		{
			if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
			{
				throw new ArgumentException("Only PCM supported");
			}
			if (sourceProvider.WaveFormat.BitsPerSample != 16)
			{
				throw new ArgumentException("Only 16 bit audio supported");
			}
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sourceProvider.WaveFormat.SampleRate, sourceProvider.WaveFormat.Channels);
			this.sourceProvider = sourceProvider;
			this.volume = 1f;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0001DCD0 File Offset: 0x0001BED0
		public int Read(byte[] destBuffer, int offset, int numBytes)
		{
			int num = numBytes / 2;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			int num2 = this.sourceProvider.Read(this.sourceBuffer, offset, num);
			WaveBuffer waveBuffer = new WaveBuffer(this.sourceBuffer);
			WaveBuffer waveBuffer2 = new WaveBuffer(destBuffer);
			int num3 = num2 / 2;
			int num4 = offset / 4;
			for (int i = 0; i < num3; i++)
			{
				waveBuffer2.FloatBuffer[num4++] = (float)waveBuffer.ShortBuffer[i] / 32768f * this.volume;
			}
			return num3 * 4;
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0001DD60 File Offset: 0x0001BF60
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x0001DD68 File Offset: 0x0001BF68
		// (set) Token: 0x06000A30 RID: 2608 RVA: 0x0001DD72 File Offset: 0x0001BF72
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

		// Token: 0x04000B48 RID: 2888
		private IWaveProvider sourceProvider;

		// Token: 0x04000B49 RID: 2889
		private readonly WaveFormat waveFormat;

		// Token: 0x04000B4A RID: 2890
		private volatile float volume;

		// Token: 0x04000B4B RID: 2891
		private byte[] sourceBuffer;
	}
}
