using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
	// Token: 0x020001BA RID: 442
	public class TokenAbbreviationScorer : TokenAbbreviationScorerBase
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x000654F2 File Offset: 0x000636F2
		protected override Func<string, string, int> Scorer
		{
			get
			{
				return new Func<string, string, int>(DefaultRatioStrategy.Calculate);
			}
		}
	}
}
