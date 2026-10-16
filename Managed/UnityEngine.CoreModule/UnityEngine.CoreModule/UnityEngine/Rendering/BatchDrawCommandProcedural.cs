using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000505 RID: 1285
	public struct BatchDrawCommandProcedural
	{
		// Token: 0x04000FB9 RID: 4025
		public BatchDrawCommandFlags flags;

		// Token: 0x04000FBA RID: 4026
		public BatchID batchID;

		// Token: 0x04000FBB RID: 4027
		public BatchMaterialID materialID;

		// Token: 0x04000FBC RID: 4028
		public ushort splitVisibilityMask;

		// Token: 0x04000FBD RID: 4029
		public ushort lightmapIndex;

		// Token: 0x04000FBE RID: 4030
		public int sortingPosition;

		// Token: 0x04000FBF RID: 4031
		public uint visibleOffset;

		// Token: 0x04000FC0 RID: 4032
		public uint visibleCount;

		// Token: 0x04000FC1 RID: 4033
		public MeshTopology topology;

		// Token: 0x04000FC2 RID: 4034
		public GraphicsBufferHandle indexBufferHandle;

		// Token: 0x04000FC3 RID: 4035
		public uint baseVertex;

		// Token: 0x04000FC4 RID: 4036
		public uint indexOffsetBytes;

		// Token: 0x04000FC5 RID: 4037
		public uint elementCount;
	}
}
