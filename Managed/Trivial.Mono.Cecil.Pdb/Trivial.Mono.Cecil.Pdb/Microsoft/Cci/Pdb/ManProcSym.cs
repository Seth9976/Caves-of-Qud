using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000B3 RID: 179
	internal struct ManProcSym
	{
		// Token: 0x0400032B RID: 811
		internal uint parent;

		// Token: 0x0400032C RID: 812
		internal uint end;

		// Token: 0x0400032D RID: 813
		internal uint next;

		// Token: 0x0400032E RID: 814
		internal uint len;

		// Token: 0x0400032F RID: 815
		internal uint dbgStart;

		// Token: 0x04000330 RID: 816
		internal uint dbgEnd;

		// Token: 0x04000331 RID: 817
		internal uint token;

		// Token: 0x04000332 RID: 818
		internal uint off;

		// Token: 0x04000333 RID: 819
		internal ushort seg;

		// Token: 0x04000334 RID: 820
		internal byte flags;

		// Token: 0x04000335 RID: 821
		internal ushort retReg;

		// Token: 0x04000336 RID: 822
		internal string name;
	}
}
