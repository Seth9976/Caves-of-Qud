using System;

namespace NAudio.Wave
{
	// Token: 0x020001D6 RID: 470
	public abstract class WaveProvider16 : IWaveProvider
	{
		// Token: 0x06000A35 RID: 2613 RVA: 0x0001DDE6 File Offset: 0x0001BFE6
		public WaveProvider16()
			: this(44100, 1)
		{
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0001DDF4 File Offset: 0x0001BFF4
		public WaveProvider16(int sampleRate, int channels)
		{
			this.SetWaveFormat(sampleRate, channels);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0001DE04 File Offset: 0x0001C004
		public void SetWaveFormat(int sampleRate, int channels)
		{
			this.waveFormat = new WaveFormat(sampleRate, 16, channels);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0001DE18 File Offset: 0x0001C018
		public int Read(byte[] buffer, int offset, int count)
		{
			WaveBuffer waveBuffer = new WaveBuffer(buffer);
			int num = count / 2;
			int num2 = this.Read(waveBuffer.ShortBuffer, offset / 2, num);
			return num2 * 2;
		}

		// Token: 0x06000A39 RID: 2617
		public abstract int Read(short[] buffer, int offset, int sampleCount);

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0001DE44 File Offset: 0x0001C044
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x04000B4E RID: 2894
		private WaveFormat waveFormat;
	}
}
