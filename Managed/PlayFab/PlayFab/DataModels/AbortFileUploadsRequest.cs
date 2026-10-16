using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	// Token: 0x02000161 RID: 353
	[Serializable]
	public class AbortFileUploadsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000712 RID: 1810
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000713 RID: 1811
		public EntityKey Entity;

		// Token: 0x04000714 RID: 1812
		public List<string> FileNames;

		// Token: 0x04000715 RID: 1813
		public int? ProfileVersion;
	}
}
