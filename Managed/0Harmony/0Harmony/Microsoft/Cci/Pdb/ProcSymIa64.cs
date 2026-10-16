using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003D1 RID: 977
	internal struct ProcSymIa64
	{
		// Token: 0x04000E5A RID: 3674
		internal uint parent;

		// Token: 0x04000E5B RID: 3675
		internal uint end;

		// Token: 0x04000E5C RID: 3676
		internal uint next;

		// Token: 0x04000E5D RID: 3677
		internal uint len;

		// Token: 0x04000E5E RID: 3678
		internal uint dbgStart;

		// Token: 0x04000E5F RID: 3679
		internal uint dbgEnd;

		// Token: 0x04000E60 RID: 3680
		internal uint typind;

		// Token: 0x04000E61 RID: 3681
		internal uint off;

		// Token: 0x04000E62 RID: 3682
		internal ushort seg;

		// Token: 0x04000E63 RID: 3683
		internal ushort retReg;

		// Token: 0x04000E64 RID: 3684
		internal byte flags;

		// Token: 0x04000E65 RID: 3685
		internal string name;
	}
}
