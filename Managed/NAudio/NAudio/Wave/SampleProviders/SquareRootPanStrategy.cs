using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000193 RID: 403
	public class SquareRootPanStrategy : IPanStrategy
	{
		// Token: 0x0600081C RID: 2076 RVA: 0x00017344 File Offset: 0x00015544
		public StereoSamplePair GetMultipliers(float pan)
		{
			float num = (-pan + 1f) / 2f;
			float num2 = (float)Math.Sqrt((double)num);
			float num3 = (float)Math.Sqrt((double)(1f - num));
			return new StereoSamplePair
			{
				Left = num2,
				Right = num3
			};
		}
	}
}
