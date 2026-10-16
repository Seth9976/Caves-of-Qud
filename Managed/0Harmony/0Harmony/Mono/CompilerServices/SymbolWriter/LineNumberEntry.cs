using System;
using System.Collections.Generic;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000304 RID: 772
	public class LineNumberEntry
	{
		// Token: 0x06001357 RID: 4951 RVA: 0x0003E98F File Offset: 0x0003CB8F
		public LineNumberEntry(int file, int row, int column, int offset)
			: this(file, row, column, offset, false)
		{
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0003E99D File Offset: 0x0003CB9D
		public LineNumberEntry(int file, int row, int offset)
			: this(file, row, -1, offset, false)
		{
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0003E9AA File Offset: 0x0003CBAA
		public LineNumberEntry(int file, int row, int column, int offset, bool is_hidden)
			: this(file, row, column, -1, -1, offset, is_hidden)
		{
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0003E9BB File Offset: 0x0003CBBB
		public LineNumberEntry(int file, int row, int column, int end_row, int end_column, int offset, bool is_hidden)
		{
			this.File = file;
			this.Row = row;
			this.Column = column;
			this.EndRow = end_row;
			this.EndColumn = end_column;
			this.Offset = offset;
			this.IsHidden = is_hidden;
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0003E9F8 File Offset: 0x0003CBF8
		public override string ToString()
		{
			return string.Format("[Line {0}:{1},{2}-{3},{4}:{5}]", new object[] { this.File, this.Row, this.Column, this.EndRow, this.EndColumn, this.Offset });
		}

		// Token: 0x040009E9 RID: 2537
		public readonly int Row;

		// Token: 0x040009EA RID: 2538
		public int Column;

		// Token: 0x040009EB RID: 2539
		public int EndRow;

		// Token: 0x040009EC RID: 2540
		public int EndColumn;

		// Token: 0x040009ED RID: 2541
		public readonly int File;

		// Token: 0x040009EE RID: 2542
		public readonly int Offset;

		// Token: 0x040009EF RID: 2543
		public readonly bool IsHidden;

		// Token: 0x040009F0 RID: 2544
		public static readonly LineNumberEntry Null = new LineNumberEntry(0, 0, 0, 0);

		// Token: 0x02000305 RID: 773
		public sealed class LocationComparer : IComparer<LineNumberEntry>
		{
			// Token: 0x0600135D RID: 4957 RVA: 0x0003EA7C File Offset: 0x0003CC7C
			public int Compare(LineNumberEntry l1, LineNumberEntry l2)
			{
				if (l1.Row != l2.Row)
				{
					return l1.Row.CompareTo(l2.Row);
				}
				return l1.Column.CompareTo(l2.Column);
			}

			// Token: 0x040009F1 RID: 2545
			public static readonly LineNumberEntry.LocationComparer Default = new LineNumberEntry.LocationComparer();
		}
	}
}
