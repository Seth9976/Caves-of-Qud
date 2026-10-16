using System;

namespace Unity.Services.Core.Analytics
{
	// Token: 0x02000003 RID: 3
	public static class AnalyticsOptionsExtensions
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020B8 File Offset: 0x000002B8
		[Obsolete("SetAnalyticsUserId is deprecated. Please use UnityServices.ExternalUserId instead.", false)]
		public static InitializationOptions SetAnalyticsUserId(this InitializationOptions self, string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentException("Analytics user id cannot be null or empty.", "id");
			}
			return self.SetOption("com.unity.services.core.analytics-user-id", id);
		}

		// Token: 0x04000001 RID: 1
		internal const string AnalyticsUserIdKey = "com.unity.services.core.analytics-user-id";
	}
}
