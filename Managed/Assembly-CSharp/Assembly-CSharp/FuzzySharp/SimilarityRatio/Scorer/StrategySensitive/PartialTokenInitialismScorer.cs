using System;
using FuzzySharp.SimilarityRatio.Strategy;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
	// Token: 0x020001BF RID: 447
	public class PartialTokenInitialismScorer : TokenInitialismScorerBase
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x00065838 File Offset: 0x00063A38
		protected override Func<string, string, int> Scorer
		{
			get
			{
				return new Func<string, string, int>(PartialRatioStrategy.Calculate);
			}
		}
	}
}
