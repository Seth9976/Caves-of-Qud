using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000078 RID: 120
	internal static class SampleProviderConverters
	{
		// Token: 0x06000289 RID: 649 RVA: 0x00008614 File Offset: 0x00006814
		public static ISampleProvider ConvertWaveProviderIntoSampleProvider(IWaveProvider waveProvider)
		{
			ISampleProvider sampleProvider;
			if (waveProvider.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
			{
				if (waveProvider.WaveFormat.BitsPerSample == 8)
				{
					sampleProvider = new Pcm8BitToSampleProvider(waveProvider);
				}
				else if (waveProvider.WaveFormat.BitsPerSample == 16)
				{
					sampleProvider = new Pcm16BitToSampleProvider(waveProvider);
				}
				else if (waveProvider.WaveFormat.BitsPerSample == 24)
				{
					sampleProvider = new Pcm24BitToSampleProvider(waveProvider);
				}
				else
				{
					if (waveProvider.WaveFormat.BitsPerSample != 32)
					{
						throw new InvalidOperationException("Unsupported bit depth");
					}
					sampleProvider = new Pcm32BitToSampleProvider(waveProvider);
				}
			}
			else
			{
				if (waveProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
				{
					throw new ArgumentException("Unsupported source encoding");
				}
				if (waveProvider.WaveFormat.BitsPerSample == 64)
				{
					sampleProvider = new WaveToSampleProvider64(waveProvider);
				}
				else
				{
					sampleProvider = new WaveToSampleProvider(waveProvider);
				}
			}
			return sampleProvider;
		}
	}
}
