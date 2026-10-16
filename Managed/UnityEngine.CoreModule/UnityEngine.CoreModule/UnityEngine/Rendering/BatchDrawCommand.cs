using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000503 RID: 1283
	public struct BatchDrawCommand
	{
		// Token: 0x04000FA0 RID: 4000
		public BatchDrawCommandFlags flags;

		// Token: 0x04000FA1 RID: 4001
		public BatchID batchID;

		// Token: 0x04000FA2 RID: 4002
		public BatchMaterialID materialID;

		// Token: 0x04000FA3 RID: 4003
		public ushort splitVisibilityMask;

		// Token: 0x04000FA4 RID: 4004
		public ushort lightmapIndex;

		// Token: 0x04000FA5 RID: 4005
		public int sortingPosition;

		// Token: 0x04000FA6 RID: 4006
		public uint visibleOffset;

		// Token: 0x04000FA7 RID: 4007
		public uint visibleCount;

		// Token: 0x04000FA8 RID: 4008
		public BatchMeshID meshID;

		// Token: 0x04000FA9 RID: 4009
		public ushort submeshIndex;

		// Token: 0x04000FAA RID: 4010
		private ushort unusedPadding2;
	}
}
