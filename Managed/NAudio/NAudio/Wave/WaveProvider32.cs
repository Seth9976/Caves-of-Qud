using System;

namespace NAudio.Wave
{
	// Token: 0x020001D7 RID: 471
	public abstract class WaveProvider32 : IWaveProvider, ISampleProvider
	{
		// Token: 0x06000A3B RID: 2619 RVA: 0x0001DE4C File Offset: 0x0001C04C
		public WaveProvider32()
			: this(44100, 1)
		{
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0001DE5A File Offset: 0x0001C05A
		public WaveProvider32(int sampleRate, int channels)
		{
			this.SetWaveFormat(sampleRate, channels);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0001DE6A File Offset: 0x0001C06A
		public void SetWaveFormat(int sampleRate, int channels)
		{
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0001DE7C File Offset: 0x0001C07C
		public int Read(byte[] buffer, int offset, int count)
		{
			WaveBuffer waveBuffer = new WaveBuffer(buffer);
			int num = count / 4;
			int num2 = this.Read(waveBuffer.FloatBuffer, offset / 4, num);
			return num2 * 4;
		}

		// Token: 0x06000A3F RID: 2623
		public abstract int Read(float[] buffer, int offset, int sampleCount);

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x0001DEA8 File Offset: 0x0001C0A8
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x04000B4F RID: 2895
		private WaveFormat waveFormat;
	}
}
