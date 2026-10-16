using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Collections
{
	// Token: 0x020000A1 RID: 161
	[UsedByNativeCode]
	[VisibleToOtherModules(new string[] { "UnityEngine.AIModule" })]
	internal enum LeakCategory
	{
		// Token: 0x04000268 RID: 616
		Invalid,
		// Token: 0x04000269 RID: 617
		Malloc,
		// Token: 0x0400026A RID: 618
		TempJob,
		// Token: 0x0400026B RID: 619
		Persistent,
		// Token: 0x0400026C RID: 620
		LightProbesQuery,
		// Token: 0x0400026D RID: 621
		NativeTest,
		// Token: 0x0400026E RID: 622
		MeshDataArray,
		// Token: 0x0400026F RID: 623
		TransformAccessArray,
		// Token: 0x04000270 RID: 624
		NavMeshQuery
	}
}
