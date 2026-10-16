using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200002C RID: 44
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	[RequiredByNativeCode]
	public enum MeshGenerationStatus
	{
		// Token: 0x040000F7 RID: 247
		Success,
		// Token: 0x040000F8 RID: 248
		InvalidMeshId,
		// Token: 0x040000F9 RID: 249
		GenerationAlreadyInProgress,
		// Token: 0x040000FA RID: 250
		Canceled,
		// Token: 0x040000FB RID: 251
		UnknownError
	}
}
