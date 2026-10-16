using System;
using System.Collections.Generic;

namespace Trivial.Mono.CompilerServices.SymbolWriter
{
	// Token: 0x0200000A RID: 10
	public class LineNumberEntry
	{
		// Token: 0x06000027 RID: 39 RVA: 0x0000281C File Offset: 0x00000A1C
		public LineNumberEntry(int file, int row, int column, int offset)
			: this(file, row, column, offset, false)
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000282A File Offset: 0x00000A2A
		public LineNumberEntry(int file, int row, int offset)
			: this(file, row, -1, offset, false)
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002837 File Offset: 0x00000A37
		public LineNumberEntry(int file, int row, int column, int offset, bool is_hidden)
			: this(file, row, column, -1, -1, offset, is_hidden)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002848 File Offset: 0x00000A48
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

		// Token: 0x0600002B RID: 43 RVA: 0x00002888 File Offset: 0x00000A88
		public override string ToString()
		{
			return string.Format("[Line {0}:{1,2}-{3,4}:{5}]", new object[] { this.File, this.Row, this.Column, this.EndRow, this.EndColumn, this.Offset });
		}

		// Token: 0x04000015 RID: 21
		public readonly int Row;

		// Token: 0x04000016 RID: 22
		public int Column;

		// Token: 0x04000017 RID: 23
		public int EndRow;

		// Token: 0x04000018 RID: 24
		public int EndColumn;

		// Token: 0x04000019 RID: 25
		public readonly int File;

		// Token: 0x0400001A RID: 26
		public readonly int Offset;

		// Token: 0x0400001B RID: 27
		public readonly bool IsHidden;

		// Token: 0x0400001C RID: 28
		public static readonly LineNumberEntry Null = new LineNumberEntry(0, 0, 0, 0);

		// Token: 0x02000023 RID: 35
		public sealed class LocationComparer : IComparer<LineNumberEntry>
		{
			// Token: 0x060000F9 RID: 249 RVA: 0x00006200 File Offset: 0x00004400
			public int Compare(LineNumberEntry l1, LineNumberEntry l2)
			{
				if (l1.Row != l2.Row)
				{
					return l1.Row.CompareTo(l2.Row);
				}
				return l1.Column.CompareTo(l2.Column);
			}

			// Token: 0x040000AD RID: 173
			public static readonly LineNumberEntry.LocationComparer Default = new LineNumberEntry.LocationComparer();
		}
	}
}
