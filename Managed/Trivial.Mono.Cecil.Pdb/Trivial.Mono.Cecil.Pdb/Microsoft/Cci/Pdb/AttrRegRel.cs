using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x0200001A RID: 26
	internal struct AttrRegRel
	{
		// Token: 0x04000032 RID: 50
		internal uint off;

		// Token: 0x04000033 RID: 51
		internal uint typind;

		// Token: 0x04000034 RID: 52
		internal ushort reg;

		// Token: 0x04000035 RID: 53
		internal uint offCod;

		// Token: 0x04000036 RID: 54
		internal ushort segCod;

		// Token: 0x04000037 RID: 55
		internal ushort flags;

		// Token: 0x04000038 RID: 56
		internal string name;
	}
}
