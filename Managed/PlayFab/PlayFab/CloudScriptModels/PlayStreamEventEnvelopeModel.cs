using System;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x02000190 RID: 400
	[Serializable]
	public class PlayStreamEventEnvelopeModel : PlayFabBaseModel
	{
		// Token: 0x040008CC RID: 2252
		public string EntityId;

		// Token: 0x040008CD RID: 2253
		public string EntityType;

		// Token: 0x040008CE RID: 2254
		public string EventData;

		// Token: 0x040008CF RID: 2255
		public string EventName;

		// Token: 0x040008D0 RID: 2256
		public string EventNamespace;

		// Token: 0x040008D1 RID: 2257
		public string EventSettings;
	}
}
