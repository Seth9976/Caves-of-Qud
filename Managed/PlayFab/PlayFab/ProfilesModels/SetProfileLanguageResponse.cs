using System;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	// Token: 0x02000054 RID: 84
	[Serializable]
	public class SetProfileLanguageResponse : PlayFabResultCommon
	{
		// Token: 0x04000338 RID: 824
		public OperationTypes? OperationResult;

		// Token: 0x04000339 RID: 825
		public int? VersionNumber;
	}
}
