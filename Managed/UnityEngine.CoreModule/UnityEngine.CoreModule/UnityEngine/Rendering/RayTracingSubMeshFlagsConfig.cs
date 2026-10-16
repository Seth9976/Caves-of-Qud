using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering
{
	// Token: 0x020004DE RID: 1246
	[MovedFrom("UnityEngine.Experimental.Rendering")]
	public struct RayTracingSubMeshFlagsConfig
	{
		// Token: 0x04000EF1 RID: 3825
		public RayTracingSubMeshFlags opaqueMaterials;

		// Token: 0x04000EF2 RID: 3826
		public RayTracingSubMeshFlags transparentMaterials;

		// Token: 0x04000EF3 RID: 3827
		public RayTracingSubMeshFlags alphaTestedMaterials;
	}
}
