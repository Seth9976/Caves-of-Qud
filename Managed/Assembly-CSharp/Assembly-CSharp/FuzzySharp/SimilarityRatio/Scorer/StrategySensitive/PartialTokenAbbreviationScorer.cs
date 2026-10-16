using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
	// Token: 0x020001B9 RID: 441
	public class PartialTokenAbbreviationScorer : TokenAbbreviationScorerBase
	{
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x000654DC File Offset: 0x000636DC
		protected override Func<string, string, int> Scorer
		{
			get
			{
				return new Func<string, string, int>(PartialRatioStrategy.Calculate);
			}
		}
	}
}
