using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering
{
	// Token: 0x020004D7 RID: 1239
	[MovedFrom("UnityEngine.Experimental.Rendering")]
	[Flags]
	[NativeHeader("Runtime/Export/Graphics/RayTracingAccelerationStructure.bindings.h")]
	[NativeHeader("Runtime/Shaders/RayTracing/RayTracingAccelerationStructure.h")]
	[UsedByNativeCode]
	public enum RayTracingSubMeshFlags
	{
		// Token: 0x04000ED2 RID: 3794
		Disabled = 0,
		// Token: 0x04000ED3 RID: 3795
		Enabled = 1,
		// Token: 0x04000ED4 RID: 3796
		ClosestHitOnly = 2,
		// Token: 0x04000ED5 RID: 3797
		UniqueAnyHitCalls = 4
	}
}
