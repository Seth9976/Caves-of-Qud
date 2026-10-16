using System;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	// Token: 0x02000162 RID: 354
	[Serializable]
	public class AbortFileUploadsResponse : PlayFabResultCommon
	{
		// Token: 0x04000716 RID: 1814
		public EntityKey Entity;

		// Token: 0x04000717 RID: 1815
		public int ProfileVersion;
	}
}
