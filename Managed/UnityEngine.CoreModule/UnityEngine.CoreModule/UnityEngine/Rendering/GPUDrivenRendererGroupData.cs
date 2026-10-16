using System;
using Unity.Collections;

namespace UnityEngine.Rendering
{
	// Token: 0x02000524 RID: 1316
	internal struct GPUDrivenRendererGroupData
	{
		// Token: 0x0400106C RID: 4204
		public NativeArray<int> rendererGroupID;

		// Token: 0x0400106D RID: 4205
		public NativeArray<Bounds> localBounds;

		// Token: 0x0400106E RID: 4206
		public NativeArray<Vector4> lightmapScaleOffset;

		// Token: 0x0400106F RID: 4207
		public NativeArray<int> gameObjectLayer;

		// Token: 0x04001070 RID: 4208
		public NativeArray<uint> renderingLayerMask;

		// Token: 0x04001071 RID: 4209
		public NativeArray<int> lodGroupID;

		// Token: 0x04001072 RID: 4210
		public NativeArray<int> lightmapIndex;

		// Token: 0x04001073 RID: 4211
		public NativeArray<GPUDrivenPackedRendererData> packedRendererData;

		// Token: 0x04001074 RID: 4212
		public NativeArray<int> rendererPriority;

		// Token: 0x04001075 RID: 4213
		public NativeArray<int> meshIndex;

		// Token: 0x04001076 RID: 4214
		public NativeArray<short> subMeshStartIndex;

		// Token: 0x04001077 RID: 4215
		public NativeArray<int> materialsOffset;

		// Token: 0x04001078 RID: 4216
		public NativeArray<short> materialsCount;

		// Token: 0x04001079 RID: 4217
		public NativeArray<int> instancesOffset;

		// Token: 0x0400107A RID: 4218
		public NativeArray<int> instancesCount;

		// Token: 0x0400107B RID: 4219
		public NativeArray<GPUDrivenRendererEditorData> editorData;

		// Token: 0x0400107C RID: 4220
		public NativeArray<int> invalidRendererGroupID;

		// Token: 0x0400107D RID: 4221
		public NativeArray<Matrix4x4> localToWorldMatrix;

		// Token: 0x0400107E RID: 4222
		public NativeArray<Matrix4x4> prevLocalToWorldMatrix;

		// Token: 0x0400107F RID: 4223
		public NativeArray<int> rendererGroupIndex;

		// Token: 0x04001080 RID: 4224
		public NativeArray<int> meshID;

		// Token: 0x04001081 RID: 4225
		public NativeArray<short> subMeshCount;

		// Token: 0x04001082 RID: 4226
		public NativeArray<int> subMeshDescOffset;

		// Token: 0x04001083 RID: 4227
		public NativeArray<SubMeshDescriptor> subMeshDesc;

		// Token: 0x04001084 RID: 4228
		public NativeArray<int> materialIndex;

		// Token: 0x04001085 RID: 4229
		public NativeArray<int> materialID;

		// Token: 0x04001086 RID: 4230
		public NativeArray<GPUDrivenPackedMaterialData> packedMaterialData;

		// Token: 0x04001087 RID: 4231
		public NativeArray<int> materialFilterFlags;
	}
}
