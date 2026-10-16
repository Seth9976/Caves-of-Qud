using System;
using Unity.Collections;

namespace UnityEngine.Rendering
{
	// Token: 0x02000535 RID: 1333
	public struct ShadowCastersCullingInfos
	{
		// Token: 0x0400110F RID: 4367
		public NativeArray<ShadowSplitData> splitBuffer;

		// Token: 0x04001110 RID: 4368
		public NativeArray<LightShadowCasterCullingInfo> perLightInfos;
	}
}
