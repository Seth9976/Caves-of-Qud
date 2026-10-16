using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering
{
	// Token: 0x020004DF RID: 1247
	[MovedFrom("UnityEngine.Experimental.Rendering")]
	public struct RayTracingInstanceCullingConfig
	{
		// Token: 0x04000EF4 RID: 3828
		public RayTracingInstanceCullingFlags flags;

		// Token: 0x04000EF5 RID: 3829
		public Vector3 sphereCenter;

		// Token: 0x04000EF6 RID: 3830
		public float sphereRadius;

		// Token: 0x04000EF7 RID: 3831
		public float minSolidAngle;

		// Token: 0x04000EF8 RID: 3832
		public Plane[] planes;

		// Token: 0x04000EF9 RID: 3833
		public RayTracingInstanceCullingTest[] instanceTests;

		// Token: 0x04000EFA RID: 3834
		public RayTracingInstanceCullingMaterialTest materialTest;

		// Token: 0x04000EFB RID: 3835
		public RayTracingInstanceMaterialConfig transparentMaterialConfig;

		// Token: 0x04000EFC RID: 3836
		public RayTracingInstanceMaterialConfig alphaTestedMaterialConfig;

		// Token: 0x04000EFD RID: 3837
		public RayTracingSubMeshFlagsConfig subMeshFlagsConfig;

		// Token: 0x04000EFE RID: 3838
		public RayTracingInstanceTriangleCullingConfig triangleCullingConfig;

		// Token: 0x04000EFF RID: 3839
		public LODParameters lodParameters;
	}
}
