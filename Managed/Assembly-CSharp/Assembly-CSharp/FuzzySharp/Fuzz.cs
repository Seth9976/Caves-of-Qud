using System;
using FuzzySharp.PreProcess;
using FuzzySharp.SimilarityRatio;
using FuzzySharp.SimilarityRatio.Scorer.Composite;
using FuzzySharp.SimilarityRatio.Scorer.StrategySensitive;

namespace FuzzySharp
{
	// Token: 0x020001A6 RID: 422
	public static class Fuzz
	{
		// Token: 0x06000FEB RID: 4075 RVA: 0x00063964 File Offset: 0x00061B64
		public static int Ratio(string input1, string input2)
		{
			return ScorerCache.Get<DefaultRatioScorer>().Score(input1, input2);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00063972 File Offset: 0x00061B72
		public static int Ratio(string input1, string input2, PreprocessMode preprocessMode)
		{
			return ScorerCache.Get<DefaultRatioScorer>().Score(input1, input2, preprocessMode);
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00063981 File Offset: 0x00061B81
		public static int PartialRatio(string input1, string input2)
		{
			return ScorerCache.Get<PartialRatioScorer>().Score(input1, input2);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0006398F File Offset: 0x00061B8F
		public static int PartialRatio(string input1, string input2, PreprocessMode preprocessMode)
		{
			return ScorerCache.Get<PartialRatioScorer>().Score(input1, input2, preprocessMode);
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0006399E File Offset: 0x00061B9E
		public static int TokenSortRatio(string input1, string input2)
		{
			return ScorerCache.Get<TokenSortScorer>().Score(input1, input2);
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x000639AC File Offset: 0x00061BAC
		public static int TokenSortRatio(string input1, string input2, PreprocessMode preprocessMode)
		{
			return ScorerCache.Get<TokenSortScorer>().Score(input1, input2, preprocessMode);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x000639BB File Offset: 0x00061BBB
		public static int PartialTokenSortRatio(string input1, string input2)
		{
			return ScorerCache.Get<PartialTokenSortScorer>().Score(input1, input2);
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x000639C9 File Offset: 0x00061BC9
		public static int PartialTokenSortRatio(string input1, string input2, PreprocessMode preprocessMode)
		{
			return ScorerCache.Get<PartialTokenSortScorer>().Score(input1, input2, preprocessMode);
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x000639D8 File Offset: 0x00061BD8
		public static int TokenSetRatio(string input1, string input2)
		{
			return ScorerCache.Get<TokenSetScorer>().Score(input1, input2);
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x000639E6 File Offset: 0x00061BE6
		public static int TokenSetRatio(string input1, string input2, PreprocessMode preprocessMode)
		{
			return ScorerCache.Get<TokenSetScorer>().Score(input1, input2, preprocessMode);
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x000639F5 File Offset: 0x00061BF5
		public static int PartialTokenSetRatio(string input1, string input2)
		{
			return ScorerCache.Get<PartialTokenSetScorer>().Score(input1, input2);
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00063A03 File Offset: 0x00061C03
		public static int PartialTokenSetRatio(string input1, string input2, PreprocessMode preprocessMode)
		{
			return ScorerCache.Get<PartialTokenSetScorer>().Score(input1, input2, preprocessMode);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00063A12 File Offset: 0x00061C12
		public static int TokenDifferenceRatio(string input1, string input2)
		{
			return ScorerCache.Get<TokenDifferenceScorer>().Score(input1, input2);
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00063A20 File Offset: 0x00061C20
		public static int TokenDifferenceRatio(string input1, string input2, PreprocessMode preprocessMode)
		{
			return ScorerCache.Get<TokenDifferenceScorer>().Score(input1, input2, preprocessMode);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00063A2F File Offset: 0x00061C2F
		public static int PartialTokenDifferenceRatio(string input1, string input2)
		{
			return ScorerCache.Get<PartialTokenDifferenceScorer>().Score(input1, input2);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00063A3D File Offset: 0x00061C3D
		public static int PartialTokenDifferenceRatio(string input1, string input2, PreprocessMode preprocessMode)
		{
			return ScorerCache.Get<PartialTokenDifferenceScorer>().Score(input1, input2, preprocessMode);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00063A4C File Offset: 0x00061C4C
		public static int TokenInitialismRatio(string input1, string input2)
		{
			return ScorerCache.Get<TokenInitialismScorer>().Score(input1, input2);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00063A5A File Offset: 0x00061C5A
		public static int TokenInitialismRatio(string input1, string input2, PreprocessMode preprocessMode)
		{
			return ScorerCache.Get<TokenInitialismScorer>().Score(input1, input2, preprocessMode);
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00063A69 File Offset: 0x00061C69
		public static int PartialTokenInitialismRatio(string input1, string input2)
		{
			return ScorerCache.Get<PartialTokenInitialismScorer>().Score(input1, input2);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00063A77 File Offset: 0x00061C77
		public static int PartialTokenInitialismRatio(string input1, string input2, PreprocessMode preprocessMode)
		{
			return ScorerCache.Get<PartialTokenInitialismScorer>().Score(input1, input2);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00063A85 File Offset: 0x00061C85
		public static int TokenAbbreviationRatio(string input1, string input2)
		{
			return ScorerCache.Get<TokenAbbreviationScorer>().Score(input1, input2);
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00063A93 File Offset: 0x00061C93
		public static int TokenAbbreviationRatio(string input1, string input2, PreprocessMode preprocessMode)
		{
			return ScorerCache.Get<TokenAbbreviationScorer>().Score(input1, input2, preprocessMode);
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00063AA2 File Offset: 0x00061CA2
		public static int PartialTokenAbbreviationRatio(string input1, string input2)
		{
			return ScorerCache.Get<PartialTokenAbbreviationScorer>().Score(input1, input2);
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00063AB0 File Offset: 0x00061CB0
		public static int PartialTokenAbbreviationRatio(string input1, string input2, PreprocessMode preprocessMode)
		{
			return ScorerCache.Get<PartialTokenAbbreviationScorer>().Score(input1, input2, preprocessMode);
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00063ABF File Offset: 0x00061CBF
		public static int WeightedRatio(string input1, string input2)
		{
			return ScorerCache.Get<WeightedRatioScorer>().Score(input1, input2);
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00063ACD File Offset: 0x00061CCD
		public static int WeightedRatio(string input1, string input2, PreprocessMode preprocessMode)
		{
			return ScorerCache.Get<WeightedRatioScorer>().Score(input1, input2, preprocessMode);
		}
	}
}
