using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003DD RID: 989
	internal struct SectionSym
	{
		// Token: 0x04000E9E RID: 3742
		internal ushort isec;

		// Token: 0x04000E9F RID: 3743
		internal byte align;

		// Token: 0x04000EA0 RID: 3744
		internal byte bReserved;

		// Token: 0x04000EA1 RID: 3745
		internal uint rva;

		// Token: 0x04000EA2 RID: 3746
		internal uint cb;

		// Token: 0x04000EA3 RID: 3747
		internal uint characteristics;

		// Token: 0x04000EA4 RID: 3748
		internal string name;
	}
}
