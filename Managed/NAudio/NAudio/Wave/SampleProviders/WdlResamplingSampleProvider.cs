using System;
using NAudio.Dsp;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x0200007F RID: 127
	public class WdlResamplingSampleProvider : ISampleProvider
	{
		// Token: 0x060002AB RID: 683 RVA: 0x00008FA4 File Offset: 0x000071A4
		public WdlResamplingSampleProvider(ISampleProvider source, int newSampleRate)
		{
			this.channels = source.WaveFormat.Channels;
			this.outFormat = WaveFormat.CreateIeeeFloatWaveFormat(newSampleRate, this.channels);
			this.source = source;
			this.resampler = new WdlResampler();
			this.resampler.SetMode(true, 2, false, 64, 32);
			this.resampler.SetFilterParms(0.693f, 0.707f);
			this.resampler.SetFeedMode(false);
			this.resampler.SetRates((double)source.WaveFormat.SampleRate, (double)newSampleRate);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00009038 File Offset: 0x00007238
		public int Read(float[] buffer, int offset, int count)
		{
			int num = count / this.channels;
			float[] array;
			int num3;
			int num2 = this.resampler.ResamplePrepare(num, this.outFormat.Channels, out array, out num3);
			int num4 = this.source.Read(array, num3, num2 * this.channels) / this.channels;
			int num5 = this.resampler.ResampleOut(buffer, offset, num4, num, this.channels);
			return num5 * this.channels;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002AD RID: 685 RVA: 0x000090A9 File Offset: 0x000072A9
		public WaveFormat WaveFormat
		{
			get
			{
				return this.outFormat;
			}
		}

		// Token: 0x040003AD RID: 941
		private readonly WdlResampler resampler;

		// Token: 0x040003AE RID: 942
		private readonly WaveFormat outFormat;

		// Token: 0x040003AF RID: 943
		private readonly ISampleProvider source;

		// Token: 0x040003B0 RID: 944
		private readonly int channels;
	}
}
