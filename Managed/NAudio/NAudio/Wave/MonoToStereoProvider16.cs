using System;
using NAudio.Utils;

namespace NAudio.Wave
{
	// Token: 0x020001D1 RID: 465
	public class MonoToStereoProvider16 : IWaveProvider
	{
		// Token: 0x06000A1B RID: 2587 RVA: 0x0001D858 File Offset: 0x0001BA58
		public MonoToStereoProvider16(IWaveProvider sourceProvider)
		{
			if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
			{
				throw new ArgumentException("Source must be PCM");
			}
			if (sourceProvider.WaveFormat.Channels != 1)
			{
				throw new ArgumentException("Source must be Mono");
			}
			if (sourceProvider.WaveFormat.BitsPerSample != 16)
			{
				throw new ArgumentException("Source must be 16 bit");
			}
			this.sourceProvider = sourceProvider;
			this.outputFormat = new WaveFormat(sourceProvider.WaveFormat.SampleRate, 2);
			this.RightVolume = 1f;
			this.LeftVolume = 1f;
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0001D8EB File Offset: 0x0001BAEB
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x0001D8F3 File Offset: 0x0001BAF3
		public float LeftVolume { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0001D8FC File Offset: 0x0001BAFC
		// (set) Token: 0x06000A1F RID: 2591 RVA: 0x0001D904 File Offset: 0x0001BB04
		public float RightVolume { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0001D90D File Offset: 0x0001BB0D
		public WaveFormat WaveFormat
		{
			get
			{
				return this.outputFormat;
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001D918 File Offset: 0x0001BB18
		public int Read(byte[] buffer, int offset, int count)
		{
			int num = count / 2;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			WaveBuffer waveBuffer = new WaveBuffer(this.sourceBuffer);
			WaveBuffer waveBuffer2 = new WaveBuffer(buffer);
			int num2 = this.sourceProvider.Read(this.sourceBuffer, 0, num);
			int num3 = num2 / 2;
			int num4 = offset / 2;
			for (int i = 0; i < num3; i++)
			{
				short num5 = waveBuffer.ShortBuffer[i];
				waveBuffer2.ShortBuffer[num4++] = (short)(this.LeftVolume * (float)num5);
				waveBuffer2.ShortBuffer[num4++] = (short)(this.RightVolume * (float)num5);
			}
			return num3 * 4;
		}

		// Token: 0x04000B3D RID: 2877
		private IWaveProvider sourceProvider;

		// Token: 0x04000B3E RID: 2878
		private WaveFormat outputFormat;

		// Token: 0x04000B3F RID: 2879
		private byte[] sourceBuffer;
	}
}
