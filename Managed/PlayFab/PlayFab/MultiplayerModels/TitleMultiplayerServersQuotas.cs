using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000FB RID: 251
	[Serializable]
	public class TitleMultiplayerServersQuotas : PlayFabBaseModel
	{
		// Token: 0x040005BA RID: 1466
		public List<CoreCapacity> CoreCapacities;
	}
}
