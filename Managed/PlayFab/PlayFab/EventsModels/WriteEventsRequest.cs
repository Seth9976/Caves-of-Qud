using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.EventsModels
{
	// Token: 0x0200015A RID: 346
	[Serializable]
	public class WriteEventsRequest : PlayFabRequestCommon
	{
		// Token: 0x040006F8 RID: 1784
		public Dictionary<string, string> CustomTags;

		// Token: 0x040006F9 RID: 1785
		public List<EventContents> Events;
	}
}
