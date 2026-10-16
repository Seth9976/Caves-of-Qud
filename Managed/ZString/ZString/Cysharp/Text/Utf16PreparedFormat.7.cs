using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Cysharp.Text
{
	// Token: 0x02000021 RID: 33
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Utf16PreparedFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7>
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000AAC6 File Offset: 0x00008CC6
		public string FormatString { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000AACE File Offset: 0x00008CCE
		public int MinSize { get; }

		// Token: 0x0600010E RID: 270 RVA: 0x0000AAD8 File Offset: 0x00008CD8
		public Utf16PreparedFormat(string format)
		{
			this.FormatString = format;
			this.segments = PreparedFormatHelper.Utf16Parse(format);
			int num = 0;
			foreach (Utf16FormatSegment utf16FormatSegment in this.segments)
			{
				if (!utf16FormatSegment.IsFormatArgument)
				{
					num += utf16FormatSegment.Count;
				}
			}
			this.MinSize = num;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000AB38 File Offset: 0x00008D38
		public string Format(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
		{
			Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true);
			string text;
			try
			{
				this.FormatTo<Utf16ValueStringBuilder>(ref utf16ValueStringBuilder, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
				text = utf16ValueStringBuilder.ToString();
			}
			finally
			{
				utf16ValueStringBuilder.Dispose();
			}
			return text;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000AB8C File Offset: 0x00008D8C
		public void FormatTo<[Nullable(0)] TBufferWriter>(ref TBufferWriter sb, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) where TBufferWriter : IBufferWriter<char>
		{
			ReadOnlySpan<char> readOnlySpan = this.FormatString.AsSpan();
			foreach (Utf16FormatSegment utf16FormatSegment in this.segments)
			{
				switch (utf16FormatSegment.FormatIndex)
				{
				case -1:
				{
					ReadOnlySpan<char> readOnlySpan2 = readOnlySpan.Slice(utf16FormatSegment.Offset, utf16FormatSegment.Count);
					Span<char> span = sb.GetSpan(utf16FormatSegment.Count);
					readOnlySpan2.TryCopyTo(span);
					sb.Advance(utf16FormatSegment.Count);
					break;
				}
				case 0:
					Utf16FormatHelper.FormatTo<TBufferWriter, T1>(ref sb, arg1, utf16FormatSegment.Alignment, readOnlySpan.Slice(utf16FormatSegment.Offset, utf16FormatSegment.Count), "arg1");
					break;
				case 1:
					Utf16FormatHelper.FormatTo<TBufferWriter, T2>(ref sb, arg2, utf16FormatSegment.Alignment, readOnlySpan.Slice(utf16FormatSegment.Offset, utf16FormatSegment.Count), "arg2");
					break;
				case 2:
					Utf16FormatHelper.FormatTo<TBufferWriter, T3>(ref sb, arg3, utf16FormatSegment.Alignment, readOnlySpan.Slice(utf16FormatSegment.Offset, utf16FormatSegment.Count), "arg3");
					break;
				case 3:
					Utf16FormatHelper.FormatTo<TBufferWriter, T4>(ref sb, arg4, utf16FormatSegment.Alignment, readOnlySpan.Slice(utf16FormatSegment.Offset, utf16FormatSegment.Count), "arg4");
					break;
				case 4:
					Utf16FormatHelper.FormatTo<TBufferWriter, T5>(ref sb, arg5, utf16FormatSegment.Alignment, readOnlySpan.Slice(utf16FormatSegment.Offset, utf16FormatSegment.Count), "arg5");
					break;
				case 5:
					Utf16FormatHelper.FormatTo<TBufferWriter, T6>(ref sb, arg6, utf16FormatSegment.Alignment, readOnlySpan.Slice(utf16FormatSegment.Offset, utf16FormatSegment.Count), "arg6");
					break;
				case 6:
					Utf16FormatHelper.FormatTo<TBufferWriter, T7>(ref sb, arg7, utf16FormatSegment.Alignment, readOnlySpan.Slice(utf16FormatSegment.Offset, utf16FormatSegment.Count), "arg7");
					break;
				}
			}
		}

		// Token: 0x04000053 RID: 83
		private readonly Utf16FormatSegment[] segments;
	}
}
