using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020004A5 RID: 1189
	[Flags]
	public enum RenderTargetFlags
	{
		// Token: 0x04000DDC RID: 3548
		None = 0,
		// Token: 0x04000DDD RID: 3549
		ReadOnlyDepth = 1,
		// Token: 0x04000DDE RID: 3550
		ReadOnlyStencil = 2,
		// Token: 0x04000DDF RID: 3551
		ReadOnlyDepthStencil = 3
	}
}
