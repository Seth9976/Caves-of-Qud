using System;

namespace Cysharp.Text
{
	// Token: 0x0200003D RID: 61
	internal readonly struct Utf16FormatSegment
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000F72C File Offset: 0x0000D92C
		public bool IsFormatArgument
		{
			get
			{
				return this.FormatIndex != -1;
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000F73A File Offset: 0x0000D93A
		public Utf16FormatSegment(int offset, int count, int formatIndex, int alignment)
		{
			this.Offset = offset;
			this.Count = count;
			this.FormatIndex = formatIndex;
			this.Alignment = alignment;
		}

		// Token: 0x040000B5 RID: 181
		public const int NotFormatIndex = -1;

		// Token: 0x040000B6 RID: 182
		public readonly int Offset;

		// Token: 0x040000B7 RID: 183
		public readonly int Count;

		// Token: 0x040000B8 RID: 184
		public readonly int FormatIndex;

		// Token: 0x040000B9 RID: 185
		public readonly int Alignment;
	}
}
