using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200050B RID: 1291
	[UsedByNativeCode]
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	public struct CullingSplit
	{
		// Token: 0x04000FF4 RID: 4084
		public Vector3 sphereCenter;

		// Token: 0x04000FF5 RID: 4085
		public float sphereRadius;

		// Token: 0x04000FF6 RID: 4086
		public int cullingPlaneOffset;

		// Token: 0x04000FF7 RID: 4087
		public int cullingPlaneCount;

		// Token: 0x04000FF8 RID: 4088
		public float cascadeBlendCullingFactor;

		// Token: 0x04000FF9 RID: 4089
		public float nearPlane;

		// Token: 0x04000FFA RID: 4090
		public Matrix4x4 cullingMatrix;
	}
}
