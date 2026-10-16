using System;

namespace Steamworks
{
	// Token: 0x0200014D RID: 333
	[Flags]
	public enum EBetaBranchFlags
	{
		// Token: 0x04000921 RID: 2337
		k_EBetaBranch_None = 0,
		// Token: 0x04000922 RID: 2338
		k_EBetaBranch_Default = 1,
		// Token: 0x04000923 RID: 2339
		k_EBetaBranch_Available = 2,
		// Token: 0x04000924 RID: 2340
		k_EBetaBranch_Private = 4,
		// Token: 0x04000925 RID: 2341
		k_EBetaBranch_Selected = 8,
		// Token: 0x04000926 RID: 2342
		k_EBetaBranch_Installed = 16
	}
}
