using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000DD RID: 221
	internal struct ThunkSym32
	{
		// Token: 0x04000467 RID: 1127
		internal uint parent;

		// Token: 0x04000468 RID: 1128
		internal uint end;

		// Token: 0x04000469 RID: 1129
		internal uint next;

		// Token: 0x0400046A RID: 1130
		internal uint off;

		// Token: 0x0400046B RID: 1131
		internal ushort seg;

		// Token: 0x0400046C RID: 1132
		internal ushort len;

		// Token: 0x0400046D RID: 1133
		internal byte ord;

		// Token: 0x0400046E RID: 1134
		internal string name;

		// Token: 0x0400046F RID: 1135
		internal byte[] variant;
	}
}
