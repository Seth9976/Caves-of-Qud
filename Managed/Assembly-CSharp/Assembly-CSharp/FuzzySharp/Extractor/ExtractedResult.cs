using System;
using System.Collections.Generic;

namespace FuzzySharp.Extractor
{
	// Token: 0x020001CE RID: 462
	public class ExtractedResult<T> : IComparable<ExtractedResult<T>>
	{
		// Token: 0x0600108D RID: 4237 RVA: 0x00065DAC File Offset: 0x00063FAC
		public ExtractedResult(T value, int score)
		{
			this.Value = value;
			this.Score = score;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00065DC2 File Offset: 0x00063FC2
		public ExtractedResult(T value, int score, int index)
		{
			this.Value = value;
			this.Score = score;
			this.Index = index;
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00065DDF File Offset: 0x00063FDF
		public int CompareTo(ExtractedResult<T> other)
		{
			return Comparer<int>.Default.Compare(this.Score, other.Score);
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00065DF8 File Offset: 0x00063FF8
		public override string ToString()
		{
			if (typeof(T) == typeof(string))
			{
				return string.Format("(string: {0}, score: {1}, index: {2})", this.Value, this.Score, this.Index);
			}
			string text = "(value: {0}, score: {1}, index: {2})";
			T value = this.Value;
			return string.Format(text, value.ToString(), this.Score, this.Index);
		}

		// Token: 0x04000B0C RID: 2828
		public readonly T Value;

		// Token: 0x04000B0D RID: 2829
		public readonly int Score;

		// Token: 0x04000B0E RID: 2830
		public readonly int Index;
	}
}
