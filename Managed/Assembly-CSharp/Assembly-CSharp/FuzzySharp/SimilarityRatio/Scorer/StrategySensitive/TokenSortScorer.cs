using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
	// Token: 0x020001C7 RID: 455
	public class TokenSortScorer : TokenSortScorerBase
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x00065BC8 File Offset: 0x00063DC8
		protected override Func<string, string, int> Scorer
		{
			get
			{
				return new Func<string, string, int>(DefaultRatioStrategy.Calculate);
			}
		}
	}
}
