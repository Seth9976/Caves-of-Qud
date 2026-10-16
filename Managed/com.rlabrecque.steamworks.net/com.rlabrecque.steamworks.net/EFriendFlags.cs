using System;

namespace Steamworks
{
	// Token: 0x020000F3 RID: 243
	[Flags]
	public enum EFriendFlags
	{
		// Token: 0x04000429 RID: 1065
		k_EFriendFlagNone = 0,
		// Token: 0x0400042A RID: 1066
		k_EFriendFlagBlocked = 1,
		// Token: 0x0400042B RID: 1067
		k_EFriendFlagFriendshipRequested = 2,
		// Token: 0x0400042C RID: 1068
		k_EFriendFlagImmediate = 4,
		// Token: 0x0400042D RID: 1069
		k_EFriendFlagClanMember = 8,
		// Token: 0x0400042E RID: 1070
		k_EFriendFlagOnGameServer = 16,
		// Token: 0x0400042F RID: 1071
		k_EFriendFlagRequestingFriendship = 128,
		// Token: 0x04000430 RID: 1072
		k_EFriendFlagRequestingInfo = 256,
		// Token: 0x04000431 RID: 1073
		k_EFriendFlagIgnored = 512,
		// Token: 0x04000432 RID: 1074
		k_EFriendFlagIgnoredFriend = 1024,
		// Token: 0x04000433 RID: 1075
		k_EFriendFlagChatMember = 4096,
		// Token: 0x04000434 RID: 1076
		k_EFriendFlagAll = 65535
	}
}
