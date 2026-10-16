using System;
using NAudio.Wave.SampleProviders;

namespace NAudio.Wave
{
	// Token: 0x02000080 RID: 128
	public static class WaveExtensionMethods
	{
		// Token: 0x060002AE RID: 686 RVA: 0x000090B1 File Offset: 0x000072B1
		public static ISampleProvider ToSampleProvider(this IWaveProvider waveProvider)
		{
			return SampleProviderConverters.ConvertWaveProviderIntoSampleProvider(waveProvider);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000090BC File Offset: 0x000072BC
		public static void Init(this IWavePlayer wavePlayer, ISampleProvider sampleProvider, bool convertTo16Bit = false)
		{
			IWaveProvider waveProvider2;
			if (!convertTo16Bit)
			{
				IWaveProvider waveProvider = new SampleToWaveProvider(sampleProvider);
				waveProvider2 = waveProvider;
			}
			else
			{
				waveProvider2 = new SampleToWaveProvider16(sampleProvider);
			}
			IWaveProvider waveProvider3 = waveProvider2;
			wavePlayer.Init(waveProvider3);
		}
	}
}
