using System;
using UnityEngine.Internal;

namespace UnityEngine.Analytics
{
	// Token: 0x02000012 RID: 18
	[ExcludeFromDocs]
	public class Analytic : AnalyticsEventBase
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000021F5 File Offset: 0x000003F5
		public Analytic(IAnalytic instance, AnalyticInfoAttribute info)
			: base(info.eventName, info.version, SendEventOptions.kAppendNone, "")
		{
			this.instance = instance;
			this.info = info;
		}

		// Token: 0x04000019 RID: 25
		public readonly IAnalytic instance;

		// Token: 0x0400001A RID: 26
		public readonly AnalyticInfoAttribute info;
	}
}
