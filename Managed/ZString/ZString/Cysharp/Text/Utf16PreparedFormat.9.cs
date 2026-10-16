using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Cysharp.Text
{
	// Token: 0x02000023 RID: 35
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9>
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000B034 File Offset: 0x00009234
		[Nullable(1)]
		public string FormatString
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000B03C File Offset: 0x0000923C
		public int MinSize { get; }

		// Token: 0x06000118 RID: 280 RVA: 0x0000B044 File Offset: 0x00009244
		[NullableContext(1)]
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

		// Token: 0x06000119 RID: 281 RVA: 0x0000B0A4 File Offset: 0x000092A4
		[NullableContext(1)]
		public string Format(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
		{
			Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true);
			string text;
			try
			{
				this.FormatTo<Utf16ValueStringBuilder>(ref utf16ValueStringBuilder, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
				text = utf16ValueStringBuilder.ToString();
			}
			finally
			{
				utf16ValueStringBuilder.Dispose();
			}
			return text;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000B0FC File Offset: 0x000092FC
		[NullableContext(1)]
		public void FormatTo<[Nullable(0)] TBufferWriter>(ref TBufferWriter sb, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9) where TBufferWriter : IBufferWriter<char>
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
				case 7:
					Utf16FormatHelper.FormatTo<TBufferWriter, T8>(ref sb, arg8, utf16FormatSegment.Alignment, readOnlySpan.Slice(utf16FormatSegment.Offset, utf16FormatSegment.Count), "arg8");
					break;
				case 8:
					Utf16FormatHelper.FormatTo<TBufferWriter, T9>(ref sb, arg9, utf16FormatSegment.Alignment, readOnlySpan.Slice(utf16FormatSegment.Offset, utf16FormatSegment.Count), "arg9");
					break;
				}
			}
		}

		// Token: 0x04000059 RID: 89
		[Nullable(1)]
		private readonly Utf16FormatSegment[] segments;
	}
}
