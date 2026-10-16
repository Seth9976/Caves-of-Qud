using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020004BB RID: 1211
	[Flags]
	public enum FoveatedRenderingCaps
	{
		// Token: 0x04000E67 RID: 3687
		None = 0,
		// Token: 0x04000E68 RID: 3688
		FoveationImage = 1,
		// Token: 0x04000E69 RID: 3689
		NonUniformRaster = 2,
		// Token: 0x04000E6A RID: 3690
		ModeChangeOnlyBeforeRenderTargetSet = 4
	}
}
