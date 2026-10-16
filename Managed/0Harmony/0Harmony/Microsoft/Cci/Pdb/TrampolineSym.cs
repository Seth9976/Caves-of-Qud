using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003C7 RID: 967
	internal struct TrampolineSym
	{
		// Token: 0x04000E1E RID: 3614
		internal ushort trampType;

		// Token: 0x04000E1F RID: 3615
		internal ushort cbThunk;

		// Token: 0x04000E20 RID: 3616
		internal uint offThunk;

		// Token: 0x04000E21 RID: 3617
		internal uint offTarget;

		// Token: 0x04000E22 RID: 3618
		internal ushort sectThunk;

		// Token: 0x04000E23 RID: 3619
		internal ushort sectTarget;
	}
}
