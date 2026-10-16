using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	// Token: 0x02000163 RID: 355
	[Serializable]
	public class DeleteFilesRequest : PlayFabRequestCommon
	{
		// Token: 0x04000718 RID: 1816
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000719 RID: 1817
		public EntityKey Entity;

		// Token: 0x0400071A RID: 1818
		public List<string> FileNames;

		// Token: 0x0400071B RID: 1819
		public int? ProfileVersion;
	}
}
