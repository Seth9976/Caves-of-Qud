using System;
using UnityEngine.Internal;

namespace UnityEngine.Analytics
{
	// Token: 0x0200000D RID: 13
	[ExcludeFromDocs]
	public interface UGSAnalyticsInternalTools
	{
		// Token: 0x06000015 RID: 21 RVA: 0x0000217D File Offset: 0x0000037D
		public static void SetPrivacyStatus(bool status)
		{
			AnalyticsCommon.ugsAnalyticsEnabled = status;
		}
	}
}
