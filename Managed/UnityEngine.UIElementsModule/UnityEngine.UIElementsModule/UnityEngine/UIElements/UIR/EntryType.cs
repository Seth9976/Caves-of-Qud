using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200063C RID: 1596
	internal enum EntryType : ushort
	{
		// Token: 0x040015F3 RID: 5619
		DrawSolidMesh,
		// Token: 0x040015F4 RID: 5620
		DrawTexturedMesh,
		// Token: 0x040015F5 RID: 5621
		DrawTexturedMeshSkipAtlas,
		// Token: 0x040015F6 RID: 5622
		DrawTextMesh,
		// Token: 0x040015F7 RID: 5623
		DrawGradients,
		// Token: 0x040015F8 RID: 5624
		DrawImmediate,
		// Token: 0x040015F9 RID: 5625
		DrawImmediateCull,
		// Token: 0x040015FA RID: 5626
		DrawChildren,
		// Token: 0x040015FB RID: 5627
		BeginStencilMask,
		// Token: 0x040015FC RID: 5628
		EndStencilMask,
		// Token: 0x040015FD RID: 5629
		PopStencilMask,
		// Token: 0x040015FE RID: 5630
		PushClippingRect,
		// Token: 0x040015FF RID: 5631
		PopClippingRect,
		// Token: 0x04001600 RID: 5632
		PushScissors,
		// Token: 0x04001601 RID: 5633
		PopScissors,
		// Token: 0x04001602 RID: 5634
		PushGroupMatrix,
		// Token: 0x04001603 RID: 5635
		PopGroupMatrix,
		// Token: 0x04001604 RID: 5636
		PushRenderTexture,
		// Token: 0x04001605 RID: 5637
		BlitAndPopRenderTexture,
		// Token: 0x04001606 RID: 5638
		PushDefaultMaterial,
		// Token: 0x04001607 RID: 5639
		PopDefaultMaterial,
		// Token: 0x04001608 RID: 5640
		CutRenderChain,
		// Token: 0x04001609 RID: 5641
		DedicatedPlaceholder
	}
}
