using System;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	// Token: 0x0200018A RID: 394
	[Serializable]
	public class LocationModel : PlayFabBaseModel
	{
		// Token: 0x04000893 RID: 2195
		public string City;

		// Token: 0x04000894 RID: 2196
		public ContinentCode? ContinentCode;

		// Token: 0x04000895 RID: 2197
		public CountryCode? CountryCode;

		// Token: 0x04000896 RID: 2198
		public double? Latitude;

		// Token: 0x04000897 RID: 2199
		public double? Longitude;
	}
}
