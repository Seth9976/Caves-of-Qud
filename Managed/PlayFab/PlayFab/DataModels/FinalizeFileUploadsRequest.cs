using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	// Token: 0x02000166 RID: 358
	[Serializable]
	public class FinalizeFileUploadsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000720 RID: 1824
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000721 RID: 1825
		public EntityKey Entity;

		// Token: 0x04000722 RID: 1826
		public List<string> FileNames;
	}
}
