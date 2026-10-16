using System;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
	// Token: 0x020001B8 RID: 440
	public abstract class StrategySensitiveScorerBase : ScorerBase
	{
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600105E RID: 4190
		protected abstract Func<string, string, int> Scorer { get; }
	}
}
