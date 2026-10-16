using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000506 RID: 1286
	public struct BatchDrawCommandProceduralIndirect
	{
		// Token: 0x04000FC6 RID: 4038
		public BatchDrawCommandFlags flags;

		// Token: 0x04000FC7 RID: 4039
		public BatchID batchID;

		// Token: 0x04000FC8 RID: 4040
		public BatchMaterialID materialID;

		// Token: 0x04000FC9 RID: 4041
		public ushort splitVisibilityMask;

		// Token: 0x04000FCA RID: 4042
		public ushort lightmapIndex;

		// Token: 0x04000FCB RID: 4043
		public int sortingPosition;

		// Token: 0x04000FCC RID: 4044
		public uint visibleOffset;

		// Token: 0x04000FCD RID: 4045
		public MeshTopology topology;

		// Token: 0x04000FCE RID: 4046
		public GraphicsBufferHandle indexBufferHandle;

		// Token: 0x04000FCF RID: 4047
		public GraphicsBufferHandle visibleInstancesBufferHandle;

		// Token: 0x04000FD0 RID: 4048
		public uint visibleInstancesBufferWindowOffset;

		// Token: 0x04000FD1 RID: 4049
		public uint visibleInstancesBufferWindowSizeBytes;

		// Token: 0x04000FD2 RID: 4050
		public GraphicsBufferHandle indirectArgsBufferHandle;

		// Token: 0x04000FD3 RID: 4051
		public uint indirectArgsBufferOffset;
	}
}
