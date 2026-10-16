using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x02000193 RID: 403
	[Serializable]
	public class PostFunctionResultForPlayerTriggeredActionRequest : PlayFabRequestCommon
	{
		// Token: 0x040008D8 RID: 2264
		public Dictionary<string, string> CustomTags;

		// Token: 0x040008D9 RID: 2265
		public EntityKey Entity;

		// Token: 0x040008DA RID: 2266
		public ExecuteFunctionResult FunctionResult;

		// Token: 0x040008DB RID: 2267
		public PlayerProfileModel PlayerProfile;

		// Token: 0x040008DC RID: 2268
		public PlayStreamEventEnvelopeModel PlayStreamEventEnvelope;
	}
}
