using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.EventsModels
{
	// Token: 0x02000159 RID: 345
	[Serializable]
	public class EventContents : PlayFabBaseModel
	{
		// Token: 0x040006F0 RID: 1776
		public Dictionary<string, string> CustomTags;

		// Token: 0x040006F1 RID: 1777
		public EntityKey Entity;

		// Token: 0x040006F2 RID: 1778
		public string EventNamespace;

		// Token: 0x040006F3 RID: 1779
		public string Name;

		// Token: 0x040006F4 RID: 1780
		public string OriginalId;

		// Token: 0x040006F5 RID: 1781
		public DateTime? OriginalTimestamp;

		// Token: 0x040006F6 RID: 1782
		public object Payload;

		// Token: 0x040006F7 RID: 1783
		public string PayloadJSON;
	}
}
