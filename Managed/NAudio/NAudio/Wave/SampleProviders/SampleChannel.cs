using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x020001F2 RID: 498
	public class SampleChannel : ISampleProvider
	{
		// Token: 0x06000B2F RID: 2863 RVA: 0x00021511 File Offset: 0x0001F711
		public SampleChannel(IWaveProvider waveProvider)
			: this(waveProvider, false)
		{
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0002151C File Offset: 0x0001F71C
		public SampleChannel(IWaveProvider waveProvider, bool forceStereo)
		{
			ISampleProvider sampleProvider = SampleProviderConverters.ConvertWaveProviderIntoSampleProvider(waveProvider);
			if (sampleProvider.WaveFormat.Channels == 1 && forceStereo)
			{
				sampleProvider = new MonoToStereoSampleProvider(sampleProvider);
			}
			this.waveFormat = sampleProvider.WaveFormat;
			this.preVolumeMeter = new MeteringSampleProvider(sampleProvider);
			this.volumeProvider = new VolumeSampleProvider(this.preVolumeMeter);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00021577 File Offset: 0x0001F777
		public int Read(float[] buffer, int offset, int sampleCount)
		{
			return this.volumeProvider.Read(buffer, offset, sampleCount);
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x00021587 File Offset: 0x0001F787
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0002158F File Offset: 0x0001F78F
		// (set) Token: 0x06000B34 RID: 2868 RVA: 0x0002159C File Offset: 0x0001F79C
		public float Volume
		{
			get
			{
				return this.volumeProvider.Volume;
			}
			set
			{
				this.volumeProvider.Volume = value;
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000B35 RID: 2869 RVA: 0x000215AA File Offset: 0x0001F7AA
		// (remove) Token: 0x06000B36 RID: 2870 RVA: 0x000215B8 File Offset: 0x0001F7B8
		public event EventHandler<StreamVolumeEventArgs> PreVolumeMeter
		{
			add
			{
				this.preVolumeMeter.StreamVolume += value;
			}
			remove
			{
				this.preVolumeMeter.StreamVolume -= value;
			}
		}

		// Token: 0x04000BC4 RID: 3012
		private readonly VolumeSampleProvider volumeProvider;

		// Token: 0x04000BC5 RID: 3013
		private readonly MeteringSampleProvider preVolumeMeter;

		// Token: 0x04000BC6 RID: 3014
		private readonly WaveFormat waveFormat;
	}
}
