using System;
using FuzzySharp.PreProcess;

namespace FuzzySharp.SimilarityRatio.Scorer
{
	// Token: 0x020001B3 RID: 435
	public interface IRatioScorer
	{
		// Token: 0x06001053 RID: 4179
		int Score(string input1, string input2);

		// Token: 0x06001054 RID: 4180
		int Score(string input1, string input2, PreprocessMode preprocessMode);
	}
}
