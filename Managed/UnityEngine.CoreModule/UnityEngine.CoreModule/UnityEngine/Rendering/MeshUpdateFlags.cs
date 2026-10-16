using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000483 RID: 1155
	[Flags]
	public enum MeshUpdateFlags
	{
		// Token: 0x04000C98 RID: 3224
		Default = 0,
		// Token: 0x04000C99 RID: 3225
		DontValidateIndices = 1,
		// Token: 0x04000C9A RID: 3226
		DontResetBoneBounds = 2,
		// Token: 0x04000C9B RID: 3227
		DontNotifyMeshUsers = 4,
		// Token: 0x04000C9C RID: 3228
		DontRecalculateBounds = 8
	}
}
