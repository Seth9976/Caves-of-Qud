using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000100 RID: 256
	[Serializable]
	public class UploadCertificateRequest : PlayFabRequestCommon
	{
		// Token: 0x040005C8 RID: 1480
		public Dictionary<string, string> CustomTags;

		// Token: 0x040005C9 RID: 1481
		public Certificate GameCertificate;
	}
}
