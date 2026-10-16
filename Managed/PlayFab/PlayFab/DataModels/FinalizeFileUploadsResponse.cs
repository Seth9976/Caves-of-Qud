using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	// Token: 0x02000167 RID: 359
	[Serializable]
	public class FinalizeFileUploadsResponse : PlayFabResultCommon
	{
		// Token: 0x04000723 RID: 1827
		public EntityKey Entity;

		// Token: 0x04000724 RID: 1828
		public Dictionary<string, GetFileMetadata> Metadata;

		// Token: 0x04000725 RID: 1829
		public int ProfileVersion;
	}
}
