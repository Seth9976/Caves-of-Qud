using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200055B RID: 1371
	[Flags]
	public enum SortingCriteria
	{
		// Token: 0x040011C2 RID: 4546
		None = 0,
		// Token: 0x040011C3 RID: 4547
		SortingLayer = 1,
		// Token: 0x040011C4 RID: 4548
		RenderQueue = 2,
		// Token: 0x040011C5 RID: 4549
		BackToFront = 4,
		// Token: 0x040011C6 RID: 4550
		QuantizedFrontToBack = 8,
		// Token: 0x040011C7 RID: 4551
		OptimizeStateChanges = 16,
		// Token: 0x040011C8 RID: 4552
		CanvasOrder = 32,
		// Token: 0x040011C9 RID: 4553
		RendererPriority = 64,
		// Token: 0x040011CA RID: 4554
		CommonOpaque = 59,
		// Token: 0x040011CB RID: 4555
		CommonTransparent = 23
	}
}
