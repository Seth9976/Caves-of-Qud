using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000481 RID: 1153
	[Flags]
	public enum RayTracingAccelerationStructureBuildFlags
	{
		// Token: 0x04000C90 RID: 3216
		None = 0,
		// Token: 0x04000C91 RID: 3217
		PreferFastTrace = 1,
		// Token: 0x04000C92 RID: 3218
		PreferFastBuild = 2,
		// Token: 0x04000C93 RID: 3219
		MinimizeMemory = 4
	}
}
