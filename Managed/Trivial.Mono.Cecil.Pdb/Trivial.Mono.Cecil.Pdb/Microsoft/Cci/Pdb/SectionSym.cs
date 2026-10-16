using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000D6 RID: 214
	internal struct SectionSym
	{
		// Token: 0x040003E1 RID: 993
		internal ushort isec;

		// Token: 0x040003E2 RID: 994
		internal byte align;

		// Token: 0x040003E3 RID: 995
		internal byte bReserved;

		// Token: 0x040003E4 RID: 996
		internal uint rva;

		// Token: 0x040003E5 RID: 997
		internal uint cb;

		// Token: 0x040003E6 RID: 998
		internal uint characteristics;

		// Token: 0x040003E7 RID: 999
		internal string name;
	}
}
