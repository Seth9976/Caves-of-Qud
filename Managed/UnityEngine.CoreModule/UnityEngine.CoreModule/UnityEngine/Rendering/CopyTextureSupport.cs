using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020004B5 RID: 1205
	[Flags]
	public enum CopyTextureSupport
	{
		// Token: 0x04000E4C RID: 3660
		None = 0,
		// Token: 0x04000E4D RID: 3661
		Basic = 1,
		// Token: 0x04000E4E RID: 3662
		Copy3D = 2,
		// Token: 0x04000E4F RID: 3663
		DifferentTypes = 4,
		// Token: 0x04000E50 RID: 3664
		TextureToRT = 8,
		// Token: 0x04000E51 RID: 3665
		RTToTexture = 16
	}
}
