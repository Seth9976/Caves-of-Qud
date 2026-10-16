using System;

namespace UnityEngine.VFX
{
	// Token: 0x02000009 RID: 9
	internal enum VFXSystemFlag
	{
		// Token: 0x040000DB RID: 219
		SystemDefault,
		// Token: 0x040000DC RID: 220
		SystemHasKill,
		// Token: 0x040000DD RID: 221
		SystemHasIndirectBuffer,
		// Token: 0x040000DE RID: 222
		SystemReceivedEventGPU = 4,
		// Token: 0x040000DF RID: 223
		SystemHasStrips = 8,
		// Token: 0x040000E0 RID: 224
		SystemNeedsComputeBounds = 16,
		// Token: 0x040000E1 RID: 225
		SystemAutomaticBounds = 32,
		// Token: 0x040000E2 RID: 226
		SystemInWorldSpace = 64,
		// Token: 0x040000E3 RID: 227
		SystemHasDirectLink = 128,
		// Token: 0x040000E4 RID: 228
		SystemHasAttributeBuffer = 256,
		// Token: 0x040000E5 RID: 229
		SystemUsesInstancedRendering = 512,
		// Token: 0x040000E6 RID: 230
		SystemIsRayTraced = 1024
	}
}
