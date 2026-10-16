using System;
using FuzzySharp.SimilarityRatio.Strategy.Generic;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
	// Token: 0x020001BD RID: 445
	public class TokenDifferenceScorer : TokenDifferenceScorerBase
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x00065722 File Offset: 0x00063922
		protected override Func<string[], string[], int> Scorer
		{
			get
			{
				return new Func<string[], string[], int>(DefaultRatioStrategy<string>.Calculate);
			}
		}
	}
}
