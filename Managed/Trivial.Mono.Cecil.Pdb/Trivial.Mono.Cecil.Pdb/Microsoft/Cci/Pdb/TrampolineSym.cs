using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000DF RID: 223
	internal struct TrampolineSym
	{
		// Token: 0x04000473 RID: 1139
		internal ushort trampType;

		// Token: 0x04000474 RID: 1140
		internal ushort cbThunk;

		// Token: 0x04000475 RID: 1141
		internal uint offThunk;

		// Token: 0x04000476 RID: 1142
		internal uint offTarget;

		// Token: 0x04000477 RID: 1143
		internal ushort sectThunk;

		// Token: 0x04000478 RID: 1144
		internal ushort sectTarget;
	}
}
