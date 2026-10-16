using System;

namespace Steamworks
{
	// Token: 0x0200010E RID: 270
	[Flags]
	public enum EChatMemberStateChange
	{
		// Token: 0x040006B6 RID: 1718
		k_EChatMemberStateChangeEntered = 1,
		// Token: 0x040006B7 RID: 1719
		k_EChatMemberStateChangeLeft = 2,
		// Token: 0x040006B8 RID: 1720
		k_EChatMemberStateChangeDisconnected = 4,
		// Token: 0x040006B9 RID: 1721
		k_EChatMemberStateChangeKicked = 8,
		// Token: 0x040006BA RID: 1722
		k_EChatMemberStateChangeBanned = 16
	}
}
