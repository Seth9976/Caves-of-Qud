using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000C1 RID: 193
	[Serializable]
	public class ListCertificateSummariesResponse : PlayFabResultCommon
	{
		// Token: 0x040004F8 RID: 1272
		public List<CertificateSummary> CertificateSummaries;

		// Token: 0x040004F9 RID: 1273
		public int PageSize;

		// Token: 0x040004FA RID: 1274
		public string SkipToken;
	}
}
