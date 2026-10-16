using System;

namespace Steamworks
{
	// Token: 0x02000115 RID: 277
	public enum ESNetSocketState
	{
		// Token: 0x040006DB RID: 1755
		k_ESNetSocketStateInvalid,
		// Token: 0x040006DC RID: 1756
		k_ESNetSocketStateConnected,
		// Token: 0x040006DD RID: 1757
		k_ESNetSocketStateInitiated = 10,
		// Token: 0x040006DE RID: 1758
		k_ESNetSocketStateLocalCandidatesFound,
		// Token: 0x040006DF RID: 1759
		k_ESNetSocketStateReceivedRemoteCandidates,
		// Token: 0x040006E0 RID: 1760
		k_ESNetSocketStateChallengeHandshake = 15,
		// Token: 0x040006E1 RID: 1761
		k_ESNetSocketStateDisconnecting = 21,
		// Token: 0x040006E2 RID: 1762
		k_ESNetSocketStateLocalDisconnect,
		// Token: 0x040006E3 RID: 1763
		k_ESNetSocketStateTimeoutDuringConnect,
		// Token: 0x040006E4 RID: 1764
		k_ESNetSocketStateRemoteEndDisconnected,
		// Token: 0x040006E5 RID: 1765
		k_ESNetSocketStateConnectionBroken
	}
}
