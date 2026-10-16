using System;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
	// Token: 0x020001B7 RID: 439
	public abstract class SimpleRatioScorerBase : StrategySensitiveScorerBase
	{
		// Token: 0x0600105C RID: 4188 RVA: 0x000654BD File Offset: 0x000636BD
		public override int Score(string input1, string input2)
		{
			return this.Scorer(input1, input2);
		}
	}
}
