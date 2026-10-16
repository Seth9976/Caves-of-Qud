using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x0200008B RID: 139
	[Serializable]
	public class DeleteCertificateRequest : PlayFabRequestCommon
	{
		// Token: 0x0400044D RID: 1101
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400044E RID: 1102
		public string Name;
	}
}
