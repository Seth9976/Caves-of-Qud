using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FuzzySharp.SimilarityRatio.Scorer.StrategySensitive
{
	// Token: 0x020001C4 RID: 452
	public abstract class TokenSetScorerBase : StrategySensitiveScorerBase
	{
		// Token: 0x06001079 RID: 4217 RVA: 0x00065944 File Offset: 0x00063B44
		public override int Score(string input1, string input2)
		{
			HashSet<string> hashSet = new HashSet<string>(from s in Regex.Split(input1, "\\s+")
				where s.Any<char>()
				select s);
			HashSet<string> hashSet2 = new HashSet<string>(from s in Regex.Split(input2, "\\s+")
				where s.Any<char>()
				select s);
			string text = string.Join(" ", from s in hashSet.Intersect<string>(hashSet2)
				orderby s
				select s).Trim();
			string text2 = (text + " " + string.Join(" ", from s in hashSet.Except<string>(hashSet2)
				orderby s
				select s)).Trim();
			string text3 = (text + " " + string.Join(" ", from s in hashSet2.Except<string>(hashSet)
				orderby s
				select s)).Trim();
			return new int[]
			{
				this.Scorer(text, text2),
				this.Scorer(text, text3),
				this.Scorer(text2, text3)
			}.Max();
		}
	}
}
