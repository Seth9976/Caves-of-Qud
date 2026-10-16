using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000F4 RID: 244
	[Flags]
	public enum InputUpdateType
	{
		// Token: 0x040005FA RID: 1530
		None = 0,
		// Token: 0x040005FB RID: 1531
		Dynamic = 1,
		// Token: 0x040005FC RID: 1532
		Fixed = 2,
		// Token: 0x040005FD RID: 1533
		BeforeRender = 4,
		// Token: 0x040005FE RID: 1534
		Editor = 8,
		// Token: 0x040005FF RID: 1535
		Manual = 16,
		// Token: 0x04000600 RID: 1536
		Default = 11
	}
}
