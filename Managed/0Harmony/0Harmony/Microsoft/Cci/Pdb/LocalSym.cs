using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003DA RID: 986
	internal struct LocalSym
	{
		// Token: 0x04000E8E RID: 3726
		internal uint id;

		// Token: 0x04000E8F RID: 3727
		internal uint typind;

		// Token: 0x04000E90 RID: 3728
		internal ushort flags;

		// Token: 0x04000E91 RID: 3729
		internal uint idParent;

		// Token: 0x04000E92 RID: 3730
		internal uint offParent;

		// Token: 0x04000E93 RID: 3731
		internal uint expr;

		// Token: 0x04000E94 RID: 3732
		internal uint pad0;

		// Token: 0x04000E95 RID: 3733
		internal uint pad1;

		// Token: 0x04000E96 RID: 3734
		internal string name;
	}
}
