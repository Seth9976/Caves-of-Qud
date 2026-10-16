using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000504 RID: 1284
	public struct BatchDrawCommandIndirect
	{
		// Token: 0x04000FAB RID: 4011
		public BatchDrawCommandFlags flags;

		// Token: 0x04000FAC RID: 4012
		public BatchID batchID;

		// Token: 0x04000FAD RID: 4013
		public BatchMaterialID materialID;

		// Token: 0x04000FAE RID: 4014
		public ushort splitVisibilityMask;

		// Token: 0x04000FAF RID: 4015
		public ushort lightmapIndex;

		// Token: 0x04000FB0 RID: 4016
		public int sortingPosition;

		// Token: 0x04000FB1 RID: 4017
		public uint visibleOffset;

		// Token: 0x04000FB2 RID: 4018
		public BatchMeshID meshID;

		// Token: 0x04000FB3 RID: 4019
		public MeshTopology topology;

		// Token: 0x04000FB4 RID: 4020
		public GraphicsBufferHandle visibleInstancesBufferHandle;

		// Token: 0x04000FB5 RID: 4021
		public uint visibleInstancesBufferWindowOffset;

		// Token: 0x04000FB6 RID: 4022
		public uint visibleInstancesBufferWindowSizeBytes;

		// Token: 0x04000FB7 RID: 4023
		public GraphicsBufferHandle indirectArgsBufferHandle;

		// Token: 0x04000FB8 RID: 4024
		public uint indirectArgsBufferOffset;
	}
}
