using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000B2 RID: 178
	internal struct LocalSym
	{
		// Token: 0x04000322 RID: 802
		internal uint id;

		// Token: 0x04000323 RID: 803
		internal uint typind;

		// Token: 0x04000324 RID: 804
		internal ushort flags;

		// Token: 0x04000325 RID: 805
		internal uint idParent;

		// Token: 0x04000326 RID: 806
		internal uint offParent;

		// Token: 0x04000327 RID: 807
		internal uint expr;

		// Token: 0x04000328 RID: 808
		internal uint pad0;

		// Token: 0x04000329 RID: 809
		internal uint pad1;

		// Token: 0x0400032A RID: 810
		internal string name;
	}
}
