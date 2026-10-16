using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Cysharp.Text
{
	// Token: 0x0200001B RID: 27
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Utf16PreparedFormat<[Nullable(2)] T1>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00009F1C File Offset: 0x0000811C
		public string FormatString { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00009F24 File Offset: 0x00008124
		public int MinSize { get; }

		// Token: 0x060000F0 RID: 240 RVA: 0x00009F2C File Offset: 0x0000812C
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

		// Token: 0x060000F1 RID: 241 RVA: 0x00009F8C File Offset: 0x0000818C
		public string Format(T1 arg1)
		{
			Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true);
			string text;
			try
			{
				this.FormatTo<Utf16ValueStringBuilder>(ref utf16ValueStringBuilder, arg1);
				text = utf16ValueStringBuilder.ToString();
			}
			finally
			{
				utf16ValueStringBuilder.Dispose();
			}
			return text;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00009FD4 File Offset: 0x000081D4
		public void FormatTo<[Nullable(0)] TBufferWriter>(ref TBufferWriter sb, T1 arg1) where TBufferWriter : IBufferWriter<char>
		{
			ReadOnlySpan<char> readOnlySpan = this.FormatString.AsSpan();
			foreach (Utf16FormatSegment utf16FormatSegment in this.segments)
			{
				int formatIndex = utf16FormatSegment.FormatIndex;
				if (formatIndex != -1)
				{
					if (formatIndex == 0)
					{
						Utf16FormatHelper.FormatTo<TBufferWriter, T1>(ref sb, arg1, utf16FormatSegment.Alignment, readOnlySpan.Slice(utf16FormatSegment.Offset, utf16FormatSegment.Count), "arg1");
					}
				}
				else
				{
					ReadOnlySpan<char> readOnlySpan2 = readOnlySpan.Slice(utf16FormatSegment.Offset, utf16FormatSegment.Count);
					Span<char> span = sb.GetSpan(utf16FormatSegment.Count);
					readOnlySpan2.TryCopyTo(span);
					sb.Advance(utf16FormatSegment.Count);
				}
			}
		}

		// Token: 0x04000041 RID: 65
		private readonly Utf16FormatSegment[] segments;
	}
}
