using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x020001C8 RID: 456
	public class VolumeSampleProvider : ISampleProvider
	{
		// Token: 0x060009D3 RID: 2515 RVA: 0x0001C681 File Offset: 0x0001A881
		public VolumeSampleProvider(ISampleProvider source)
		{
			this.source = source;
			this.volume = 1f;
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x0001C69B File Offset: 0x0001A89B
		public WaveFormat WaveFormat
		{
			get
			{
				return this.source.WaveFormat;
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0001C6A8 File Offset: 0x0001A8A8
		public int Read(float[] buffer, int offset, int sampleCount)
		{
			int num = this.source.Read(buffer, offset, sampleCount);
			if (this.volume != 1f)
			{
				for (int i = 0; i < sampleCount; i++)
				{
					buffer[offset + i] *= this.volume;
				}
			}
			return num;
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0001C6F9 File Offset: 0x0001A8F9
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x0001C701 File Offset: 0x0001A901
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

		// Token: 0x04000B15 RID: 2837
		private readonly ISampleProvider source;

		// Token: 0x04000B16 RID: 2838
		private float volume;
	}
}
