using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	// Token: 0x0200016F RID: 367
	[Serializable]
	public class InitiateFileUploadsResponse : PlayFabResultCommon
	{
		// Token: 0x0400073C RID: 1852
		public EntityKey Entity;

		// Token: 0x0400073D RID: 1853
		public int ProfileVersion;

		// Token: 0x0400073E RID: 1854
		public List<InitiateFileUploadMetadata> UploadDetails;
	}
}
