using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
	// Token: 0x020001C5 RID: 453
	public class PartialTokenSortScorer : TokenSortScorerBase
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x00065AC8 File Offset: 0x00063CC8
		protected override Func<string, string, int> Scorer
		{
			get
			{
				return new Func<string, string, int>(PartialRatioStrategy.Calculate);
			}
		}
	}
}
