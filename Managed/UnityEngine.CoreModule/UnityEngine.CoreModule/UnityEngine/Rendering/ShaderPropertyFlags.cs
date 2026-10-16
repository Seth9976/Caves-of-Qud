using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000572 RID: 1394
	[Flags]
	public enum ShaderPropertyFlags
	{
		// Token: 0x04001247 RID: 4679
		None = 0,
		// Token: 0x04001248 RID: 4680
		HideInInspector = 1,
		// Token: 0x04001249 RID: 4681
		PerRendererData = 2,
		// Token: 0x0400124A RID: 4682
		NoScaleOffset = 4,
		// Token: 0x0400124B RID: 4683
		Normal = 8,
		// Token: 0x0400124C RID: 4684
		HDR = 16,
		// Token: 0x0400124D RID: 4685
		Gamma = 32,
		// Token: 0x0400124E RID: 4686
		NonModifiableTextureData = 64,
		// Token: 0x0400124F RID: 4687
		MainTexture = 128,
		// Token: 0x04001250 RID: 4688
		MainColor = 256
	}
}
