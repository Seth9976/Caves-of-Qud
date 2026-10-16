using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002F2 RID: 754
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SubText : SourceText
	{
		// Token: 0x0600203F RID: 8255 RVA: 0x00063994 File Offset: 0x00061B94
		public SubText(SourceText text, TextSpan span)
		{
			SourceHashAlgorithm checksumAlgorithm = text.ChecksumAlgorithm;
			base..ctor(default(ImmutableArray<byte>), checksumAlgorithm, null);
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			if (span.Start < 0 || span.Start >= text.Length || span.End < 0 || span.End > text.Length)
			{
				throw new ArgumentOutOfRangeException("span");
			}
			this.UnderlyingText = text;
			this.UnderlyingSpan = span;
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06002040 RID: 8256 RVA: 0x00063A12 File Offset: 0x00061C12
		[Nullable(2)]
		public override Encoding Encoding
		{
			[NullableContext(2)]
			get
			{
				return this.UnderlyingText.Encoding;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06002041 RID: 8257 RVA: 0x00063A1F File Offset: 0x00061C1F
		public SourceText UnderlyingText { get; }

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06002042 RID: 8258 RVA: 0x00063A27 File Offset: 0x00061C27
		public TextSpan UnderlyingSpan { get; }

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x00063A30 File Offset: 0x00061C30
		public override int Length
		{
			get
			{
				return this.UnderlyingSpan.Length;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06002044 RID: 8260 RVA: 0x00063A4B File Offset: 0x00061C4B
		internal override int StorageSize
		{
			get
			{
				return this.UnderlyingText.StorageSize;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x00063A58 File Offset: 0x00061C58
		internal override SourceText StorageKey
		{
			get
			{
				return this.UnderlyingText.StorageKey;
			}
		}

		// Token: 0x17000844 RID: 2116
		public override char this[int position]
		{
			get
			{
				if (position < 0 || position > this.Length)
				{
					throw new ArgumentOutOfRangeException("position");
				}
				return this.UnderlyingText[this.UnderlyingSpan.Start + position];
			}
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x00063AA8 File Offset: 0x00061CA8
		public override string ToString(TextSpan span)
		{
			base.CheckSubSpan(span);
			return this.UnderlyingText.ToString(this.GetCompositeSpan(span.Start, span.Length));
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x00063AD0 File Offset: 0x00061CD0
		public override SourceText GetSubText(TextSpan span)
		{
			base.CheckSubSpan(span);
			return new SubText(this.UnderlyingText, this.GetCompositeSpan(span.Start, span.Length));
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x00063AF8 File Offset: 0x00061CF8
		public override void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			TextSpan compositeSpan = this.GetCompositeSpan(sourceIndex, count);
			this.UnderlyingText.CopyTo(compositeSpan.Start, destination, destinationIndex, compositeSpan.Length);
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x00063B2C File Offset: 0x00061D2C
		private TextSpan GetCompositeSpan(int start, int length)
		{
			int num = Math.Min(this.UnderlyingText.Length, this.UnderlyingSpan.Start + start);
			int num2 = Math.Min(this.UnderlyingText.Length, num + length);
			return new TextSpan(num, num2 - num);
		}
	}
}
