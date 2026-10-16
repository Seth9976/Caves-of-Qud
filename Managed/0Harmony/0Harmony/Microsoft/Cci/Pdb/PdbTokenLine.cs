using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000410 RID: 1040
	internal class PdbTokenLine
	{
		// Token: 0x060015F1 RID: 5617 RVA: 0x000467FE File Offset: 0x000449FE
		internal PdbTokenLine(uint token, uint file_id, uint line, uint column, uint endLine, uint endColumn)
		{
			this.token = token;
			this.file_id = file_id;
			this.line = line;
			this.column = column;
			this.endLine = endLine;
			this.endColumn = endColumn;
		}

		// Token: 0x04000F87 RID: 3975
		internal uint token;

		// Token: 0x04000F88 RID: 3976
		internal uint file_id;

		// Token: 0x04000F89 RID: 3977
		internal uint line;

		// Token: 0x04000F8A RID: 3978
		internal uint column;

		// Token: 0x04000F8B RID: 3979
		internal uint endLine;

		// Token: 0x04000F8C RID: 3980
		internal uint endColumn;

		// Token: 0x04000F8D RID: 3981
		internal PdbSource sourceFile;

		// Token: 0x04000F8E RID: 3982
		internal PdbTokenLine nextLine;
	}
}
