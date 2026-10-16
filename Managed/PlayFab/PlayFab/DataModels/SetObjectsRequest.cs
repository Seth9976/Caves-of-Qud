using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	// Token: 0x02000174 RID: 372
	[Serializable]
	public class SetObjectsRequest : PlayFabRequestCommon
	{
		// Token: 0x0400074E RID: 1870
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400074F RID: 1871
		public EntityKey Entity;

		// Token: 0x04000750 RID: 1872
		public int? ExpectedProfileVersion;

		// Token: 0x04000751 RID: 1873
		public List<SetObject> Objects;
	}
}
