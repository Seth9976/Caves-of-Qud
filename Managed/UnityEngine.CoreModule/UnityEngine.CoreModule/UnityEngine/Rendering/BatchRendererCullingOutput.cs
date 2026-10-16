using System;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200050E RID: 1294
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	[UsedByNativeCode]
	internal struct BatchRendererCullingOutput
	{
		// Token: 0x0400100C RID: 4108
		public JobHandle cullingJobsFence;

		// Token: 0x0400100D RID: 4109
		public Matrix4x4 localToWorldMatrix;

		// Token: 0x0400100E RID: 4110
		public unsafe Plane* cullingPlanes;

		// Token: 0x0400100F RID: 4111
		public int cullingPlaneCount;

		// Token: 0x04001010 RID: 4112
		public int receiverPlaneOffset;

		// Token: 0x04001011 RID: 4113
		public int receiverPlaneCount;

		// Token: 0x04001012 RID: 4114
		public unsafe CullingSplit* cullingSplits;

		// Token: 0x04001013 RID: 4115
		public int cullingSplitCount;

		// Token: 0x04001014 RID: 4116
		public BatchCullingViewType viewType;

		// Token: 0x04001015 RID: 4117
		public BatchCullingProjectionType projectionType;

		// Token: 0x04001016 RID: 4118
		public BatchCullingFlags cullingFlags;

		// Token: 0x04001017 RID: 4119
		public ulong viewID;

		// Token: 0x04001018 RID: 4120
		public uint cullingLayerMask;

		// Token: 0x04001019 RID: 4121
		public byte splitExclusionMask;

		// Token: 0x0400101A RID: 4122
		public ulong sceneCullingMask;

		// Token: 0x0400101B RID: 4123
		public unsafe BatchCullingOutputDrawCommands* drawCommands;

		// Token: 0x0400101C RID: 4124
		public uint brgId;

		// Token: 0x0400101D RID: 4125
		public IntPtr occlusionBuffer;

		// Token: 0x0400101E RID: 4126
		public IntPtr customCullingResult;
	}
}
