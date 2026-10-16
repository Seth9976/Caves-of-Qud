using System;

namespace FuzzySharp.SimilarityRatio.Strategy
{
	// Token: 0x020001AF RID: 431
	internal class DefaultRatioStrategy
	{
		// Token: 0x0600104B RID: 4171 RVA: 0x0006524D File Offset: 0x0006344D
		public static int Calculate(string input1, string input2)
		{
			if (input1.Length == 0 || input2.Length == 0)
			{
				return 0;
			}
			return (int)Math.Round(100.0 * Levenshtein.GetRatio(input1, input2));
		}
	}
}
