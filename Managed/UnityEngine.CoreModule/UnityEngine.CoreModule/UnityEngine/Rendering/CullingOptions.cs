using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000530 RID: 1328
	[Flags]
	public enum CullingOptions
	{
		// Token: 0x040010E5 RID: 4325
		None = 0,
		// Token: 0x040010E6 RID: 4326
		ForceEvenIfCameraIsNotActive = 1,
		// Token: 0x040010E7 RID: 4327
		OcclusionCull = 2,
		// Token: 0x040010E8 RID: 4328
		NeedsLighting = 4,
		// Token: 0x040010E9 RID: 4329
		NeedsReflectionProbes = 8,
		// Token: 0x040010EA RID: 4330
		Stereo = 16,
		// Token: 0x040010EB RID: 4331
		DisablePerObjectCulling = 32,
		// Token: 0x040010EC RID: 4332
		ShadowCasters = 64
	}
}
