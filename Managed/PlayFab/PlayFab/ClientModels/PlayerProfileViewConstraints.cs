using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002A0 RID: 672
	[Serializable]
	public class PlayerProfileViewConstraints : PlayFabBaseModel
	{
		// Token: 0x04000E0E RID: 3598
		public bool ShowAvatarUrl;

		// Token: 0x04000E0F RID: 3599
		public bool ShowBannedUntil;

		// Token: 0x04000E10 RID: 3600
		public bool ShowCampaignAttributions;

		// Token: 0x04000E11 RID: 3601
		public bool ShowContactEmailAddresses;

		// Token: 0x04000E12 RID: 3602
		public bool ShowCreated;

		// Token: 0x04000E13 RID: 3603
		public bool ShowDisplayName;

		// Token: 0x04000E14 RID: 3604
		public bool ShowExperimentVariants;

		// Token: 0x04000E15 RID: 3605
		public bool ShowLastLogin;

		// Token: 0x04000E16 RID: 3606
		public bool ShowLinkedAccounts;

		// Token: 0x04000E17 RID: 3607
		public bool ShowLocations;

		// Token: 0x04000E18 RID: 3608
		public bool ShowMemberships;

		// Token: 0x04000E19 RID: 3609
		public bool ShowOrigination;

		// Token: 0x04000E1A RID: 3610
		public bool ShowPushNotificationRegistrations;

		// Token: 0x04000E1B RID: 3611
		public bool ShowStatistics;

		// Token: 0x04000E1C RID: 3612
		public bool ShowTags;

		// Token: 0x04000E1D RID: 3613
		public bool ShowTotalValueToDateInUsd;

		// Token: 0x04000E1E RID: 3614
		public bool ShowValuesToDate;
	}
}
