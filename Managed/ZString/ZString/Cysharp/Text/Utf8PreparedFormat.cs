using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Cysharp.Text
{
	// Token: 0x0200002B RID: 43
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Utf8PreparedFormat<[Nullable(2)] T1>
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000CD8C File Offset: 0x0000AF8C
		public string FormatString { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000CD94 File Offset: 0x0000AF94
		public int MinSize { get; }

		// Token: 0x06000140 RID: 320 RVA: 0x0000CD9C File Offset: 0x0000AF9C
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

		// Token: 0x06000141 RID: 321 RVA: 0x0000CE00 File Offset: 0x0000B000
		public string Format(T1 arg1)
		{
			Utf8ValueStringBuilder utf8ValueStringBuilder = new Utf8ValueStringBuilder(true);
			string text;
			try
			{
				this.FormatTo<Utf8ValueStringBuilder>(ref utf8ValueStringBuilder, arg1);
				text = utf8ValueStringBuilder.ToString();
			}
			finally
			{
				utf8ValueStringBuilder.Dispose();
			}
			return text;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000CE48 File Offset: 0x0000B048
		public void FormatTo<[Nullable(0)] TBufferWriter>(ref TBufferWriter sb, T1 arg1) where TBufferWriter : IBufferWriter<byte>
		{
			Span<byte> span = this.utf8PreEncodedbuffer.AsSpan<byte>();
			foreach (Utf8FormatSegment utf8FormatSegment in this.segments)
			{
				int formatIndex = utf8FormatSegment.FormatIndex;
				if (formatIndex != -1)
				{
					if (formatIndex == 0)
					{
						Utf8FormatHelper.FormatTo<TBufferWriter, T1>(ref sb, arg1, utf8FormatSegment.Alignment, utf8FormatSegment.StandardFormat, "arg1");
					}
				}
				else
				{
					Span<byte> span2 = span.Slice(utf8FormatSegment.Offset, utf8FormatSegment.Count);
					Span<byte> span3 = sb.GetSpan(utf8FormatSegment.Count);
					span2.TryCopyTo(span3);
					sb.Advance(utf8FormatSegment.Count);
				}
			}
		}

		// Token: 0x04000071 RID: 113
		private readonly Utf8FormatSegment[] segments;

		// Token: 0x04000072 RID: 114
		private readonly byte[] utf8PreEncodedbuffer;
	}
}
