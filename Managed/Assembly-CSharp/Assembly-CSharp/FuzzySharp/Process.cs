using System;
using System.Collections.Generic;
using FuzzySharp.Extractor;
using FuzzySharp.PreProcess;
using FuzzySharp.SimilarityRatio;
using FuzzySharp.SimilarityRatio.Scorer;
using FuzzySharp.SimilarityRatio.Scorer.Composite;

namespace FuzzySharp
{
	// Token: 0x020001A8 RID: 424
	public static class Process
	{
		// Token: 0x06001014 RID: 4116 RVA: 0x00064B17 File Offset: 0x00062D17
		public static IEnumerable<ExtractedResult<string>> ExtractAll(string query, IEnumerable<string> choices, Func<string, string> processor = null, IRatioScorer scorer = null, int cutoff = 0)
		{
			if (processor == null)
			{
				processor = Process.s_defaultStringProcessor;
			}
			if (scorer == null)
			{
				scorer = Process.s_defaultScorer;
			}
			return ResultExtractor.ExtractWithoutOrder<string>(query, choices, processor, scorer, cutoff);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00064B38 File Offset: 0x00062D38
		public static IEnumerable<ExtractedResult<T>> ExtractAll<T>(T query, IEnumerable<T> choices, Func<T, string> processor, IRatioScorer scorer = null, int cutoff = 0)
		{
			if (scorer == null)
			{
				scorer = Process.s_defaultScorer;
			}
			return ResultExtractor.ExtractWithoutOrder<T>(query, choices, processor, scorer, cutoff);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00064B4F File Offset: 0x00062D4F
		public static IEnumerable<ExtractedResult<string>> ExtractTop(string query, IEnumerable<string> choices, Func<string, string> processor = null, IRatioScorer scorer = null, int limit = 5, int cutoff = 0)
		{
			if (processor == null)
			{
				processor = Process.s_defaultStringProcessor;
			}
			if (scorer == null)
			{
				scorer = Process.s_defaultScorer;
			}
			return ResultExtractor.ExtractTop<string>(query, choices, processor, scorer, limit, cutoff);
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00064B72 File Offset: 0x00062D72
		public static IEnumerable<ExtractedResult<T>> ExtractTop<T>(T query, IEnumerable<T> choices, Func<T, string> processor, IRatioScorer scorer = null, int limit = 5, int cutoff = 0)
		{
			if (scorer == null)
			{
				scorer = Process.s_defaultScorer;
			}
			return ResultExtractor.ExtractTop<T>(query, choices, processor, scorer, limit, cutoff);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00064B8B File Offset: 0x00062D8B
		public static IEnumerable<ExtractedResult<string>> ExtractSorted(string query, IEnumerable<string> choices, Func<string, string> processor = null, IRatioScorer scorer = null, int cutoff = 0)
		{
			if (processor == null)
			{
				processor = Process.s_defaultStringProcessor;
			}
			if (scorer == null)
			{
				scorer = Process.s_defaultScorer;
			}
			return ResultExtractor.ExtractSorted<string>(query, choices, processor, scorer, cutoff);
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00064BAC File Offset: 0x00062DAC
		public static IEnumerable<ExtractedResult<T>> ExtractSorted<T>(T query, IEnumerable<T> choices, Func<T, string> processor, IRatioScorer scorer = null, int cutoff = 0)
		{
			if (scorer == null)
			{
				scorer = Process.s_defaultScorer;
			}
			return ResultExtractor.ExtractSorted<T>(query, choices, processor, scorer, cutoff);
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00064BC3 File Offset: 0x00062DC3
		public static ExtractedResult<string> ExtractOne(string query, IEnumerable<string> choices, Func<string, string> processor = null, IRatioScorer scorer = null, int cutoff = 0)
		{
			if (processor == null)
			{
				processor = Process.s_defaultStringProcessor;
			}
			if (scorer == null)
			{
				scorer = Process.s_defaultScorer;
			}
			return ResultExtractor.ExtractOne<string>(query, choices, processor, scorer, cutoff);
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x00064BE4 File Offset: 0x00062DE4
		public static ExtractedResult<T> ExtractOne<T>(T query, IEnumerable<T> choices, Func<T, string> processor, IRatioScorer scorer = null, int cutoff = 0)
		{
			if (scorer == null)
			{
				scorer = Process.s_defaultScorer;
			}
			return ResultExtractor.ExtractOne<T>(query, choices, processor, scorer, cutoff);
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00064BFB File Offset: 0x00062DFB
		public static ExtractedResult<string> ExtractOne(string query, params string[] choices)
		{
			return ResultExtractor.ExtractOne<string>(query, choices, Process.s_defaultStringProcessor, Process.s_defaultScorer, 0);
		}

		// Token: 0x04000AFA RID: 2810
		private static readonly IRatioScorer s_defaultScorer = ScorerCache.Get<WeightedRatioScorer>();

		// Token: 0x04000AFB RID: 2811
		private static readonly Func<string, string> s_defaultStringProcessor = StringPreprocessorFactory.GetPreprocessor(PreprocessMode.Full);
	}
}
