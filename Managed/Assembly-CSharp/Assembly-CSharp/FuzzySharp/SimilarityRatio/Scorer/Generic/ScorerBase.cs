using System;

namespace FuzzySharp.SimilarityRatio.Scorer.Generic
{
	// Token: 0x020001CA RID: 458
	public abstract class ScorerBase<T> : IRatioScorer<T> where T : IEquatable<T>
	{
		// Token: 0x06001084 RID: 4228
		public abstract int Score(T[] input1, T[] input2);
	}
}
