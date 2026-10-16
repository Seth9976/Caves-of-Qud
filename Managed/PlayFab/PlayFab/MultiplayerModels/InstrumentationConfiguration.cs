using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000B3 RID: 179
	[Serializable]
	public class InstrumentationConfiguration : PlayFabBaseModel
	{
		// Token: 0x040004D5 RID: 1237
		public List<string> ProcessesToMonitor;
	}
}
