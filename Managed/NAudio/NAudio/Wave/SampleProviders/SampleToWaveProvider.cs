using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x020001C9 RID: 457
	public class SampleToWaveProvider : IWaveProvider
	{
		// Token: 0x060009D8 RID: 2520 RVA: 0x0001C70A File Offset: 0x0001A90A
		public SampleToWaveProvider(ISampleProvider source)
		{
			if (source.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
			{
				throw new ArgumentException("Must be already floating point");
			}
			this.source = source;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0001C734 File Offset: 0x0001A934
		public int Read(byte[] buffer, int offset, int count)
		{
			int num = count / 4;
			WaveBuffer waveBuffer = new WaveBuffer(buffer);
			int num2 = this.source.Read(waveBuffer.FloatBuffer, offset / 4, num);
			return num2 * 4;
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x0001C765 File Offset: 0x0001A965
		public WaveFormat WaveFormat
		{
			get
			{
				return this.source.WaveFormat;
			}
		}

		// Token: 0x04000B17 RID: 2839
		private ISampleProvider source;
	}
}
