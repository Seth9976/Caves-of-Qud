using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002EF RID: 751
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class StringBuilderText : SourceText
	{
		// Token: 0x0600202A RID: 8234 RVA: 0x00063750 File Offset: 0x00061950
		public StringBuilderText(StringBuilder builder, [Nullable(2)] Encoding encodingOpt, SourceHashAlgorithm checksumAlgorithm)
			: base(default(ImmutableArray<byte>), checksumAlgorithm, null)
		{
			this._builder = builder;
			this._encodingOpt = encodingOpt;
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x0600202B RID: 8235 RVA: 0x0006377E File Offset: 0x0006197E
		[Nullable(2)]
		public override Encoding Encoding
		{
			[NullableContext(2)]
			get
			{
				return this._encodingOpt;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x0600202C RID: 8236 RVA: 0x00063786 File Offset: 0x00061986
		internal StringBuilder Builder
		{
			get
			{
				return this._builder;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x0600202D RID: 8237 RVA: 0x0006378E File Offset: 0x0006198E
		public override int Length
		{
			get
			{
				return this._builder.Length;
			}
		}

		// Token: 0x17000838 RID: 2104
		public override char this[int position]
		{
			get
			{
				if (position < 0 || position >= this._builder.Length)
				{
					throw new ArgumentOutOfRangeException("position");
				}
				return this._builder.get_Chars(position);
			}
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x000637C6 File Offset: 0x000619C6
		public override string ToString(TextSpan span)
		{
			if (span.End > this._builder.Length)
			{
				throw new ArgumentOutOfRangeException("span");
			}
			return this._builder.ToString(span.Start, span.Length);
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x00063800 File Offset: 0x00061A00
		public override void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			this._builder.CopyTo(sourceIndex, destination, destinationIndex, count);
		}

		// Token: 0x04000E19 RID: 3609
		private readonly StringBuilder _builder;

		// Token: 0x04000E1A RID: 3610
		[Nullable(2)]
		private readonly Encoding _encodingOpt;
	}
}
