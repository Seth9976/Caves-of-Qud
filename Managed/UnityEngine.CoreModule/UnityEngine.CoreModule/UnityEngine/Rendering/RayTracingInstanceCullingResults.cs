using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering
{
	// Token: 0x020004E1 RID: 1249
	[MovedFrom("UnityEngine.Experimental.Rendering")]
	public struct RayTracingInstanceCullingResults
	{
		// Token: 0x04000F02 RID: 3842
		public RayTracingInstanceMaterialCRC[] materialsCRC;

		// Token: 0x04000F03 RID: 3843
		public bool transformsChanged;
	}
}
