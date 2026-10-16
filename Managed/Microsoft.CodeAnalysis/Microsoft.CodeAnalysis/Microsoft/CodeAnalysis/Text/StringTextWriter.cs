using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002F1 RID: 753
	[NullableContext(1)]
	[Nullable(0)]
	internal class StringTextWriter : SourceTextWriter
	{
		// Token: 0x06002039 RID: 8249 RVA: 0x000638FE File Offset: 0x00061AFE
		[NullableContext(2)]
		public StringTextWriter(Encoding encoding, SourceHashAlgorithm checksumAlgorithm, int capacity)
		{
			this._builder = new StringBuilder(capacity);
			this._encoding = encoding;
			this._checksumAlgorithm = checksumAlgorithm;
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x0600203A RID: 8250 RVA: 0x00063920 File Offset: 0x00061B20
		public override Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x00063928 File Offset: 0x00061B28
		public override SourceText ToSourceText()
		{
			string text = this._builder.ToString();
			Encoding encoding = this._encoding;
			SourceHashAlgorithm checksumAlgorithm = this._checksumAlgorithm;
			return new StringText(text, encoding, default(ImmutableArray<byte>), checksumAlgorithm, default(ImmutableArray<byte>));
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x00063965 File Offset: 0x00061B65
		public override void Write(char value)
		{
			this._builder.Append(value);
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x00063974 File Offset: 0x00061B74
		[NullableContext(2)]
		public override void Write(string value)
		{
			this._builder.Append(value);
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x00063983 File Offset: 0x00061B83
		public override void Write(char[] buffer, int index, int count)
		{
			this._builder.Append(buffer, index, count);
		}

		// Token: 0x04000E1D RID: 3613
		private readonly StringBuilder _builder;

		// Token: 0x04000E1E RID: 3614
		[Nullable(2)]
		private readonly Encoding _encoding;

		// Token: 0x04000E1F RID: 3615
		private readonly SourceHashAlgorithm _checksumAlgorithm;
	}
}
