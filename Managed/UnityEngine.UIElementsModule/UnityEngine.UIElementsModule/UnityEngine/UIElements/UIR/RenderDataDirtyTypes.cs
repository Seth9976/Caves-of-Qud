using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000665 RID: 1637
	[Flags]
	internal enum RenderDataDirtyTypes
	{
		// Token: 0x04001730 RID: 5936
		None = 0,
		// Token: 0x04001731 RID: 5937
		Transform = 1,
		// Token: 0x04001732 RID: 5938
		ClipRectSize = 2,
		// Token: 0x04001733 RID: 5939
		Clipping = 4,
		// Token: 0x04001734 RID: 5940
		ClippingHierarchy = 8,
		// Token: 0x04001735 RID: 5941
		Visuals = 16,
		// Token: 0x04001736 RID: 5942
		VisualsHierarchy = 32,
		// Token: 0x04001737 RID: 5943
		VisualsOpacityId = 64,
		// Token: 0x04001738 RID: 5944
		Opacity = 128,
		// Token: 0x04001739 RID: 5945
		OpacityHierarchy = 256,
		// Token: 0x0400173A RID: 5946
		Color = 512,
		// Token: 0x0400173B RID: 5947
		AllVisuals = 112
	}
}
