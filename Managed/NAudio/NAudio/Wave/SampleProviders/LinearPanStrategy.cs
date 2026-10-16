using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000195 RID: 405
	public class LinearPanStrategy : IPanStrategy
	{
		// Token: 0x06000820 RID: 2080 RVA: 0x000173F8 File Offset: 0x000155F8
		public StereoSamplePair GetMultipliers(float pan)
		{
			float num = (-pan + 1f) / 2f;
			float num2 = num;
			float num3 = 1f - num;
			return new StereoSamplePair
			{
				Left = num2,
				Right = num3
			};
		}
	}
}
