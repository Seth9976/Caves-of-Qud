using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000251 RID: 593
	[Serializable]
	public class GetWindowsHelloChallengeRequest : PlayFabRequestCommon
	{
		// Token: 0x04000CA0 RID: 3232
		public string PublicKeyHint;

		// Token: 0x04000CA1 RID: 3233
		public string TitleId;
	}
}
