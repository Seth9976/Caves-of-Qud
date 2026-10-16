using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.UIElements
{
	// Token: 0x020002EE RID: 750
	internal struct MeshWriteDataInterface
	{
		// Token: 0x06001553 RID: 5459 RVA: 0x00055764 File Offset: 0x00053964
		public static MeshWriteDataInterface FromMeshWriteData(MeshWriteData data)
		{
			return new MeshWriteDataInterface
			{
				vertices = new IntPtr(data.m_Vertices.GetUnsafePtr<Vertex>()),
				indices = new IntPtr(data.m_Indices.GetUnsafePtr<ushort>()),
				vertexCount = data.m_Vertices.Length,
				indexCount = data.m_Indices.Length
			};
		}

		// Token: 0x04000B8C RID: 2956
		public IntPtr vertices;

		// Token: 0x04000B8D RID: 2957
		public IntPtr indices;

		// Token: 0x04000B8E RID: 2958
		public int vertexCount;

		// Token: 0x04000B8F RID: 2959
		public int indexCount;
	}
}
