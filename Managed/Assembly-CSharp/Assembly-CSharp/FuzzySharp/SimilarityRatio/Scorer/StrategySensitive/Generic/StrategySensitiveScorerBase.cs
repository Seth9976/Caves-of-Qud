using System;
using FuzzySharp.SimilarityRatio.Scorer.Generic;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive.Generic
{
	// Token: 0x020001C8 RID: 456
	public abstract class StrategySensitiveScorerBase<T> : ScorerBase<T> where T : IEquatable<T>
	{
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06001081 RID: 4225
		protected abstract Func<T[], T[], int> Scorer { get; }
	}
}
