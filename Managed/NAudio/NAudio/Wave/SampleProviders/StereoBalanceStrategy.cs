using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000192 RID: 402
	public class StereoBalanceStrategy : IPanStrategy
	{
		// Token: 0x0600081A RID: 2074 RVA: 0x000172DC File Offset: 0x000154DC
		public StereoSamplePair GetMultipliers(float pan)
		{
			float num = ((pan <= 0f) ? 1f : ((1f - pan) / 2f));
			float num2 = ((pan >= 0f) ? 1f : ((pan + 1f) / 2f));
			return new StereoSamplePair
			{
				Left = num,
				Right = num2
			};
		}
	}
}
