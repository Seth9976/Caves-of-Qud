using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
	// Token: 0x020001C3 RID: 451
	public class TokenSetScorer : TokenSetScorerBase
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x0006592E File Offset: 0x00063B2E
		protected override Func<string, string, int> Scorer
		{
			get
			{
				return new Func<string, string, int>(DefaultRatioStrategy.Calculate);
			}
		}
	}
}
