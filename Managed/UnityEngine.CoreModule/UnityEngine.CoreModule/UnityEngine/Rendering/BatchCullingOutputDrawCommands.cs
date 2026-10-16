using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000509 RID: 1289
	public struct BatchCullingOutputDrawCommands
	{
		// Token: 0x04000FE3 RID: 4067
		public unsafe BatchDrawCommand* drawCommands;

		// Token: 0x04000FE4 RID: 4068
		public unsafe BatchDrawCommandIndirect* indirectDrawCommands;

		// Token: 0x04000FE5 RID: 4069
		public unsafe BatchDrawCommandProcedural* proceduralDrawCommands;

		// Token: 0x04000FE6 RID: 4070
		public unsafe BatchDrawCommandProceduralIndirect* proceduralIndirectDrawCommands;

		// Token: 0x04000FE7 RID: 4071
		public unsafe int* visibleInstances;

		// Token: 0x04000FE8 RID: 4072
		public unsafe BatchDrawRange* drawRanges;

		// Token: 0x04000FE9 RID: 4073
		public unsafe float* instanceSortingPositions;

		// Token: 0x04000FEA RID: 4074
		public unsafe int* drawCommandPickingInstanceIDs;

		// Token: 0x04000FEB RID: 4075
		public int drawCommandCount;

		// Token: 0x04000FEC RID: 4076
		public int indirectDrawCommandCount;

		// Token: 0x04000FED RID: 4077
		public int proceduralDrawCommandCount;

		// Token: 0x04000FEE RID: 4078
		public int proceduralIndirectDrawCommandCount;

		// Token: 0x04000FEF RID: 4079
		public int visibleInstanceCount;

		// Token: 0x04000FF0 RID: 4080
		public int drawRangeCount;

		// Token: 0x04000FF1 RID: 4081
		public int instanceSortingPositionFloatCount;
	}
}
