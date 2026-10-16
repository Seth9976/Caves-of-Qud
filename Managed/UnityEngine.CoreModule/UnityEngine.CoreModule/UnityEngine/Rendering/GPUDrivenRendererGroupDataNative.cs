using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200051E RID: 1310
	[UsedByNativeCode]
	internal struct GPUDrivenRendererGroupDataNative
	{
		// Token: 0x0400102D RID: 4141
		public unsafe int* rendererGroupID;

		// Token: 0x0400102E RID: 4142
		public unsafe Bounds* localBounds;

		// Token: 0x0400102F RID: 4143
		public unsafe Vector4* lightmapScaleOffset;

		// Token: 0x04001030 RID: 4144
		public unsafe int* gameObjectLayer;

		// Token: 0x04001031 RID: 4145
		public unsafe uint* renderingLayerMask;

		// Token: 0x04001032 RID: 4146
		public unsafe int* lodGroupID;

		// Token: 0x04001033 RID: 4147
		public unsafe MotionVectorGenerationMode* motionVecGenMode;

		// Token: 0x04001034 RID: 4148
		public unsafe GPUDrivenPackedRendererData* packedRendererData;

		// Token: 0x04001035 RID: 4149
		public unsafe int* rendererPriority;

		// Token: 0x04001036 RID: 4150
		public unsafe int* meshIndex;

		// Token: 0x04001037 RID: 4151
		public unsafe short* subMeshStartIndex;

		// Token: 0x04001038 RID: 4152
		public unsafe int* materialsOffset;

		// Token: 0x04001039 RID: 4153
		public unsafe short* materialsCount;

		// Token: 0x0400103A RID: 4154
		public unsafe int* instancesOffset;

		// Token: 0x0400103B RID: 4155
		public unsafe int* instancesCount;

		// Token: 0x0400103C RID: 4156
		public unsafe GPUDrivenRendererEditorData* editorData;

		// Token: 0x0400103D RID: 4157
		public int rendererGroupCount;

		// Token: 0x0400103E RID: 4158
		public unsafe int* invalidRendererGroupID;

		// Token: 0x0400103F RID: 4159
		public int invalidRendererGroupIDCount;

		// Token: 0x04001040 RID: 4160
		public unsafe Matrix4x4* localToWorldMatrix;

		// Token: 0x04001041 RID: 4161
		public unsafe Matrix4x4* prevLocalToWorldMatrix;

		// Token: 0x04001042 RID: 4162
		public unsafe int* rendererGroupIndex;

		// Token: 0x04001043 RID: 4163
		public int instanceCount;

		// Token: 0x04001044 RID: 4164
		public unsafe int* meshID;

		// Token: 0x04001045 RID: 4165
		public unsafe short* subMeshCount;

		// Token: 0x04001046 RID: 4166
		public unsafe int* subMeshDescOffset;

		// Token: 0x04001047 RID: 4167
		public int meshCount;

		// Token: 0x04001048 RID: 4168
		public unsafe SubMeshDescriptor* subMeshDesc;

		// Token: 0x04001049 RID: 4169
		public int subMeshDescCount;

		// Token: 0x0400104A RID: 4170
		public unsafe int* materialIndex;

		// Token: 0x0400104B RID: 4171
		public int materialIndexCount;

		// Token: 0x0400104C RID: 4172
		public unsafe int* materialID;

		// Token: 0x0400104D RID: 4173
		public unsafe GPUDrivenPackedMaterialData* packedMaterialData;

		// Token: 0x0400104E RID: 4174
		public unsafe int* materialFilterFlags;

		// Token: 0x0400104F RID: 4175
		public int materialCount;
	}
}
