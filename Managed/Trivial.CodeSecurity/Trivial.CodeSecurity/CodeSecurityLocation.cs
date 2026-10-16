using System;

namespace Trivial.CodeSecurity
{
	// Token: 0x02000004 RID: 4
	public struct CodeSecurityLocation
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002533 File Offset: 0x00000733
		public string FileLocation
		{
			get
			{
				return this.fileLocation;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000253B File Offset: 0x0000073B
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002543 File Offset: 0x00000743
		public int ColumnNumber
		{
			get
			{
				return this.columnNumber;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000254B File Offset: 0x0000074B
		public CodeSecurityLocation(string fileLocation)
		{
			this.fileLocation = fileLocation;
			this.lineNumber = -1;
			this.columnNumber = -1;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002562 File Offset: 0x00000762
		public CodeSecurityLocation(string fileLocation, int line, int column)
		{
			this.fileLocation = fileLocation;
			this.lineNumber = line;
			this.columnNumber = column;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002579 File Offset: 0x00000779
		public override string ToString()
		{
			if (this.lineNumber != -1)
			{
				return string.Format("{0}, {1}, {2}", this.fileLocation, this.lineNumber, this.columnNumber);
			}
			return this.fileLocation;
		}

		// Token: 0x0400000D RID: 13
		private string fileLocation;

		// Token: 0x0400000E RID: 14
		private int lineNumber;

		// Token: 0x0400000F RID: 15
		private int columnNumber;

		// Token: 0x04000010 RID: 16
		public static readonly CodeSecurityLocation defaultLocation = new CodeSecurityLocation("Unknown");
	}
}
