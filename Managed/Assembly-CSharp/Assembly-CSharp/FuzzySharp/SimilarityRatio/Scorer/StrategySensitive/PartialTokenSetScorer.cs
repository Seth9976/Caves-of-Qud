using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
	// Token: 0x020001C2 RID: 450
	public class PartialTokenSetScorer : TokenSetScorerBase
	{
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x00065918 File Offset: 0x00063B18
		protected override Func<string, string, int> Scorer
		{
			get
			{
				return new Func<string, string, int>(PartialRatioStrategy.Calculate);
			}
		}
	}
}
