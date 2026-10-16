using System;
using Unity.Collections;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200050C RID: 1292
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	[UsedByNativeCode]
	public struct BatchCullingContext
	{
		// Token: 0x060031EE RID: 12782 RVA: 0x00060AB4 File Offset: 0x0005ECB4
		internal BatchCullingContext(NativeArray<Plane> inCullingPlanes, NativeArray<CullingSplit> inCullingSplits, LODParameters inLodParameters, Matrix4x4 inLocalToWorldMatrix, BatchCullingViewType inViewType, BatchCullingProjectionType inProjectionType, BatchCullingFlags inBatchCullingFlags, ulong inViewID, uint inCullingLayerMask, ulong inSceneCullingMask, byte inExclusionSplitMask, int inReceiverPlaneOffset, int inReceiverPlaneCount, IntPtr inOcclusionBuffer)
		{
			this.cullingPlanes = inCullingPlanes;
			this.cullingSplits = inCullingSplits;
			this.lodParameters = inLodParameters;
			this.localToWorldMatrix = inLocalToWorldMatrix;
			this.viewType = inViewType;
			this.projectionType = inProjectionType;
			this.cullingFlags = inBatchCullingFlags;
			this.viewID = new BatchPackedCullingViewID
			{
				handle = inViewID
			};
			this.cullingLayerMask = inCullingLayerMask;
			this.sceneCullingMask = inSceneCullingMask;
			this.splitExclusionMask = (ushort)inExclusionSplitMask;
			this.receiverPlaneOffset = inReceiverPlaneOffset;
			this.receiverPlaneCount = inReceiverPlaneCount;
			this.isOrthographic = 0;
			this.occlusionBuffer = inOcclusionBuffer;
		}

		// Token: 0x04000FFB RID: 4091
		public readonly NativeArray<Plane> cullingPlanes;

		// Token: 0x04000FFC RID: 4092
		public readonly NativeArray<CullingSplit> cullingSplits;

		// Token: 0x04000FFD RID: 4093
		public readonly LODParameters lodParameters;

		// Token: 0x04000FFE RID: 4094
		public readonly Matrix4x4 localToWorldMatrix;

		// Token: 0x04000FFF RID: 4095
		public readonly BatchCullingViewType viewType;

		// Token: 0x04001000 RID: 4096
		public readonly BatchCullingProjectionType projectionType;

		// Token: 0x04001001 RID: 4097
		public readonly BatchCullingFlags cullingFlags;

		// Token: 0x04001002 RID: 4098
		public readonly BatchPackedCullingViewID viewID;

		// Token: 0x04001003 RID: 4099
		public readonly uint cullingLayerMask;

		// Token: 0x04001004 RID: 4100
		public readonly ulong sceneCullingMask;

		// Token: 0x04001005 RID: 4101
		public readonly ushort splitExclusionMask;

		// Token: 0x04001006 RID: 4102
		[Obsolete("BatchCullingContext.isOrthographic is deprecated. Use BatchCullingContext.projectionType instead.")]
		public readonly byte isOrthographic;

		// Token: 0x04001007 RID: 4103
		public readonly int receiverPlaneOffset;

		// Token: 0x04001008 RID: 4104
		public readonly int receiverPlaneCount;

		// Token: 0x04001009 RID: 4105
		internal readonly IntPtr occlusionBuffer;
	}
}
