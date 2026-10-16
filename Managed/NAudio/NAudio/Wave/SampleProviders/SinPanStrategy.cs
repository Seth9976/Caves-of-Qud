using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000194 RID: 404
	public class SinPanStrategy : IPanStrategy
	{
		// Token: 0x0600081E RID: 2078 RVA: 0x0001739C File Offset: 0x0001559C
		public StereoSamplePair GetMultipliers(float pan)
		{
			float num = (-pan + 1f) / 2f;
			float num2 = (float)Math.Sin((double)(num * 1.5707964f));
			float num3 = (float)Math.Cos((double)(num * 1.5707964f));
			return new StereoSamplePair
			{
				Left = num2,
				Right = num3
			};
		}

		// Token: 0x04000988 RID: 2440
		private const float HalfPi = 1.5707964f;
	}
}
