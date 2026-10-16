using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x0200035F RID: 863
	internal struct LeafMFunc
	{
		// Token: 0x04000C50 RID: 3152
		internal uint rvtype;

		// Token: 0x04000C51 RID: 3153
		internal uint classtype;

		// Token: 0x04000C52 RID: 3154
		internal uint thistype;

		// Token: 0x04000C53 RID: 3155
		internal byte calltype;

		// Token: 0x04000C54 RID: 3156
		internal byte reserved;

		// Token: 0x04000C55 RID: 3157
		internal ushort parmcount;

		// Token: 0x04000C56 RID: 3158
		internal uint arglist;

		// Token: 0x04000C57 RID: 3159
		internal int thisadjust;
	}
}
