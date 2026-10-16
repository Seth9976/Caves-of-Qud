using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000198 RID: 408
	[NativeHeader("Runtime/Camera/SharedLightData.h")]
	public struct LightBakingOutput
	{
		// Token: 0x0400051E RID: 1310
		public int probeOcclusionLightIndex;

		// Token: 0x0400051F RID: 1311
		public int occlusionMaskChannel;

		// Token: 0x04000520 RID: 1312
		[NativeName("lightmapBakeMode.lightmapBakeType")]
		public LightmapBakeType lightmapBakeType;

		// Token: 0x04000521 RID: 1313
		[NativeName("lightmapBakeMode.mixedLightingMode")]
		public MixedLightingMode mixedLightingMode;

		// Token: 0x04000522 RID: 1314
		public bool isBaked;
	}
}
