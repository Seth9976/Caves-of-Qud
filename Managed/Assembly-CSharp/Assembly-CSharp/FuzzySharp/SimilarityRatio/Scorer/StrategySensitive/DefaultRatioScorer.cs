using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
	// Token: 0x020001B5 RID: 437
	public class DefaultRatioScorer : SimpleRatioScorerBase
	{
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x00065491 File Offset: 0x00063691
		protected override Func<string, string, int> Scorer
		{
			get
			{
				return new Func<string, string, int>(DefaultRatioStrategy.Calculate);
			}
		}
	}
}
