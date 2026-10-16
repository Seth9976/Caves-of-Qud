using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.EventsModels
{
	// Token: 0x0200015B RID: 347
	[Serializable]
	public class WriteEventsResponse : PlayFabResultCommon
	{
		// Token: 0x040006FA RID: 1786
		public List<string> AssignedEventIds;
	}
}
