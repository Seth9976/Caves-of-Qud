using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering
{
	// Token: 0x020004DD RID: 1245
	[MovedFrom("UnityEngine.Experimental.Rendering")]
	public struct RayTracingInstanceTriangleCullingConfig
	{
		// Token: 0x04000EED RID: 3821
		public string[] optionalDoubleSidedShaderKeywords;

		// Token: 0x04000EEE RID: 3822
		public bool frontTriangleCounterClockwise;

		// Token: 0x04000EEF RID: 3823
		public bool checkDoubleSidedGIMaterial;

		// Token: 0x04000EF0 RID: 3824
		public bool forceDoubleSided;
	}
}
