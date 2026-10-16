using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000031 RID: 49
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	[UsedByNativeCode]
	public enum MeshChangeState
	{
		// Token: 0x04000110 RID: 272
		Added,
		// Token: 0x04000111 RID: 273
		Updated,
		// Token: 0x04000112 RID: 274
		Removed,
		// Token: 0x04000113 RID: 275
		Unchanged
	}
}
