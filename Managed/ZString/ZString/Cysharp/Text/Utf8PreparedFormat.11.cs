using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Cysharp.Text
{
	// Token: 0x02000035 RID: 53
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000E215 File Offset: 0x0000C415
		[Nullable(1)]
		public string FormatString
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000E21D File Offset: 0x0000C41D
		public int MinSize { get; }

		// Token: 0x06000172 RID: 370 RVA: 0x0000E228 File Offset: 0x0000C428
		[NullableContext(1)]
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

		// Token: 0x06000173 RID: 371 RVA: 0x0000E28C File Offset: 0x0000C48C
		[NullableContext(1)]
		public string Format(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
		{
			Utf8ValueStringBuilder utf8ValueStringBuilder = new Utf8ValueStringBuilder(true);
			string text;
			try
			{
				this.FormatTo<Utf8ValueStringBuilder>(ref utf8ValueStringBuilder, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
				text = utf8ValueStringBuilder.ToString();
			}
			finally
			{
				utf8ValueStringBuilder.Dispose();
			}
			return text;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000E2E8 File Offset: 0x0000C4E8
		[NullableContext(1)]
		public void FormatTo<[Nullable(0)] TBufferWriter>(ref TBufferWriter sb, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11) where TBufferWriter : IBufferWriter<byte>
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
				case 6:
					Utf8FormatHelper.FormatTo<TBufferWriter, T7>(ref sb, arg7, utf8FormatSegment.Alignment, utf8FormatSegment.StandardFormat, "arg7");
					break;
				case 7:
					Utf8FormatHelper.FormatTo<TBufferWriter, T8>(ref sb, arg8, utf8FormatSegment.Alignment, utf8FormatSegment.StandardFormat, "arg8");
					break;
				case 8:
					Utf8FormatHelper.FormatTo<TBufferWriter, T9>(ref sb, arg9, utf8FormatSegment.Alignment, utf8FormatSegment.StandardFormat, "arg9");
					break;
				case 9:
					Utf8FormatHelper.FormatTo<TBufferWriter, T10>(ref sb, arg10, utf8FormatSegment.Alignment, utf8FormatSegment.StandardFormat, "arg10");
					break;
				case 10:
					Utf8FormatHelper.FormatTo<TBufferWriter, T11>(ref sb, arg11, utf8FormatSegment.Alignment, utf8FormatSegment.StandardFormat, "arg11");
					break;
				}
			}
		}

		// Token: 0x04000099 RID: 153
		[Nullable(1)]
		private readonly Utf8FormatSegment[] segments;

		// Token: 0x0400009A RID: 154
		[Nullable(1)]
		private readonly byte[] utf8PreEncodedbuffer;
	}
}
