using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001E6 RID: 486
	[UsedByNativeCode]
	public struct LOD
	{
		// Token: 0x0600152D RID: 5421 RVA: 0x00023CAD File Offset: 0x00021EAD
		public LOD(float screenRelativeTransitionHeight, Renderer[] renderers)
		{
			this.screenRelativeTransitionHeight = screenRelativeTransitionHeight;
			this.fadeTransitionWidth = 0f;
			this.renderers = renderers;
		}

		// Token: 0x040006C1 RID: 1729
		public float screenRelativeTransitionHeight;

		// Token: 0x040006C2 RID: 1730
		public float fadeTransitionWidth;

		// Token: 0x040006C3 RID: 1731
		public Renderer[] renderers;
	}
}
