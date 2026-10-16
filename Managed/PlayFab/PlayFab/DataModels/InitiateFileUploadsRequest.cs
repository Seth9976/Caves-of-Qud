using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	// Token: 0x0200016E RID: 366
	[Serializable]
	public class InitiateFileUploadsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000738 RID: 1848
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000739 RID: 1849
		public EntityKey Entity;

		// Token: 0x0400073A RID: 1850
		public List<string> FileNames;

		// Token: 0x0400073B RID: 1851
		public int? ProfileVersion;
	}
}
