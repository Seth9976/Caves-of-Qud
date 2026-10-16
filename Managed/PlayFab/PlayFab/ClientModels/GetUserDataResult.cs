using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200024E RID: 590
	[Serializable]
	public class GetUserDataResult : PlayFabResultCommon
	{
		// Token: 0x04000C9A RID: 3226
		public Dictionary<string, UserDataRecord> Data;

		// Token: 0x04000C9B RID: 3227
		public uint DataVersion;
	}
}
