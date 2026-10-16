using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000019 RID: 25
	internal struct AttrManyRegSym2
	{
		// Token: 0x0400002B RID: 43
		internal uint typind;

		// Token: 0x0400002C RID: 44
		internal uint offCod;

		// Token: 0x0400002D RID: 45
		internal ushort segCod;

		// Token: 0x0400002E RID: 46
		internal ushort flags;

		// Token: 0x0400002F RID: 47
		internal ushort count;

		// Token: 0x04000030 RID: 48
		internal ushort[] reg;

		// Token: 0x04000031 RID: 49
		internal string name;
	}
}
