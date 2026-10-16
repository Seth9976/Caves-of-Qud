using System;
using Unity.Collections;

namespace UnityEngine.Rendering
{
	// Token: 0x02000525 RID: 1317
	internal struct GPUDrivenLODGroupData
	{
		// Token: 0x04001088 RID: 4232
		public NativeArray<int> lodGroupID;

		// Token: 0x04001089 RID: 4233
		public NativeArray<int> lodOffset;

		// Token: 0x0400108A RID: 4234
		public NativeArray<int> lodCount;

		// Token: 0x0400108B RID: 4235
		public NativeArray<LODFadeMode> fadeMode;

		// Token: 0x0400108C RID: 4236
		public NativeArray<Vector3> worldSpaceReferencePoint;

		// Token: 0x0400108D RID: 4237
		public NativeArray<float> worldSpaceSize;

		// Token: 0x0400108E RID: 4238
		public NativeArray<short> renderersCount;

		// Token: 0x0400108F RID: 4239
		public NativeArray<bool> lastLODIsBillboard;

		// Token: 0x04001090 RID: 4240
		public NativeArray<byte> forceLODMask;

		// Token: 0x04001091 RID: 4241
		public NativeArray<int> invalidLODGroupID;

		// Token: 0x04001092 RID: 4242
		public NativeArray<short> lodRenderersCount;

		// Token: 0x04001093 RID: 4243
		public NativeArray<float> lodScreenRelativeTransitionHeight;

		// Token: 0x04001094 RID: 4244
		public NativeArray<float> lodFadeTransitionWidth;
	}
}
