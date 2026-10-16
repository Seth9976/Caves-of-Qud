using System;

namespace FuzzySharp.SimilarityRatio.Scorer.Generic
{
	// Token: 0x020001C9 RID: 457
	public interface IRatioScorer<in T> where T : IEquatable<T>
	{
		// Token: 0x06001083 RID: 4227
		int Score(T[] input1, T[] input2);
	}
}
