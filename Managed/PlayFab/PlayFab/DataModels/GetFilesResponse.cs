using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	// Token: 0x0200016A RID: 362
	[Serializable]
	public class GetFilesResponse : PlayFabResultCommon
	{
		// Token: 0x0400072D RID: 1837
		public EntityKey Entity;

		// Token: 0x0400072E RID: 1838
		public Dictionary<string, GetFileMetadata> Metadata;

		// Token: 0x0400072F RID: 1839
		public int ProfileVersion;
	}
}
