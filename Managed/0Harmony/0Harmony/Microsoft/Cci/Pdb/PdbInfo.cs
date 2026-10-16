using System;
using System.Collections.Generic;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000409 RID: 1033
	internal class PdbInfo
	{
		// Token: 0x04000F67 RID: 3943
		public PdbFunction[] Functions;

		// Token: 0x04000F68 RID: 3944
		public Dictionary<uint, PdbTokenLine> TokenToSourceMapping;

		// Token: 0x04000F69 RID: 3945
		public string SourceServerData;

		// Token: 0x04000F6A RID: 3946
		public int Age;

		// Token: 0x04000F6B RID: 3947
		public Guid Guid;

		// Token: 0x04000F6C RID: 3948
		public byte[] SourceLinkData;
	}
}
