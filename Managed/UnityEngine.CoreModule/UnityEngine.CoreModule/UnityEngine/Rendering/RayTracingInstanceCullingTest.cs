using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering
{
	// Token: 0x020004D9 RID: 1241
	[MovedFrom("UnityEngine.Experimental.Rendering")]
	public struct RayTracingInstanceCullingTest
	{
		// Token: 0x04000EDE RID: 3806
		public uint instanceMask;

		// Token: 0x04000EDF RID: 3807
		public int layerMask;

		// Token: 0x04000EE0 RID: 3808
		public int shadowCastingModeMask;

		// Token: 0x04000EE1 RID: 3809
		public bool allowOpaqueMaterials;

		// Token: 0x04000EE2 RID: 3810
		public bool allowTransparentMaterials;

		// Token: 0x04000EE3 RID: 3811
		public bool allowAlphaTestedMaterials;

		// Token: 0x04000EE4 RID: 3812
		public bool allowVisualEffects;
	}
}
