using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x0200008F RID: 143
	[Serializable]
	public class DynamicStandbySettings : PlayFabBaseModel
	{
		// Token: 0x04000460 RID: 1120
		public List<DynamicStandbyThreshold> DynamicFloorMultiplierThresholds;

		// Token: 0x04000461 RID: 1121
		public bool IsEnabled;

		// Token: 0x04000462 RID: 1122
		public int? RampDownSeconds;
	}
}
