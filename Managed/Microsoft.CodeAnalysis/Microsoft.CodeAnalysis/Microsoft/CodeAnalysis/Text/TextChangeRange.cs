using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002F5 RID: 757
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	public readonly struct TextChangeRange : IEquatable<TextChangeRange>
	{
		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x0600205C RID: 8284 RVA: 0x00063D7F File Offset: 0x00061F7F
		public TextSpan Span { get; }

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x0600205D RID: 8285 RVA: 0x00063D87 File Offset: 0x00061F87
		public int NewLength { get; }

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x0600205E RID: 8286 RVA: 0x00063D90 File Offset: 0x00061F90
		internal int NewEnd
		{
			get
			{
				return this.Span.Start + this.NewLength;
			}
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x00063DB2 File Offset: 0x00061FB2
		public TextChangeRange(TextSpan span, int newLength)
		{
			this = default(TextChangeRange);
			if (newLength < 0)
			{
				throw new ArgumentOutOfRangeException("newLength");
			}
			this.Span = span;
			this.NewLength = newLength;
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x00063DD8 File Offset: 0x00061FD8
		public bool Equals(TextChangeRange other)
		{
			return other.Span == this.Span && other.NewLength == this.NewLength;
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x00063E00 File Offset: 0x00062000
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is TextChangeRange)
			{
				TextChangeRange textChangeRange = (TextChangeRange)obj;
				return this.Equals(textChangeRange);
			}
			return false;
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x00063E28 File Offset: 0x00062028
		public override int GetHashCode()
		{
			return Hash.Combine(this.NewLength, this.Span.GetHashCode());
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x00063E54 File Offset: 0x00062054
		public static bool operator ==(TextChangeRange left, TextChangeRange right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x00063E5E File Offset: 0x0006205E
		public static bool operator !=(TextChangeRange left, TextChangeRange right)
		{
			return !(left == right);
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x00063E6A File Offset: 0x0006206A
		public static IReadOnlyList<TextChangeRange> NoChanges
		{
			get
			{
				return SpecializedCollections.EmptyReadOnlyList<TextChangeRange>();
			}
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x00063E74 File Offset: 0x00062074
		public static TextChangeRange Collapse(IEnumerable<TextChangeRange> changes)
		{
			int num = 0;
			int num2 = int.MaxValue;
			int num3 = 0;
			foreach (TextChangeRange textChangeRange in changes)
			{
				num += textChangeRange.NewLength - textChangeRange.Span.Length;
				if (textChangeRange.Span.Start < num2)
				{
					num2 = textChangeRange.Span.Start;
				}
				if (textChangeRange.Span.End > num3)
				{
					num3 = textChangeRange.Span.End;
				}
			}
			if (num2 > num3)
			{
				return default(TextChangeRange);
			}
			TextSpan textSpan = TextSpan.FromBounds(num2, num3);
			int num4 = textSpan.Length + num;
			return new TextChangeRange(textSpan, num4);
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x00063F54 File Offset: 0x00062154
		private string GetDebuggerDisplay()
		{
			return string.Format("new TextChangeRange(new TextSpan({0}, {1}), {2})", this.Span.Start, this.Span.Length, this.NewLength);
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x00063F9C File Offset: 0x0006219C
		public override string ToString()
		{
			return string.Format("TextChangeRange(Span={0}, NewLength={1})", this.Span, this.NewLength);
		}
	}
}
