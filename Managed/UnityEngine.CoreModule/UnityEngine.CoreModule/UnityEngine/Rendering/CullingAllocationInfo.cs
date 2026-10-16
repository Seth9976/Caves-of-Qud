using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000537 RID: 1335
	internal struct CullingAllocationInfo
	{
		// Token: 0x04001113 RID: 4371
		public unsafe VisibleLight* visibleLightsPtr;

		// Token: 0x04001114 RID: 4372
		public unsafe VisibleLight* visibleOffscreenVertexLightsPtr;

		// Token: 0x04001115 RID: 4373
		public unsafe VisibleReflectionProbe* visibleReflectionProbesPtr;

		// Token: 0x04001116 RID: 4374
		public int visibleLightCount;

		// Token: 0x04001117 RID: 4375
		public int visibleOffscreenVertexLightCount;

		// Token: 0x04001118 RID: 4376
		public int visibleReflectionProbeCount;
	}
}
