using System;
using System.Collections.Immutable;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002F0 RID: 752
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class StringText : SourceText
	{
		// Token: 0x06002031 RID: 8241 RVA: 0x00063812 File Offset: 0x00061A12
		[NullableContext(0)]
		internal StringText([Nullable(1)] string source, [Nullable(2)] Encoding encodingOpt, ImmutableArray<byte> checksum = default(ImmutableArray<byte>), SourceHashAlgorithm checksumAlgorithm = SourceHashAlgorithm.Sha1, ImmutableArray<byte> embeddedTextBlob = default(ImmutableArray<byte>))
			: base(checksum, checksumAlgorithm, embeddedTextBlob)
		{
			this._source = source;
			this._encodingOpt = encodingOpt;
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06002032 RID: 8242 RVA: 0x0006382D File Offset: 0x00061A2D
		[Nullable(2)]
		public override Encoding Encoding
		{
			[NullableContext(2)]
			get
			{
				return this._encodingOpt;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06002033 RID: 8243 RVA: 0x00063835 File Offset: 0x00061A35
		public string Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06002034 RID: 8244 RVA: 0x0006383D File Offset: 0x00061A3D
		public override int Length
		{
			get
			{
				return this._source.Length;
			}
		}

		// Token: 0x1700083C RID: 2108
		public override char this[int position]
		{
			get
			{
				return this._source.get_Chars(position);
			}
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x00063858 File Offset: 0x00061A58
		public override string ToString(TextSpan span)
		{
			if (span.End > this.Source.Length)
			{
				throw new ArgumentOutOfRangeException("span");
			}
			if (span.Start == 0 && span.Length == this.Length)
			{
				return this.Source;
			}
			return this.Source.Substring(span.Start, span.Length);
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x000638BC File Offset: 0x00061ABC
		public override void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			this.Source.CopyTo(sourceIndex, destination, destinationIndex, count);
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x000638CE File Offset: 0x00061ACE
		public override void Write(TextWriter textWriter, TextSpan span, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (span.Start == 0 && span.End == this.Length)
			{
				textWriter.Write(this.Source);
				return;
			}
			base.Write(textWriter, span, cancellationToken);
		}

		// Token: 0x04000E1B RID: 3611
		private readonly string _source;

		// Token: 0x04000E1C RID: 3612
		[Nullable(2)]
		private readonly Encoding _encodingOpt;
	}
}
