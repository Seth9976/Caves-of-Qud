using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000030 RID: 48
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	[Flags]
	[UsedByNativeCode]
	public enum MeshGenerationOptions
	{
		// Token: 0x0400010D RID: 269
		None = 0,
		// Token: 0x0400010E RID: 270
		ConsumeTransform = 1
	}
}
