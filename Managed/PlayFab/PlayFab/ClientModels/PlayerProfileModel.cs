using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200029F RID: 671
	[Serializable]
	public class PlayerProfileModel : PlayFabBaseModel
	{
		// Token: 0x04000DFA RID: 3578
		public List<AdCampaignAttributionModel> AdCampaignAttributions;

		// Token: 0x04000DFB RID: 3579
		public string AvatarUrl;

		// Token: 0x04000DFC RID: 3580
		public DateTime? BannedUntil;

		// Token: 0x04000DFD RID: 3581
		public List<ContactEmailInfoModel> ContactEmailAddresses;

		// Token: 0x04000DFE RID: 3582
		public DateTime? Created;

		// Token: 0x04000DFF RID: 3583
		public string DisplayName;

		// Token: 0x04000E00 RID: 3584
		public List<string> ExperimentVariants;

		// Token: 0x04000E01 RID: 3585
		public DateTime? LastLogin;

		// Token: 0x04000E02 RID: 3586
		public List<LinkedPlatformAccountModel> LinkedAccounts;

		// Token: 0x04000E03 RID: 3587
		public List<LocationModel> Locations;

		// Token: 0x04000E04 RID: 3588
		public List<MembershipModel> Memberships;

		// Token: 0x04000E05 RID: 3589
		public LoginIdentityProvider? Origination;

		// Token: 0x04000E06 RID: 3590
		public string PlayerId;

		// Token: 0x04000E07 RID: 3591
		public string PublisherId;

		// Token: 0x04000E08 RID: 3592
		public List<PushNotificationRegistrationModel> PushNotificationRegistrations;

		// Token: 0x04000E09 RID: 3593
		public List<StatisticModel> Statistics;

		// Token: 0x04000E0A RID: 3594
		public List<TagModel> Tags;

		// Token: 0x04000E0B RID: 3595
		public string TitleId;

		// Token: 0x04000E0C RID: 3596
		public uint? TotalValueToDateInUSD;

		// Token: 0x04000E0D RID: 3597
		public List<ValueToDateModel> ValuesToDate;
	}
}
