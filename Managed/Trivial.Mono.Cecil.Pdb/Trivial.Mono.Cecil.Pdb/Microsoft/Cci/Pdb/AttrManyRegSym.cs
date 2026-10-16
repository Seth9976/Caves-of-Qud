using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000018 RID: 24
	internal struct AttrManyRegSym
	{
		// Token: 0x04000024 RID: 36
		internal uint typind;

		// Token: 0x04000025 RID: 37
		internal uint offCod;

		// Token: 0x04000026 RID: 38
		internal ushort segCod;

		// Token: 0x04000027 RID: 39
		internal ushort flags;

		// Token: 0x04000028 RID: 40
		internal byte count;

		// Token: 0x04000029 RID: 41
		internal byte[] reg;

		// Token: 0x0400002A RID: 42
		internal string name;
	}
}
