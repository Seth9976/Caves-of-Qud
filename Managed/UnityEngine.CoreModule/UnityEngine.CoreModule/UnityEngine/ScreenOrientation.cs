using System;

namespace UnityEngine
{
	// Token: 0x020001BF RID: 447
	public enum ScreenOrientation
	{
		// Token: 0x040005D4 RID: 1492
		Portrait = 1,
		// Token: 0x040005D5 RID: 1493
		PortraitUpsideDown,
		// Token: 0x040005D6 RID: 1494
		LandscapeLeft,
		// Token: 0x040005D7 RID: 1495
		LandscapeRight,
		// Token: 0x040005D8 RID: 1496
		AutoRotation,
		// Token: 0x040005D9 RID: 1497
		[Obsolete("Enum member Unknown has been deprecated.", false)]
		Unknown = 0,
		// Token: 0x040005DA RID: 1498
		[Obsolete("Use LandscapeLeft instead (UnityUpgradable) -> LandscapeLeft", true)]
		Landscape = 3
	}
}
