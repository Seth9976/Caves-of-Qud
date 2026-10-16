using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	// Token: 0x0200004D RID: 77
	[Serializable]
	public class GetTitlePlayersFromMasterPlayerAccountIdsResponse : PlayFabResultCommon
	{
		// Token: 0x04000327 RID: 807
		public string TitleId;

		// Token: 0x04000328 RID: 808
		public Dictionary<string, EntityKey> TitlePlayerAccounts;
	}
}
