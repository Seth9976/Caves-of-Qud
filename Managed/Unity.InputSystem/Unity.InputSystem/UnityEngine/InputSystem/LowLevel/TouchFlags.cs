using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000DB RID: 219
	[Flags]
	internal enum TouchFlags : byte
	{
		// Token: 0x04000585 RID: 1413
		IndirectTouch = 1,
		// Token: 0x04000586 RID: 1414
		PrimaryTouch = 8,
		// Token: 0x04000587 RID: 1415
		TapPress = 16,
		// Token: 0x04000588 RID: 1416
		TapRelease = 32,
		// Token: 0x04000589 RID: 1417
		OrphanedPrimaryTouch = 64,
		// Token: 0x0400058A RID: 1418
		BeganInSameFrame = 128
	}
}
