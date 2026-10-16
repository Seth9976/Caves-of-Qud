using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020004B0 RID: 1200
	public enum BuiltinShaderType
	{
		// Token: 0x04000E0A RID: 3594
		DeferredShading,
		// Token: 0x04000E0B RID: 3595
		DeferredReflections,
		// Token: 0x04000E0C RID: 3596
		[Obsolete("LegacyDeferredLighting has been removed.", false)]
		LegacyDeferredLighting,
		// Token: 0x04000E0D RID: 3597
		ScreenSpaceShadows,
		// Token: 0x04000E0E RID: 3598
		DepthNormals,
		// Token: 0x04000E0F RID: 3599
		MotionVectors,
		// Token: 0x04000E10 RID: 3600
		LightHalo,
		// Token: 0x04000E11 RID: 3601
		LensFlare
	}
}
