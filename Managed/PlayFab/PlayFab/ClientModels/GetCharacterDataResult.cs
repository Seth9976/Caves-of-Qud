using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020001FA RID: 506
	[Serializable]
	public class GetCharacterDataResult : PlayFabResultCommon
	{
		// Token: 0x04000BD8 RID: 3032
		public string CharacterId;

		// Token: 0x04000BD9 RID: 3033
		public Dictionary<string, UserDataRecord> Data;

		// Token: 0x04000BDA RID: 3034
		public uint DataVersion;
	}
}
