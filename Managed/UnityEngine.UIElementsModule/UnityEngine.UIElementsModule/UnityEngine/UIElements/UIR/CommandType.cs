using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000633 RID: 1587
	internal enum CommandType
	{
		// Token: 0x04001594 RID: 5524
		Draw,
		// Token: 0x04001595 RID: 5525
		ImmediateCull,
		// Token: 0x04001596 RID: 5526
		Immediate,
		// Token: 0x04001597 RID: 5527
		PushView,
		// Token: 0x04001598 RID: 5528
		PopView,
		// Token: 0x04001599 RID: 5529
		PushScissor,
		// Token: 0x0400159A RID: 5530
		PopScissor,
		// Token: 0x0400159B RID: 5531
		PushRenderTexture,
		// Token: 0x0400159C RID: 5532
		PopRenderTexture,
		// Token: 0x0400159D RID: 5533
		BlitToPreviousRT,
		// Token: 0x0400159E RID: 5534
		PushDefaultMaterial,
		// Token: 0x0400159F RID: 5535
		PopDefaultMaterial,
		// Token: 0x040015A0 RID: 5536
		BeginDisable,
		// Token: 0x040015A1 RID: 5537
		EndDisable,
		// Token: 0x040015A2 RID: 5538
		CutRenderChain
	}
}
