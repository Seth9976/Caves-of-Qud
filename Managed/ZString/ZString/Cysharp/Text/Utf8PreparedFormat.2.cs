using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Cysharp.Text
{
	// Token: 0x0200002C RID: 44
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Utf8PreparedFormat<[Nullable(2)] T1, [Nullable(2)] T2>
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000CEF3 File Offset: 0x0000B0F3
		public string FormatString { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000CEFB File Offset: 0x0000B0FB
		public int MinSize { get; }

		// Token: 0x06000145 RID: 325 RVA: 0x0000CF04 File Offset: 0x0000B104
		public Utf8PreparedFormat(string format)
		{
			this.FormatString = format;
			this.segments = PreparedFormatHelper.Utf8Parse(format, out this.utf8PreEncodedbuffer);
			int num = 0;
			foreach (Utf8FormatSegment utf8FormatSegment in this.segments)
			{
				if (!utf8FormatSegment.IsFormatArgument)
				{
					num += utf8FormatSegment.Count;
				}
			}
			this.MinSize = num;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000CF68 File Offset: 0x0000B168
		public string Format(T1 arg1, T2 arg2)
		{
			Utf8ValueStringBuilder utf8ValueStringBuilder = new Utf8ValueStringBuilder(true);
			string text;
			try
			{
				this.FormatTo<Utf8ValueStringBuilder>(ref utf8ValueStringBuilder, arg1, arg2);
				text = utf8ValueStringBuilder.ToString();
			}
			finally
			{
				utf8ValueStringBuilder.Dispose();
			}
			return text;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000CFB0 File Offset: 0x0000B1B0
		public void FormatTo<[Nullable(0)] TBufferWriter>(ref TBufferWriter sb, T1 arg1, T2 arg2) where TBufferWriter : IBufferWriter<byte>
		{
			Span<byte> span = this.utf8PreEncodedbuffer.AsSpan<byte>();
			foreach (Utf8FormatSegment utf8FormatSegment in this.segments)
			{
				switch (utf8FormatSegment.FormatIndex)
				{
				case -1:
				{
					Span<byte> span2 = span.Slice(utf8FormatSegment.Offset, utf8FormatSegment.Count);
					Span<byte> span3 = sb.GetSpan(utf8FormatSegment.Count);
					span2.TryCopyTo(span3);
					sb.Advance(utf8FormatSegment.Count);
					break;
				}
				case 0:
					Utf8FormatHelper.FormatTo<TBufferWriter, T1>(ref sb, arg1, utf8FormatSegment.Alignment, utf8FormatSegment.StandardFormat, "arg1");
					break;
				case 1:
					Utf8FormatHelper.FormatTo<TBufferWriter, T2>(ref sb, arg2, utf8FormatSegment.Alignment, utf8FormatSegment.StandardFormat, "arg2");
					break;
				}
			}
		}

		// Token: 0x04000075 RID: 117
		private readonly Utf8FormatSegment[] segments;

		// Token: 0x04000076 RID: 118
		private readonly byte[] utf8PreEncodedbuffer;
	}
}
