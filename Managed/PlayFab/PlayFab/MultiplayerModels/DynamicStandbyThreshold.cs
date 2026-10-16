using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000090 RID: 144
	[Serializable]
	public class DynamicStandbyThreshold : PlayFabBaseModel
	{
		// Token: 0x04000463 RID: 1123
		public double Multiplier;

		// Token: 0x04000464 RID: 1124
		public double TriggerThresholdPercentage;
	}
}
