using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002DC RID: 732
	[Serializable]
	public class TradeInfo : PlayFabBaseModel
	{
		// Token: 0x04000EE2 RID: 3810
		public List<string> AcceptedInventoryInstanceIds;

		// Token: 0x04000EE3 RID: 3811
		public string AcceptedPlayerId;

		// Token: 0x04000EE4 RID: 3812
		public List<string> AllowedPlayerIds;

		// Token: 0x04000EE5 RID: 3813
		public DateTime? CancelledAt;

		// Token: 0x04000EE6 RID: 3814
		public DateTime? FilledAt;

		// Token: 0x04000EE7 RID: 3815
		public DateTime? InvalidatedAt;

		// Token: 0x04000EE8 RID: 3816
		public List<string> OfferedCatalogItemIds;

		// Token: 0x04000EE9 RID: 3817
		public List<string> OfferedInventoryInstanceIds;

		// Token: 0x04000EEA RID: 3818
		public string OfferingPlayerId;

		// Token: 0x04000EEB RID: 3819
		public DateTime? OpenedAt;

		// Token: 0x04000EEC RID: 3820
		public List<string> RequestedCatalogItemIds;

		// Token: 0x04000EED RID: 3821
		public TradeStatus? Status;

		// Token: 0x04000EEE RID: 3822
		public string TradeId;
	}
}
