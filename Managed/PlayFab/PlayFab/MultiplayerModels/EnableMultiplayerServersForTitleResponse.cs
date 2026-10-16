using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000093 RID: 147
	[Serializable]
	public class EnableMultiplayerServersForTitleResponse : PlayFabResultCommon
	{
		// Token: 0x04000466 RID: 1126
		public TitleMultiplayerServerEnabledStatus? Status;
	}
}
