using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
	// Token: 0x020001C6 RID: 454
	public abstract class TokenSortScorerBase : StrategySensitiveScorerBase
	{
		// Token: 0x0600107D RID: 4221 RVA: 0x00065AE0 File Offset: 0x00063CE0
		public override int Score(string input1, string input2)
		{
			string text = string.Join(" ", from s in Regex.Split(input1, "\\s+")
				where s.Any<char>()
				orderby s
				select s).Trim();
			string text2 = string.Join(" ", from s in Regex.Split(input2, "\\s+")
				where s.Any<char>()
				orderby s
				select s).Trim();
			return this.Scorer(text, text2);
		}
	}
}
