using System;

namespace UnityEngine
{
	// Token: 0x020001A3 RID: 419
	public enum LightType
	{
		// Token: 0x0400054A RID: 1354
		Spot,
		// Token: 0x0400054B RID: 1355
		Directional,
		// Token: 0x0400054C RID: 1356
		Point,
		// Token: 0x0400054D RID: 1357
		[Obsolete("Enum member LightType.Area has been deprecated. Use LightType.Rectangle instead (UnityUpgradable) -> Rectangle", true)]
		Area,
		// Token: 0x0400054E RID: 1358
		Rectangle = 3,
		// Token: 0x0400054F RID: 1359
		Disc,
		// Token: 0x04000550 RID: 1360
		Pyramid,
		// Token: 0x04000551 RID: 1361
		Box,
		// Token: 0x04000552 RID: 1362
		Tube
	}
}
