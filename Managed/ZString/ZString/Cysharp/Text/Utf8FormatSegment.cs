using System;
using System.Buffers;

namespace Cysharp.Text
{
	// Token: 0x0200003C RID: 60
	internal readonly struct Utf8FormatSegment
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000F6F7 File Offset: 0x0000D8F7
		public bool IsFormatArgument
		{
			get
			{
				return this.FormatIndex != -1;
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000F705 File Offset: 0x0000D905
		public Utf8FormatSegment(int offset, int count, int formatIndex, StandardFormat format, int alignment)
		{
			this.Offset = offset;
			this.Count = count;
			this.FormatIndex = formatIndex;
			this.StandardFormat = format;
			this.Alignment = alignment;
		}

		// Token: 0x040000AF RID: 175
		public const int NotFormatIndex = -1;

		// Token: 0x040000B0 RID: 176
		public readonly int Offset;

		// Token: 0x040000B1 RID: 177
		public readonly int Count;

		// Token: 0x040000B2 RID: 178
		public readonly int FormatIndex;

		// Token: 0x040000B3 RID: 179
		public readonly StandardFormat StandardFormat;

		// Token: 0x040000B4 RID: 180
		public readonly int Alignment;
	}
}
