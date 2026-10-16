using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000076 RID: 118
	public abstract class SampleProviderConverterBase : ISampleProvider
	{
		// Token: 0x06000283 RID: 643 RVA: 0x0000853F File Offset: 0x0000673F
		public SampleProviderConverterBase(IWaveProvider source)
		{
			this.source = source;
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(source.WaveFormat.SampleRate, source.WaveFormat.Channels);
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000856F File Offset: 0x0000676F
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x06000285 RID: 645
		public abstract int Read(float[] buffer, int offset, int count);

		// Token: 0x06000286 RID: 646 RVA: 0x00008577 File Offset: 0x00006777
		protected void EnsureSourceBuffer(int sourceBytesRequired)
		{
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, sourceBytesRequired);
		}

		// Token: 0x0400038E RID: 910
		protected IWaveProvider source;

		// Token: 0x0400038F RID: 911
		private WaveFormat waveFormat;

		// Token: 0x04000390 RID: 912
		protected byte[] sourceBuffer;
	}
}
