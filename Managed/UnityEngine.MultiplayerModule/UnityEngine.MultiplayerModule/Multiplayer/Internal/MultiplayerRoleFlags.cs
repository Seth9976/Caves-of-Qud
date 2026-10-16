using System;
using UnityEngine.Bindings;

namespace UnityEngine.Multiplayer.Internal
{
	// Token: 0x02000005 RID: 5
	[Flags]
	[VisibleToOtherModules]
	internal enum MultiplayerRoleFlags
	{
		// Token: 0x04000005 RID: 5
		Client = 1,
		// Token: 0x04000006 RID: 6
		Server = 2,
		// Token: 0x04000007 RID: 7
		ClientAndServer = 3
	}
}
