using System;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000010 RID: 16
	[Flags]
	public enum TileAnimationFlags
	{
		// Token: 0x0400003D RID: 61
		None = 0,
		// Token: 0x0400003E RID: 62
		LoopOnce = 1,
		// Token: 0x0400003F RID: 63
		PauseAnimation = 2,
		// Token: 0x04000040 RID: 64
		UpdatePhysics = 4,
		// Token: 0x04000041 RID: 65
		UnscaledTime = 8,
		// Token: 0x04000042 RID: 66
		SyncAnimation = 16
	}
}
