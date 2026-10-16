using System;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	// Token: 0x02000164 RID: 356
	[Serializable]
	public class DeleteFilesResponse : PlayFabResultCommon
	{
		// Token: 0x0400071C RID: 1820
		public EntityKey Entity;

		// Token: 0x0400071D RID: 1821
		public int ProfileVersion;
	}
}
