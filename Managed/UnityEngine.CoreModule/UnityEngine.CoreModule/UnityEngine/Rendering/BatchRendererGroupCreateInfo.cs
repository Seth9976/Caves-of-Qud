using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000510 RID: 1296
	public struct BatchRendererGroupCreateInfo
	{
		// Token: 0x04001020 RID: 4128
		public BatchRendererGroup.OnPerformCulling cullingCallback;

		// Token: 0x04001021 RID: 4129
		public BatchRendererGroup.OnFinishedCulling finishedCullingCallback;

		// Token: 0x04001022 RID: 4130
		public IntPtr userContext;
	}
}
