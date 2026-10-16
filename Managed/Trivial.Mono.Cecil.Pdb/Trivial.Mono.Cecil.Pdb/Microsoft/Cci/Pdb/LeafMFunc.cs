using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x0200008F RID: 143
	internal struct LeafMFunc
	{
		// Token: 0x040002C7 RID: 711
		internal uint rvtype;

		// Token: 0x040002C8 RID: 712
		internal uint classtype;

		// Token: 0x040002C9 RID: 713
		internal uint thistype;

		// Token: 0x040002CA RID: 714
		internal byte calltype;

		// Token: 0x040002CB RID: 715
		internal byte reserved;

		// Token: 0x040002CC RID: 716
		internal ushort parmcount;

		// Token: 0x040002CD RID: 717
		internal uint arglist;

		// Token: 0x040002CE RID: 718
		internal int thisadjust;
	}
}
