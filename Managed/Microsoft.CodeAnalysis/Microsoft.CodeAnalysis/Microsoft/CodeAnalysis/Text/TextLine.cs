using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002F6 RID: 758
	[NullableContext(2)]
	[Nullable(0)]
	public readonly struct TextLine : IEquatable<TextLine>
	{
		// Token: 0x06002069 RID: 8297 RVA: 0x00063FBE File Offset: 0x000621BE
		[NullableContext(1)]
		private TextLine(SourceText text, int start, int endIncludingBreaks)
		{
			this._text = text;
			this._start = start;
			this._endIncludingBreaks = endIncludingBreaks;
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x00063FD8 File Offset: 0x000621D8
		[NullableContext(1)]
		public static TextLine FromSpan(SourceText text, TextSpan span)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			if (span.Start > text.Length || span.Start < 0 || span.End > text.Length)
			{
				throw new ArgumentOutOfRangeException("span");
			}
			if (text.Length <= 0)
			{
				return new TextLine(text, 0, 0);
			}
			if (span.Start > 0 && !TextUtilities.IsAnyLineBreakCharacter(text[span.Start - 1]))
			{
				throw new ArgumentOutOfRangeException("span", CodeAnalysisResources.SpanDoesNotIncludeStartOfLine);
			}
			bool flag = false;
			if (span.End > span.Start)
			{
				flag = TextUtilities.IsAnyLineBreakCharacter(text[span.End - 1]);
			}
			if (!flag && span.End < text.Length)
			{
				int lengthOfLineBreak = TextUtilities.GetLengthOfLineBreak(text, span.End);
				if (lengthOfLineBreak > 0)
				{
					flag = true;
					span = new TextSpan(span.Start, span.Length + lengthOfLineBreak);
				}
			}
			if (span.End < text.Length && !flag)
			{
				throw new ArgumentOutOfRangeException("span", CodeAnalysisResources.SpanDoesNotIncludeEndOfLine);
			}
			return new TextLine(text, span.Start, span.End);
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x0600206B RID: 8299 RVA: 0x00064105 File Offset: 0x00062305
		public SourceText Text
		{
			get
			{
				return this._text;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x0006410D File Offset: 0x0006230D
		public int LineNumber
		{
			get
			{
				SourceText text = this._text;
				if (text == null)
				{
					return 0;
				}
				return text.Lines.IndexOf(this._start);
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x0600206D RID: 8301 RVA: 0x0006412B File Offset: 0x0006232B
		public int Start
		{
			get
			{
				return this._start;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x0600206E RID: 8302 RVA: 0x00064133 File Offset: 0x00062333
		public int End
		{
			get
			{
				return this._endIncludingBreaks - this.LineBreakLength;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x0600206F RID: 8303 RVA: 0x00064144 File Offset: 0x00062344
		private int LineBreakLength
		{
			get
			{
				if (this._text == null || this._text.Length == 0 || this._endIncludingBreaks == this._start)
				{
					return 0;
				}
				int num;
				int num2;
				TextUtilities.GetStartAndLengthOfLineBreakEndingAt(this._text, this._endIncludingBreaks - 1, out num, out num2);
				return num2;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06002070 RID: 8304 RVA: 0x0006418E File Offset: 0x0006238E
		public int EndIncludingLineBreak
		{
			get
			{
				return this._endIncludingBreaks;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06002071 RID: 8305 RVA: 0x00064196 File Offset: 0x00062396
		public TextSpan Span
		{
			get
			{
				return TextSpan.FromBounds(this.Start, this.End);
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06002072 RID: 8306 RVA: 0x000641A9 File Offset: 0x000623A9
		public TextSpan SpanIncludingLineBreak
		{
			get
			{
				return TextSpan.FromBounds(this.Start, this.EndIncludingLineBreak);
			}
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x000641BC File Offset: 0x000623BC
		[NullableContext(1)]
		public override string ToString()
		{
			if (this._text == null || this._text.Length == 0)
			{
				return string.Empty;
			}
			return this._text.ToString(this.Span);
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x000641EA File Offset: 0x000623EA
		public static bool operator ==(TextLine left, TextLine right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x000641F4 File Offset: 0x000623F4
		public static bool operator !=(TextLine left, TextLine right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x00064201 File Offset: 0x00062401
		public bool Equals(TextLine other)
		{
			return other._text == this._text && other._start == this._start && other._endIncludingBreaks == this._endIncludingBreaks;
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x0006422F File Offset: 0x0006242F
		public override bool Equals(object obj)
		{
			return obj is TextLine && this.Equals((TextLine)obj);
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x00064247 File Offset: 0x00062447
		public override int GetHashCode()
		{
			return Hash.Combine<SourceText>(this._text, Hash.Combine(this._start, this._endIncludingBreaks));
		}

		// Token: 0x04000E29 RID: 3625
		private readonly SourceText _text;

		// Token: 0x04000E2A RID: 3626
		private readonly int _start;

		// Token: 0x04000E2B RID: 3627
		private readonly int _endIncludingBreaks;
	}
}
