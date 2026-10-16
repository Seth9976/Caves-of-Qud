using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200051F RID: 1311
	[UsedByNativeCode]
	internal struct GPUDrivenLODGroupDataNative
	{
		// Token: 0x04001050 RID: 4176
		public unsafe int* lodGroupID;

		// Token: 0x04001051 RID: 4177
		public unsafe int* lodOffset;

		// Token: 0x04001052 RID: 4178
		public unsafe int* lodCount;

		// Token: 0x04001053 RID: 4179
		public unsafe LODFadeMode* fadeMode;

		// Token: 0x04001054 RID: 4180
		public unsafe Vector3* worldSpaceReferencePoint;

		// Token: 0x04001055 RID: 4181
		public unsafe float* worldSpaceSize;

		// Token: 0x04001056 RID: 4182
		public unsafe short* renderersCount;

		// Token: 0x04001057 RID: 4183
		public unsafe bool* lastLODIsBillboard;

		// Token: 0x04001058 RID: 4184
		public unsafe byte* forceLODMask;

		// Token: 0x04001059 RID: 4185
		public int lodGroupCount;

		// Token: 0x0400105A RID: 4186
		public unsafe int* invalidLODGroupID;

		// Token: 0x0400105B RID: 4187
		public int invalidLODGroupCount;

		// Token: 0x0400105C RID: 4188
		public unsafe short* lodRenderersCount;

		// Token: 0x0400105D RID: 4189
		public unsafe float* lodScreenRelativeTransitionHeight;

		// Token: 0x0400105E RID: 4190
		public unsafe float* lodFadeTransitionWidth;

		// Token: 0x0400105F RID: 4191
		public int lodDataCount;
	}
}
