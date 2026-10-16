using System;

namespace XRL.World
{
	// Token: 0x0200098F RID: 2447
	public enum Suspendability
	{
		// Token: 0x04003033 RID: 12339
		Active,
		// Token: 0x04003034 RID: 12340
		Pinned,
		// Token: 0x04003035 RID: 12341
		TooRecentlyActive,
		// Token: 0x04003036 RID: 12342
		InsideInterior,
		// Token: 0x04003037 RID: 12343
		CompanionWhilePlayerIsOnWorldMap,
		// Token: 0x04003038 RID: 12344
		CompanionTryingToJoinPartyLeader,
		// Token: 0x04003039 RID: 12345
		Suspendable
	}
}
