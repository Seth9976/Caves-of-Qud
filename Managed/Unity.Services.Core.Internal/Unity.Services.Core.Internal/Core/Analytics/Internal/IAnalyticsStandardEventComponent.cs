using System;
using System.Collections.Generic;
using Unity.Services.Core.Internal;
using UnityEngine.Scripting;

namespace Unity.Services.Core.Analytics.Internal
{
	// Token: 0x02000026 RID: 38
	[RequireImplementors]
	public interface IAnalyticsStandardEventComponent : IServiceComponent
	{
		// Token: 0x0600006B RID: 107
		void Record(string eventName, IDictionary<string, object> eventParameters, int eventVersion, string packageName);
	}
}
