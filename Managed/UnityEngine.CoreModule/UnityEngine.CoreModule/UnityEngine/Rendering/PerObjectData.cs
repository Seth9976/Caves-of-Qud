using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000542 RID: 1346
	[Flags]
	public enum PerObjectData
	{
		// Token: 0x04001143 RID: 4419
		None = 0,
		// Token: 0x04001144 RID: 4420
		LightProbe = 1,
		// Token: 0x04001145 RID: 4421
		ReflectionProbes = 2,
		// Token: 0x04001146 RID: 4422
		LightProbeProxyVolume = 4,
		// Token: 0x04001147 RID: 4423
		Lightmaps = 8,
		// Token: 0x04001148 RID: 4424
		LightData = 16,
		// Token: 0x04001149 RID: 4425
		MotionVectors = 32,
		// Token: 0x0400114A RID: 4426
		LightIndices = 64,
		// Token: 0x0400114B RID: 4427
		ReflectionProbeData = 128,
		// Token: 0x0400114C RID: 4428
		OcclusionProbe = 256,
		// Token: 0x0400114D RID: 4429
		OcclusionProbeProxyVolume = 512,
		// Token: 0x0400114E RID: 4430
		ShadowMask = 1024
	}
}
