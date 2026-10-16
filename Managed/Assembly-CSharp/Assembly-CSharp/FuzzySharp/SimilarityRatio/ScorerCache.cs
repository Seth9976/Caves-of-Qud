using System;
using System.Collections.Concurrent;
using FuzzySharp.SimilarityRatio.Scorer;

namespace FuzzySharp.SimilarityRatio
{
	// Token: 0x020001AE RID: 430
	public static class ScorerCache
	{
		// Token: 0x06001049 RID: 4169 RVA: 0x00065221 File Offset: 0x00063421
		public static IRatioScorer Get<T>() where T : IRatioScorer, new()
		{
			return ScorerCache.s_scorerCache.GetOrAdd(typeof(T), new T());
		}

		// Token: 0x04000B04 RID: 2820
		private static readonly ConcurrentDictionary<Type, IRatioScorer> s_scorerCache = new ConcurrentDictionary<Type, IRatioScorer>();
	}
}
