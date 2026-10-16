using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000489 RID: 1161
	public enum RenderQueue
	{
		// Token: 0x04000CC9 RID: 3273
		Background = 1000,
		// Token: 0x04000CCA RID: 3274
		Geometry = 2000,
		// Token: 0x04000CCB RID: 3275
		AlphaTest = 2450,
		// Token: 0x04000CCC RID: 3276
		GeometryLast = 2500,
		// Token: 0x04000CCD RID: 3277
		Transparent = 3000,
		// Token: 0x04000CCE RID: 3278
		Overlay = 4000
	}
}
