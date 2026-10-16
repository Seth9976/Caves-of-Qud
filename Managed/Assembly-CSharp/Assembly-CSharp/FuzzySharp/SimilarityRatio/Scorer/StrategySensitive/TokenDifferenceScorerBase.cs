using System;
using System.Linq;
using System.Text.RegularExpressions;
using FuzzySharp.PreProcess;
using FuzzySharp.SimilarityRatio.Scorer.StrategySensitive.Generic;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
	// Token: 0x020001BE RID: 446
	public abstract class TokenDifferenceScorerBase : StrategySensitiveScorerBase<string>, IRatioScorer
	{
		// Token: 0x0600106B RID: 4203 RVA: 0x00065738 File Offset: 0x00063938
		public override int Score(string[] input1, string[] input2)
		{
			return this.Scorer(input1, input2);
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00065748 File Offset: 0x00063948
		public int Score(string input1, string input2)
		{
			string[] array = (from s in Regex.Split(input1, "\\s+")
				where s.Any<char>()
				orderby s
				select s).ToArray<string>();
			string[] array2 = (from s in Regex.Split(input2, "\\s+")
				where s.Any<char>()
				orderby s
				select s).ToArray<string>();
			return this.Score(array, array2);
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0006580F File Offset: 0x00063A0F
		public int Score(string input1, string input2, PreprocessMode preprocessMode)
		{
			Func<string, string> preprocessor = StringPreprocessorFactory.GetPreprocessor(preprocessMode);
			input1 = preprocessor(input1);
			input2 = preprocessor(input2);
			return this.Score(input1, input2);
		}
	}
}
