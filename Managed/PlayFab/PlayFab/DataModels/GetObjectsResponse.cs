using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	// Token: 0x0200016C RID: 364
	[Serializable]
	public class GetObjectsResponse : PlayFabResultCommon
	{
		// Token: 0x04000733 RID: 1843
		public EntityKey Entity;

		// Token: 0x04000734 RID: 1844
		public Dictionary<string, ObjectResult> Objects;

		// Token: 0x04000735 RID: 1845
		public int ProfileVersion;
	}
}
