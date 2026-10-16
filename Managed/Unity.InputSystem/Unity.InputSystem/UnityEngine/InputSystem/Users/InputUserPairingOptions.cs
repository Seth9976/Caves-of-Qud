using System;

namespace UnityEngine.InputSystem.Users
{
	// Token: 0x02000083 RID: 131
	[Flags]
	public enum InputUserPairingOptions
	{
		// Token: 0x040003D8 RID: 984
		None = 0,
		// Token: 0x040003D9 RID: 985
		ForcePlatformUserAccountSelection = 1,
		// Token: 0x040003DA RID: 986
		ForceNoPlatformUserAccountSelection = 2,
		// Token: 0x040003DB RID: 987
		UnpairCurrentDevicesFromUser = 8
	}
}
