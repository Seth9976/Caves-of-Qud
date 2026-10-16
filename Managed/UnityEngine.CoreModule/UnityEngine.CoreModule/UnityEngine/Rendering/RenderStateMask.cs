using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000550 RID: 1360
	[Flags]
	public enum RenderStateMask
	{
		// Token: 0x04001192 RID: 4498
		Nothing = 0,
		// Token: 0x04001193 RID: 4499
		Blend = 1,
		// Token: 0x04001194 RID: 4500
		Raster = 2,
		// Token: 0x04001195 RID: 4501
		Depth = 4,
		// Token: 0x04001196 RID: 4502
		Stencil = 8,
		// Token: 0x04001197 RID: 4503
		Everything = 15
	}
}
