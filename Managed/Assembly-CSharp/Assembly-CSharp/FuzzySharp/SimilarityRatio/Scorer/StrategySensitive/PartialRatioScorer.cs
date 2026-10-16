using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
	// Token: 0x020001B6 RID: 438
	public class PartialRatioScorer : SimpleRatioScorerBase
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x000654A7 File Offset: 0x000636A7
		protected override Func<string, string, int> Scorer
		{
			get
			{
				return new Func<string, string, int>(PartialRatioStrategy.Calculate);
			}
		}
	}
}
