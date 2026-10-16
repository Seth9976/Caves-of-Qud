using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000CB RID: 203
	internal class PdbTokenLine
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x00006B01 File Offset: 0x00004D01
		internal PdbTokenLine(uint token, uint file_id, uint line, uint column, uint endLine, uint endColumn)
		{
			this.token = token;
			this.file_id = file_id;
			this.line = line;
			this.column = column;
			this.endLine = endLine;
			this.endColumn = endColumn;
		}

		// Token: 0x0400039B RID: 923
		internal uint token;

		// Token: 0x0400039C RID: 924
		internal uint file_id;

		// Token: 0x0400039D RID: 925
		internal uint line;

		// Token: 0x0400039E RID: 926
		internal uint column;

		// Token: 0x0400039F RID: 927
		internal uint endLine;

		// Token: 0x040003A0 RID: 928
		internal uint endColumn;

		// Token: 0x040003A1 RID: 929
		internal PdbSource sourceFile;

		// Token: 0x040003A2 RID: 930
		internal PdbTokenLine nextLine;
	}
}
