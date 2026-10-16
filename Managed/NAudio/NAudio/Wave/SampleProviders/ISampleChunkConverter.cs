using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000185 RID: 389
	internal interface ISampleChunkConverter
	{
		// Token: 0x060007E0 RID: 2016
		bool Supports(WaveFormat format);

		// Token: 0x060007E1 RID: 2017
		void LoadNextChunk(IWaveProvider sourceProvider, int samplePairsRequired);

		// Token: 0x060007E2 RID: 2018
		bool GetNextSample(out float sampleLeft, out float sampleRight);
	}
}
