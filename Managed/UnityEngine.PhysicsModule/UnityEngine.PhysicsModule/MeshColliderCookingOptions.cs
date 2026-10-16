using System;

namespace UnityEngine
{
	// Token: 0x0200002D RID: 45
	[Flags]
	public enum MeshColliderCookingOptions
	{
		// Token: 0x040000A9 RID: 169
		None = 0,
		// Token: 0x040000AA RID: 170
		[Obsolete("No longer used because the problem this was trying to solve is gone since Unity 2018.3", true)]
		InflateConvexMesh = 1,
		// Token: 0x040000AB RID: 171
		CookForFasterSimulation = 2,
		// Token: 0x040000AC RID: 172
		EnableMeshCleaning = 4,
		// Token: 0x040000AD RID: 173
		WeldColocatedVertices = 8,
		// Token: 0x040000AE RID: 174
		UseFastMidphase = 16
	}
}
