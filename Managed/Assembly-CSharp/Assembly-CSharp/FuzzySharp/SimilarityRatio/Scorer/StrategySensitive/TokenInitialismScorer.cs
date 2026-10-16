using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
	// Token: 0x020001C0 RID: 448
	public class TokenInitialismScorer : TokenInitialismScorerBase
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x0006584E File Offset: 0x00063A4E
		protected override Func<string, string, int> Scorer
		{
			get
			{
				return new Func<string, string, int>(DefaultRatioStrategy.Calculate);
			}
		}
	}
}
