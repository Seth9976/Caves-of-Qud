using System;
using FuzzySharp.SimilarityRatio.Strategy.Generic;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
	// Token: 0x020001BC RID: 444
	public class PartialTokenDifferenceScorer : TokenDifferenceScorerBase
	{
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x0006570C File Offset: 0x0006390C
		protected override Func<string[], string[], int> Scorer
		{
			get
			{
				return new Func<string[], string[], int>(PartialRatioStrategy<string>.Calculate);
			}
		}
	}
}
