using System;
using System.Collections.Generic;
using System.Linq;
using FuzzySharp.Extensions;
using FuzzySharp.SimilarityRatio.Scorer;

namespace FuzzySharp.Extractor
{
	// Token: 0x020001CF RID: 463
	public static class ResultExtractor
	{
		// Token: 0x06001091 RID: 4241 RVA: 0x00065E80 File Offset: 0x00064080
		public static IEnumerable<ExtractedResult<T>> ExtractWithoutOrder<T>(T query, IEnumerable<T> choices, Func<T, string> processor, IRatioScorer scorer, int cutoff = 0)
		{
			int index = 0;
			string processedQuery = processor(query);
			foreach (T t in choices)
			{
				int num = scorer.Score(processedQuery, processor(t));
				if (num >= cutoff)
				{
					yield return new ExtractedResult<T>(t, num, index);
				}
				int num2 = index;
				index = num2 + 1;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00065EAD File Offset: 0x000640AD
		public static ExtractedResult<T> ExtractOne<T>(T query, IEnumerable<T> choices, Func<T, string> processor, IRatioScorer calculator, int cutoff = 0)
		{
			return ResultExtractor.ExtractWithoutOrder<T>(query, choices, processor, calculator, cutoff).Max<ExtractedResult<T>>();
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00065EBF File Offset: 0x000640BF
		public static IEnumerable<ExtractedResult<T>> ExtractSorted<T>(T query, IEnumerable<T> choices, Func<T, string> processor, IRatioScorer calculator, int cutoff = 0)
		{
			return from r in ResultExtractor.ExtractWithoutOrder<T>(query, choices, processor, calculator, cutoff)
				orderby r.Score descending
				select r;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00065EF0 File Offset: 0x000640F0
		public static IEnumerable<ExtractedResult<T>> ExtractTop<T>(T query, IEnumerable<T> choices, Func<T, string> processor, IRatioScorer calculator, int limit, int cutoff = 0)
		{
			return ResultExtractor.ExtractWithoutOrder<T>(query, choices, processor, calculator, cutoff).MaxN<ExtractedResult<T>>(limit).Reverse<ExtractedResult<T>>();
		}
	}
}
