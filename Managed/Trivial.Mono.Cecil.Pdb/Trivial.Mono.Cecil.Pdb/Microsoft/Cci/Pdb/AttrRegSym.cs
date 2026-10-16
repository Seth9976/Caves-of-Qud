using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x0200001B RID: 27
	internal struct AttrRegSym
	{
		// Token: 0x04000039 RID: 57
		internal uint typind;

		// Token: 0x0400003A RID: 58
		internal uint offCod;

		// Token: 0x0400003B RID: 59
		internal ushort segCod;

		// Token: 0x0400003C RID: 60
		internal ushort flags;

		// Token: 0x0400003D RID: 61
		internal ushort reg;

		// Token: 0x0400003E RID: 62
		internal string name;
	}
}
