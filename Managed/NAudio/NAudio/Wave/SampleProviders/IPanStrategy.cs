using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000191 RID: 401
	public interface IPanStrategy
	{
		// Token: 0x06000819 RID: 2073
		StereoSamplePair GetMultipliers(float pan);
	}
}
