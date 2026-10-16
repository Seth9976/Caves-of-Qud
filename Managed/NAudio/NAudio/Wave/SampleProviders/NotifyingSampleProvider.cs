using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x020001C7 RID: 455
	public class NotifyingSampleProvider : ISampleProvider, ISampleNotifier
	{
		// Token: 0x060009CE RID: 2510 RVA: 0x0001C54F File Offset: 0x0001A74F
		public NotifyingSampleProvider(ISampleProvider source)
		{
			this.source = source;
			this.channels = this.WaveFormat.Channels;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x0001C584 File Offset: 0x0001A784
		public WaveFormat WaveFormat
		{
			get
			{
				return this.source.WaveFormat;
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0001C594 File Offset: 0x0001A794
		public int Read(float[] buffer, int offset, int sampleCount)
		{
			int num = this.source.Read(buffer, offset, sampleCount);
			if (this.Sample != null)
			{
				for (int i = 0; i < num; i += this.channels)
				{
					this.sampleArgs.Left = buffer[offset + i];
					this.sampleArgs.Right = ((this.channels > 1) ? buffer[offset + i + 1] : this.sampleArgs.Left);
					this.Sample.Invoke(this, this.sampleArgs);
				}
			}
			return num;
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060009D1 RID: 2513 RVA: 0x0001C614 File Offset: 0x0001A814
		// (remove) Token: 0x060009D2 RID: 2514 RVA: 0x0001C64C File Offset: 0x0001A84C
		public event EventHandler<SampleEventArgs> Sample;

		// Token: 0x04000B11 RID: 2833
		private ISampleProvider source;

		// Token: 0x04000B12 RID: 2834
		private SampleEventArgs sampleArgs = new SampleEventArgs(0f, 0f);

		// Token: 0x04000B13 RID: 2835
		private int channels;
	}
}
