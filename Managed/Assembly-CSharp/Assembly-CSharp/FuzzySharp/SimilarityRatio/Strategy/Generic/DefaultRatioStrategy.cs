using System;

namespace FuzzySharp.SimilarityRatio.Strategy.Generic
{
	// Token: 0x020001B1 RID: 433
	internal class DefaultRatioStrategy<T> where T : IEquatable<T>
	{
		// Token: 0x0600104F RID: 4175 RVA: 0x00065366 File Offset: 0x00063566
		public static int Calculate(T[] input1, T[] input2)
		{
			if (input1.Length == 0 || input2.Length == 0)
			{
				return 0;
			}
			return (int)Math.Round(100.0 * Levenshtein.GetRatio<T>(input1, input2));
		}
	}
}
