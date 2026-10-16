using System;
using FuzzySharp.PreProcess;

namespace FuzzySharp.SimilarityRatio.Scorer
{
	// Token: 0x020001B4 RID: 436
	public abstract class ScorerBase : IRatioScorer
	{
		// Token: 0x06001055 RID: 4181
		public abstract int Score(string input1, string input2);

		// Token: 0x06001056 RID: 4182 RVA: 0x00065468 File Offset: 0x00063668
		public int Score(string input1, string input2, PreprocessMode preprocessMode)
		{
			Func<string, string> preprocessor = StringPreprocessorFactory.GetPreprocessor(preprocessMode);
			input1 = preprocessor(input1);
			input2 = preprocessor(input2);
			return this.Score(input1, input2);
		}
	}
}
