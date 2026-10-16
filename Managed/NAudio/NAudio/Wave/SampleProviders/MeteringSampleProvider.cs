using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x020001C4 RID: 452
	public class MeteringSampleProvider : ISampleProvider
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x0001C378 File Offset: 0x0001A578
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x0001C380 File Offset: 0x0001A580
		public int SamplesPerNotification { get; set; }

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060009C3 RID: 2499 RVA: 0x0001C38C File Offset: 0x0001A58C
		// (remove) Token: 0x060009C4 RID: 2500 RVA: 0x0001C3C4 File Offset: 0x0001A5C4
		public event EventHandler<StreamVolumeEventArgs> StreamVolume;

		// Token: 0x060009C5 RID: 2501 RVA: 0x0001C3F9 File Offset: 0x0001A5F9
		public MeteringSampleProvider(ISampleProvider source)
			: this(source, source.WaveFormat.SampleRate / 10)
		{
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0001C410 File Offset: 0x0001A610
		public MeteringSampleProvider(ISampleProvider source, int samplesPerNotification)
		{
			this.source = source;
			this.channels = source.WaveFormat.Channels;
			this.maxSamples = new float[this.channels];
			this.SamplesPerNotification = samplesPerNotification;
			this.args = new StreamVolumeEventArgs
			{
				MaxSampleValues = this.maxSamples
			};
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0001C46C File Offset: 0x0001A66C
		public WaveFormat WaveFormat
		{
			get
			{
				return this.source.WaveFormat;
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0001C47C File Offset: 0x0001A67C
		public int Read(float[] buffer, int offset, int count)
		{
			int num = this.source.Read(buffer, offset, count);
			if (this.StreamVolume != null)
			{
				for (int i = 0; i < num; i += this.channels)
				{
					for (int j = 0; j < this.channels; j++)
					{
						float num2 = Math.Abs(buffer[offset + i + j]);
						this.maxSamples[j] = Math.Max(this.maxSamples[j], num2);
					}
					this.sampleCount++;
					if (this.sampleCount >= this.SamplesPerNotification)
					{
						this.StreamVolume.Invoke(this, this.args);
						this.sampleCount = 0;
						Array.Clear(this.maxSamples, 0, this.channels);
					}
				}
			}
			return num;
		}

		// Token: 0x04000B09 RID: 2825
		private readonly ISampleProvider source;

		// Token: 0x04000B0A RID: 2826
		private readonly float[] maxSamples;

		// Token: 0x04000B0B RID: 2827
		private int sampleCount;

		// Token: 0x04000B0C RID: 2828
		private readonly int channels;

		// Token: 0x04000B0D RID: 2829
		private readonly StreamVolumeEventArgs args;
	}
}
