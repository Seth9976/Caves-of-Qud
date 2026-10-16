using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000565 RID: 1381
	[Flags]
	internal enum VisibleLightFlags
	{
		// Token: 0x04001219 RID: 4633
		IntersectsNearPlane = 1,
		// Token: 0x0400121A RID: 4634
		IntersectsFarPlane = 2,
		// Token: 0x0400121B RID: 4635
		ForcedVisible = 4
	}
}
