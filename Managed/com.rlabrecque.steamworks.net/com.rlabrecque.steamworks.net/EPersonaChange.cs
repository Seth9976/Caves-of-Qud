using System;

namespace Steamworks
{
	// Token: 0x020000F9 RID: 249
	[Flags]
	public enum EPersonaChange
	{
		// Token: 0x04000459 RID: 1113
		k_EPersonaChangeName = 1,
		// Token: 0x0400045A RID: 1114
		k_EPersonaChangeStatus = 2,
		// Token: 0x0400045B RID: 1115
		k_EPersonaChangeComeOnline = 4,
		// Token: 0x0400045C RID: 1116
		k_EPersonaChangeGoneOffline = 8,
		// Token: 0x0400045D RID: 1117
		k_EPersonaChangeGamePlayed = 16,
		// Token: 0x0400045E RID: 1118
		k_EPersonaChangeGameServer = 32,
		// Token: 0x0400045F RID: 1119
		k_EPersonaChangeAvatar = 64,
		// Token: 0x04000460 RID: 1120
		k_EPersonaChangeJoinedSource = 128,
		// Token: 0x04000461 RID: 1121
		k_EPersonaChangeLeftSource = 256,
		// Token: 0x04000462 RID: 1122
		k_EPersonaChangeRelationshipChanged = 512,
		// Token: 0x04000463 RID: 1123
		k_EPersonaChangeNameFirstSet = 1024,
		// Token: 0x04000464 RID: 1124
		k_EPersonaChangeBroadcast = 2048,
		// Token: 0x04000465 RID: 1125
		k_EPersonaChangeNickname = 4096,
		// Token: 0x04000466 RID: 1126
		k_EPersonaChangeSteamLevel = 8192,
		// Token: 0x04000467 RID: 1127
		k_EPersonaChangeRichPresence = 16384
	}
}
