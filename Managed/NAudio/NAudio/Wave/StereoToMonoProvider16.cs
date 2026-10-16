using System;
using NAudio.Utils;

namespace NAudio.Wave
{
	// Token: 0x020001D0 RID: 464
	public class StereoToMonoProvider16 : IWaveProvider
	{
		// Token: 0x06000A14 RID: 2580 RVA: 0x0001D6E8 File Offset: 0x0001B8E8
		public StereoToMonoProvider16(IWaveProvider sourceProvider)
		{
			if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
			{
				throw new ArgumentException("Source must be PCM");
			}
			if (sourceProvider.WaveFormat.Channels != 2)
			{
				throw new ArgumentException("Source must be stereo");
			}
			if (sourceProvider.WaveFormat.BitsPerSample != 16)
			{
				throw new ArgumentException("Source must be 16 bit");
			}
			this.sourceProvider = sourceProvider;
			this.outputFormat = new WaveFormat(sourceProvider.WaveFormat.SampleRate, 1);
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0001D765 File Offset: 0x0001B965
		// (set) Token: 0x06000A16 RID: 2582 RVA: 0x0001D76D File Offset: 0x0001B96D
		public float LeftVolume { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0001D776 File Offset: 0x0001B976
		// (set) Token: 0x06000A18 RID: 2584 RVA: 0x0001D77E File Offset: 0x0001B97E
		public float RightVolume { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0001D787 File Offset: 0x0001B987
		public WaveFormat WaveFormat
		{
			get
			{
				return this.outputFormat;
			}
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0001D790 File Offset: 0x0001B990
		public int Read(byte[] buffer, int offset, int count)
		{
			int num = count * 2;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			WaveBuffer waveBuffer = new WaveBuffer(this.sourceBuffer);
			WaveBuffer waveBuffer2 = new WaveBuffer(buffer);
			int num2 = this.sourceProvider.Read(this.sourceBuffer, 0, num);
			int num3 = num2 / 2;
			int num4 = offset / 2;
			for (int i = 0; i < num3; i += 2)
			{
				short num5 = waveBuffer.ShortBuffer[i];
				short num6 = waveBuffer.ShortBuffer[i + 1];
				float num7 = (float)num5 * this.LeftVolume + (float)num6 * this.RightVolume;
				if (num7 > 32767f)
				{
					num7 = 32767f;
				}
				if (num7 < -32768f)
				{
					num7 = -32768f;
				}
				waveBuffer2.ShortBuffer[num4++] = (short)num7;
			}
			return num2 / 2;
		}

		// Token: 0x04000B38 RID: 2872
		private IWaveProvider sourceProvider;

		// Token: 0x04000B39 RID: 2873
		private WaveFormat outputFormat;

		// Token: 0x04000B3A RID: 2874
		private byte[] sourceBuffer;
	}
}
