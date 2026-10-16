using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	// Token: 0x0200016B RID: 363
	[Serializable]
	public class GetObjectsRequest : PlayFabRequestCommon
	{
		// Token: 0x04000730 RID: 1840
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000731 RID: 1841
		public EntityKey Entity;

		// Token: 0x04000732 RID: 1842
		public bool? EscapeObject;
	}
}
