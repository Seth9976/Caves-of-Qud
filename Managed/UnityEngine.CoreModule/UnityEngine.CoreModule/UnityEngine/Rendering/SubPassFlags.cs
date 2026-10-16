using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000527 RID: 1319
	[Flags]
	public enum SubPassFlags
	{
		// Token: 0x0400109E RID: 4254
		None = 0,
		// Token: 0x0400109F RID: 4255
		ReadOnlyDepth = 2,
		// Token: 0x040010A0 RID: 4256
		ReadOnlyStencil = 4,
		// Token: 0x040010A1 RID: 4257
		ReadOnlyDepthStencil = 6
	}
}
