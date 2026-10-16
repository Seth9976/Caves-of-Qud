using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	// Token: 0x02000053 RID: 83
	[Serializable]
	public class SetProfileLanguageRequest : PlayFabRequestCommon
	{
		// Token: 0x04000334 RID: 820
		public Dictionary<string, string> CustomTags;

		// Token: 0x04000335 RID: 821
		public EntityKey Entity;

		// Token: 0x04000336 RID: 822
		public int? ExpectedVersion;

		// Token: 0x04000337 RID: 823
		public string Language;
	}
}
