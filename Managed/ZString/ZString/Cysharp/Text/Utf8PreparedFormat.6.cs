using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Cysharp.Text
{
	// Token: 0x02000030 RID: 48
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Utf8PreparedFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6>
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000D60F File Offset: 0x0000B80F
		public string FormatString { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000D617 File Offset: 0x0000B817
		public int MinSize { get; }

		// Token: 0x06000159 RID: 345 RVA: 0x0000D620 File Offset: 0x0000B820
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

		// Token: 0x0600015A RID: 346 RVA: 0x0000D684 File Offset: 0x0000B884
		public string Format(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			Utf8ValueStringBuilder utf8ValueStringBuilder = new Utf8ValueStringBuilder(true);
			string text;
			try
			{
				this.FormatTo<Utf8ValueStringBuilder>(ref utf8ValueStringBuilder, arg1, arg2, arg3, arg4, arg5, arg6);
				text = utf8ValueStringBuilder.ToString();
			}
			finally
			{
				utf8ValueStringBuilder.Dispose();
			}
			return text;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
		public void FormatTo<[Nullable(0)] TBufferWriter>(ref TBufferWriter sb, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) where TBufferWriter : IBufferWriter<byte>
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
				case 2:
					Utf8FormatHelper.FormatTo<TBufferWriter, T3>(ref sb, arg3, utf8FormatSegment.Alignment, utf8FormatSegment.StandardFormat, "arg3");
					break;
				case 3:
					Utf8FormatHelper.FormatTo<TBufferWriter, T4>(ref sb, arg4, utf8FormatSegment.Alignment, utf8FormatSegment.StandardFormat, "arg4");
					break;
				case 4:
					Utf8FormatHelper.FormatTo<TBufferWriter, T5>(ref sb, arg5, utf8FormatSegment.Alignment, utf8FormatSegment.StandardFormat, "arg5");
					break;
				case 5:
					Utf8FormatHelper.FormatTo<TBufferWriter, T6>(ref sb, arg6, utf8FormatSegment.Alignment, utf8FormatSegment.StandardFormat, "arg6");
					break;
				}
			}
		}

		// Token: 0x04000085 RID: 133
		private readonly Utf8FormatSegment[] segments;

		// Token: 0x04000086 RID: 134
		private readonly byte[] utf8PreEncodedbuffer;
	}
}
