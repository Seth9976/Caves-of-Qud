using System;

namespace UnityEngine
{
	// Token: 0x020001CE RID: 462
	[Flags]
	public enum MaterialGlobalIlluminationFlags
	{
		// Token: 0x04000677 RID: 1655
		None = 0,
		// Token: 0x04000678 RID: 1656
		RealtimeEmissive = 1,
		// Token: 0x04000679 RID: 1657
		BakedEmissive = 2,
		// Token: 0x0400067A RID: 1658
		EmissiveIsBlack = 4,
		// Token: 0x0400067B RID: 1659
		AnyEmissive = 3
	}
}
