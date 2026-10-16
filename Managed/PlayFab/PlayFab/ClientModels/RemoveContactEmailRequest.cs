using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002B2 RID: 690
	[Serializable]
	public class RemoveContactEmailRequest : PlayFabRequestCommon
	{
		// Token: 0x04000E66 RID: 3686
		public Dictionary<string, string> CustomTags;
	}
}
