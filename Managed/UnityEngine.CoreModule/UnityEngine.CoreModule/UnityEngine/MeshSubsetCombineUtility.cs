using System;
using System.Collections.Generic;

namespace UnityEngine
{
	// Token: 0x020002DA RID: 730
	internal class MeshSubsetCombineUtility
	{
		// Token: 0x020002DB RID: 731
		public struct MeshInstance
		{
			// Token: 0x04000A32 RID: 2610
			public int meshInstanceID;

			// Token: 0x04000A33 RID: 2611
			public int rendererInstanceID;

			// Token: 0x04000A34 RID: 2612
			public int additionalVertexStreamsMeshInstanceID;

			// Token: 0x04000A35 RID: 2613
			public int enlightenVertexStreamMeshInstanceID;

			// Token: 0x04000A36 RID: 2614
			public Matrix4x4 transform;

			// Token: 0x04000A37 RID: 2615
			public Vector4 lightmapScaleOffset;

			// Token: 0x04000A38 RID: 2616
			public Vector4 realtimeLightmapScaleOffset;
		}

		// Token: 0x020002DC RID: 732
		public struct SubMeshInstance
		{
			// Token: 0x04000A39 RID: 2617
			public int meshInstanceID;

			// Token: 0x04000A3A RID: 2618
			public int vertexOffset;

			// Token: 0x04000A3B RID: 2619
			public int gameObjectInstanceID;

			// Token: 0x04000A3C RID: 2620
			public int subMeshIndex;

			// Token: 0x04000A3D RID: 2621
			public Matrix4x4 transform;
		}

		// Token: 0x020002DD RID: 733
		public struct MeshContainer
		{
			// Token: 0x04000A3E RID: 2622
			public GameObject gameObject;

			// Token: 0x04000A3F RID: 2623
			public MeshSubsetCombineUtility.MeshInstance instance;

			// Token: 0x04000A40 RID: 2624
			public List<MeshSubsetCombineUtility.SubMeshInstance> subMeshInstances;
		}
	}
}
