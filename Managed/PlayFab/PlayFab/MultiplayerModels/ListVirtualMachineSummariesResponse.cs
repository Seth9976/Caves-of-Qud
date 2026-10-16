using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000D5 RID: 213
	[Serializable]
	public class ListVirtualMachineSummariesResponse : PlayFabResultCommon
	{
		// Token: 0x04000528 RID: 1320
		public int PageSize;

		// Token: 0x04000529 RID: 1321
		public string SkipToken;

		// Token: 0x0400052A RID: 1322
		public List<VirtualMachineSummary> VirtualMachines;
	}
}
