using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020004BE RID: 1214
	[Flags]
	public enum RTClearFlags
	{
		// Token: 0x04000E72 RID: 3698
		None = 0,
		// Token: 0x04000E73 RID: 3699
		Color = 1,
		// Token: 0x04000E74 RID: 3700
		Depth = 2,
		// Token: 0x04000E75 RID: 3701
		Stencil = 4,
		// Token: 0x04000E76 RID: 3702
		All = 7,
		// Token: 0x04000E77 RID: 3703
		DepthStencil = 6,
		// Token: 0x04000E78 RID: 3704
		ColorDepth = 3,
		// Token: 0x04000E79 RID: 3705
		ColorStencil = 5
	}
}
