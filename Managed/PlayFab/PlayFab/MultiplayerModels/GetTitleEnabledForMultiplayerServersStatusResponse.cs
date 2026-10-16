using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000B0 RID: 176
	[Serializable]
	public class GetTitleEnabledForMultiplayerServersStatusResponse : PlayFabResultCommon
	{
		// Token: 0x040004D2 RID: 1234
		public TitleMultiplayerServerEnabledStatus? Status;
	}
}
