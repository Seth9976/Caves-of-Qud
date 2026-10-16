using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Cysharp.Text
{
	// Token: 0x0200001D RID: 29
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Utf16PreparedFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3>
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000A239 File Offset: 0x00008439
		public string FormatString { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000A241 File Offset: 0x00008441
		public int MinSize { get; }

		// Token: 0x060000FA RID: 250 RVA: 0x0000A24C File Offset: 0x0000844C
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

		// Token: 0x060000FB RID: 251 RVA: 0x0000A2AC File Offset: 0x000084AC
		public string Format(T1 arg1, T2 arg2, T3 arg3)
		{
			Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true);
			string text;
			try
			{
				this.FormatTo<Utf16ValueStringBuilder>(ref utf16ValueStringBuilder, arg1, arg2, arg3);
				text = utf16ValueStringBuilder.ToString();
			}
			finally
			{
				utf16ValueStringBuilder.Dispose();
			}
			return text;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000A2F8 File Offset: 0x000084F8
		public void FormatTo<[Nullable(0)] TBufferWriter>(ref TBufferWriter sb, T1 arg1, T2 arg2, T3 arg3) where TBufferWriter : IBufferWriter<char>
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
				}
			}
		}

		// Token: 0x04000047 RID: 71
		private readonly Utf16FormatSegment[] segments;
	}
}
