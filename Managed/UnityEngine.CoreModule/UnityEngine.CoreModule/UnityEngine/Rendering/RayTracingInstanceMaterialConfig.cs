using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering
{
	// Token: 0x020004DB RID: 1243
	[MovedFrom("UnityEngine.Experimental.Rendering")]
	public struct RayTracingInstanceMaterialConfig
	{
		// Token: 0x04000EE7 RID: 3815
		public int renderQueueLowerBound;

		// Token: 0x04000EE8 RID: 3816
		public int renderQueueUpperBound;

		// Token: 0x04000EE9 RID: 3817
		public RayTracingInstanceCullingShaderTagConfig[] optionalShaderTags;

		// Token: 0x04000EEA RID: 3818
		public string[] optionalShaderKeywords;
	}
}
