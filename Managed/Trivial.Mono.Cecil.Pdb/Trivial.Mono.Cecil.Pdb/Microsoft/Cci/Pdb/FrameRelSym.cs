using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000069 RID: 105
	internal struct FrameRelSym
	{
		// Token: 0x040001F1 RID: 497
		internal int off;

		// Token: 0x040001F2 RID: 498
		internal uint typind;

		// Token: 0x040001F3 RID: 499
		internal uint offCod;

		// Token: 0x040001F4 RID: 500
		internal ushort segCod;

		// Token: 0x040001F5 RID: 501
		internal ushort flags;

		// Token: 0x040001F6 RID: 502
		internal string name;
	}
}
