using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200002F RID: 47
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	[UsedByNativeCode]
	[Flags]
	public enum MeshVertexAttributes
	{
		// Token: 0x04000107 RID: 263
		None = 0,
		// Token: 0x04000108 RID: 264
		Normals = 1,
		// Token: 0x04000109 RID: 265
		Tangents = 2,
		// Token: 0x0400010A RID: 266
		UVs = 4,
		// Token: 0x0400010B RID: 267
		Colors = 8
	}
}
