using System;
using System.Linq;

namespace FuzzySharp.SimilarityRatio.Scorer.Composite
{
	// Token: 0x020001CB RID: 459
	public class WeightedRatioScorer : ScorerBase
	{
		// Token: 0x06001086 RID: 4230 RVA: 0x00065BF0 File Offset: 0x00063DF0
		public override int Score(string input1, string input2)
		{
			int length = input1.Length;
			int length2 = input2.Length;
			if (length == 0 || length2 == 0)
			{
				return 0;
			}
			bool flag = WeightedRatioScorer.TRY_PARTIALS;
			double unbase_SCALE = WeightedRatioScorer.UNBASE_SCALE;
			double num = WeightedRatioScorer.PARTIAL_SCALE;
			int num2 = Fuzz.Ratio(input1, input2);
			double num3 = (double)Math.Max(length, length2) / (double)Math.Min(length, length2);
			if (num3 < 1.5)
			{
				flag = false;
			}
			if (num3 > 8.0)
			{
				num = 0.6;
			}
			if (flag)
			{
				double num4 = (double)Fuzz.PartialRatio(input1, input2) * num;
				double num5 = (double)Fuzz.TokenSortRatio(input1, input2) * unbase_SCALE * num;
				double num6 = (double)Fuzz.TokenSetRatio(input1, input2) * unbase_SCALE * num;
				return (int)Math.Round(new double[]
				{
					(double)num2,
					num4,
					num5,
					num6
				}.Max());
			}
			double num7 = (double)Fuzz.TokenSortRatio(input1, input2) * unbase_SCALE;
			double num8 = (double)Fuzz.TokenSetRatio(input1, input2) * unbase_SCALE;
			return (int)Math.Round(new double[]
			{
				(double)num2,
				num7,
				num8
			}.Max());
		}

		// Token: 0x04000B05 RID: 2821
		private static double UNBASE_SCALE = 0.95;

		// Token: 0x04000B06 RID: 2822
		private static double PARTIAL_SCALE = 0.9;

		// Token: 0x04000B07 RID: 2823
		private static bool TRY_PARTIALS = true;
	}
}
