using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003C5 RID: 965
	internal struct ThunkSym32
	{
		// Token: 0x04000E12 RID: 3602
		internal uint parent;

		// Token: 0x04000E13 RID: 3603
		internal uint end;

		// Token: 0x04000E14 RID: 3604
		internal uint next;

		// Token: 0x04000E15 RID: 3605
		internal uint off;

		// Token: 0x04000E16 RID: 3606
		internal ushort seg;

		// Token: 0x04000E17 RID: 3607
		internal ushort len;

		// Token: 0x04000E18 RID: 3608
		internal byte ord;

		// Token: 0x04000E19 RID: 3609
		internal string name;

		// Token: 0x04000E1A RID: 3610
		internal byte[] variant;
	}
}
