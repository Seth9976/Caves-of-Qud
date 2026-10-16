using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x0200018F RID: 399
	[Serializable]
	public class PlayerProfileModel : PlayFabBaseModel
	{
		// Token: 0x040008B8 RID: 2232
		public List<AdCampaignAttributionModel> AdCampaignAttributions;

		// Token: 0x040008B9 RID: 2233
		public string AvatarUrl;

		// Token: 0x040008BA RID: 2234
		public DateTime? BannedUntil;

		// Token: 0x040008BB RID: 2235
		public List<ContactEmailInfoModel> ContactEmailAddresses;

		// Token: 0x040008BC RID: 2236
		public DateTime? Created;

		// Token: 0x040008BD RID: 2237
		public string DisplayName;

		// Token: 0x040008BE RID: 2238
		public List<string> ExperimentVariants;

		// Token: 0x040008BF RID: 2239
		public DateTime? LastLogin;

		// Token: 0x040008C0 RID: 2240
		public List<LinkedPlatformAccountModel> LinkedAccounts;

		// Token: 0x040008C1 RID: 2241
		public List<LocationModel> Locations;

		// Token: 0x040008C2 RID: 2242
		public List<MembershipModel> Memberships;

		// Token: 0x040008C3 RID: 2243
		public LoginIdentityProvider? Origination;

		// Token: 0x040008C4 RID: 2244
		public string PlayerId;

		// Token: 0x040008C5 RID: 2245
		public string PublisherId;

		// Token: 0x040008C6 RID: 2246
		public List<PushNotificationRegistrationModel> PushNotificationRegistrations;

		// Token: 0x040008C7 RID: 2247
		public List<StatisticModel> Statistics;

		// Token: 0x040008C8 RID: 2248
		public List<TagModel> Tags;

		// Token: 0x040008C9 RID: 2249
		public string TitleId;

		// Token: 0x040008CA RID: 2250
		public uint? TotalValueToDateInUSD;

		// Token: 0x040008CB RID: 2251
		public List<ValueToDateModel> ValuesToDate;
	}
}
