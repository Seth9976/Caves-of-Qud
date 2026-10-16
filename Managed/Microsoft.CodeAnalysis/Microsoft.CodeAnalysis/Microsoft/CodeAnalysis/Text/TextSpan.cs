using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002F8 RID: 760
	[DataContract]
	public readonly struct TextSpan : IEquatable<TextSpan>, IComparable<TextSpan>
	{
		// Token: 0x06002085 RID: 8325 RVA: 0x00064336 File Offset: 0x00062536
		public TextSpan(int start, int length)
		{
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException("start");
			}
			if (start + length < start)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			this.Start = start;
			this.Length = length;
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06002086 RID: 8326 RVA: 0x00064366 File Offset: 0x00062566
		[DataMember(Order = 0)]
		public int Start { get; }

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06002087 RID: 8327 RVA: 0x0006436E File Offset: 0x0006256E
		public int End
		{
			get
			{
				return this.Start + this.Length;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06002088 RID: 8328 RVA: 0x0006437D File Offset: 0x0006257D
		[DataMember(Order = 1)]
		public int Length { get; }

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06002089 RID: 8329 RVA: 0x00064385 File Offset: 0x00062585
		public bool IsEmpty
		{
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x00064390 File Offset: 0x00062590
		public bool Contains(int position)
		{
			return position - this.Start < this.Length;
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x000643A2 File Offset: 0x000625A2
		public bool Contains(TextSpan span)
		{
			return span.Start >= this.Start && span.End <= this.End;
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x000643C8 File Offset: 0x000625C8
		public bool OverlapsWith(TextSpan span)
		{
			int num = Math.Max(this.Start, span.Start);
			int num2 = Math.Min(this.End, span.End);
			return num < num2;
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x00064400 File Offset: 0x00062600
		public TextSpan? Overlap(TextSpan span)
		{
			int num = Math.Max(this.Start, span.Start);
			int num2 = Math.Min(this.End, span.End);
			if (num >= num2)
			{
				return default(TextSpan?);
			}
			return new TextSpan?(TextSpan.FromBounds(num, num2));
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x0006444D File Offset: 0x0006264D
		public bool IntersectsWith(TextSpan span)
		{
			return span.Start <= this.End && span.End >= this.Start;
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x00064472 File Offset: 0x00062672
		public bool IntersectsWith(int position)
		{
			return position - this.Start <= this.Length;
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x00064488 File Offset: 0x00062688
		public TextSpan? Intersection(TextSpan span)
		{
			int num = Math.Max(this.Start, span.Start);
			int num2 = Math.Min(this.End, span.End);
			if (num > num2)
			{
				return default(TextSpan?);
			}
			return new TextSpan?(TextSpan.FromBounds(num, num2));
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x000644D5 File Offset: 0x000626D5
		public static TextSpan FromBounds(int start, int end)
		{
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException("start", CodeAnalysisResources.StartMustNotBeNegative);
			}
			if (end < start)
			{
				throw new ArgumentOutOfRangeException("end", CodeAnalysisResources.EndMustNotBeLessThanStart);
			}
			return new TextSpan(start, end - start);
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x00064508 File Offset: 0x00062708
		public static bool operator ==(TextSpan left, TextSpan right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x00064512 File Offset: 0x00062712
		public static bool operator !=(TextSpan left, TextSpan right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x0006451F File Offset: 0x0006271F
		public bool Equals(TextSpan other)
		{
			return this.Start == other.Start && this.Length == other.Length;
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x00064541 File Offset: 0x00062741
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is TextSpan && this.Equals((TextSpan)obj);
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x00064559 File Offset: 0x00062759
		public override int GetHashCode()
		{
			return Hash.Combine(this.Start, this.Length);
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x0006456C File Offset: 0x0006276C
		[NullableContext(1)]
		public override string ToString()
		{
			return string.Format("[{0}..{1})", this.Start, this.End);
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x00064590 File Offset: 0x00062790
		public int CompareTo(TextSpan other)
		{
			int num = this.Start - other.Start;
			if (num != 0)
			{
				return num;
			}
			return this.Length - other.Length;
		}
	}
}
