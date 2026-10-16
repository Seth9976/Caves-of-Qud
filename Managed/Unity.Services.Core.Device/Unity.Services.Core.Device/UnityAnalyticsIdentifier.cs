using System;
using UnityEngine;

namespace Unity.Services.Core.Device
{
	// Token: 0x02000006 RID: 6
	internal class UnityAnalyticsIdentifier : IUserIdentifierProvider
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002221 File Offset: 0x00000421
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002230 File Offset: 0x00000430
		public string UserId
		{
			get
			{
				return PlayerPrefs.GetString("unity.cloud_userid");
			}
			set
			{
				try
				{
					PlayerPrefs.SetString("unity.cloud_userid", value);
					PlayerPrefs.Save();
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x04000006 RID: 6
		private const string k_PlayerUserIdKey = "unity.cloud_userid";
	}
}
