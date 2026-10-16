using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Cysharp.Text
{
	// Token: 0x0200001C RID: 28
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Utf16PreparedFormat<[Nullable(2)] T1, [Nullable(2)] T2>
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000A08F File Offset: 0x0000828F
		public string FormatString { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000A097 File Offset: 0x00008297
		public int MinSize { get; }

		// Token: 0x060000F5 RID: 245 RVA: 0x0000A0A0 File Offset: 0x000082A0
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

		// Token: 0x060000F6 RID: 246 RVA: 0x0000A100 File Offset: 0x00008300
		public string Format(T1 arg1, T2 arg2)
		{
			Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true);
			string text;
			try
			{
				this.FormatTo<Utf16ValueStringBuilder>(ref utf16ValueStringBuilder, arg1, arg2);
				text = utf16ValueStringBuilder.ToString();
			}
			finally
			{
				utf16ValueStringBuilder.Dispose();
			}
			return text;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000A148 File Offset: 0x00008348
		public void FormatTo<[Nullable(0)] TBufferWriter>(ref TBufferWriter sb, T1 arg1, T2 arg2) where TBufferWriter : IBufferWriter<char>
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
				}
			}
		}

		// Token: 0x04000044 RID: 68
		private readonly Utf16FormatSegment[] segments;
	}
}
