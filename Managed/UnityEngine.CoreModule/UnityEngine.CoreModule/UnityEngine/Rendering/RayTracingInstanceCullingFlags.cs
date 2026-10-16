using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering
{
	// Token: 0x020004D8 RID: 1240
	[Flags]
	[MovedFrom("UnityEngine.Experimental.Rendering")]
	public enum RayTracingInstanceCullingFlags
	{
		// Token: 0x04000ED7 RID: 3799
		None = 0,
		// Token: 0x04000ED8 RID: 3800
		EnableSphereCulling = 1,
		// Token: 0x04000ED9 RID: 3801
		EnablePlaneCulling = 2,
		// Token: 0x04000EDA RID: 3802
		EnableLODCulling = 4,
		// Token: 0x04000EDB RID: 3803
		ComputeMaterialsCRC = 8,
		// Token: 0x04000EDC RID: 3804
		IgnoreReflectionProbes = 16,
		// Token: 0x04000EDD RID: 3805
		EnableSolidAngleCulling = 32
	}
}
