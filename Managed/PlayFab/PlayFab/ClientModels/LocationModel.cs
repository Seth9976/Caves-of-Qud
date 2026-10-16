using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200027B RID: 635
	[Serializable]
	public class LocationModel : PlayFabBaseModel
	{
		// Token: 0x04000D08 RID: 3336
		public string City;

		// Token: 0x04000D09 RID: 3337
		public ContinentCode? ContinentCode;

		// Token: 0x04000D0A RID: 3338
		public CountryCode? CountryCode;

		// Token: 0x04000D0B RID: 3339
		public double? Latitude;

		// Token: 0x04000D0C RID: 3340
		public double? Longitude;
	}
}
