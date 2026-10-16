using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003CD RID: 973
	internal struct AttrRegRel
	{
		// Token: 0x04000E3C RID: 3644
		internal uint off;

		// Token: 0x04000E3D RID: 3645
		internal uint typind;

		// Token: 0x04000E3E RID: 3646
		internal ushort reg;

		// Token: 0x04000E3F RID: 3647
		internal uint offCod;

		// Token: 0x04000E40 RID: 3648
		internal ushort segCod;

		// Token: 0x04000E41 RID: 3649
		internal ushort flags;

		// Token: 0x04000E42 RID: 3650
		internal string name;
	}
}
