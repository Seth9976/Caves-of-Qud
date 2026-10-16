using System;
using Unity.Services.Core.Internal;
using UnityEngine.Scripting;

namespace Unity.Services.Core.Analytics.Internal
{
	// Token: 0x02000027 RID: 39
	[RequireImplementors]
	public interface IAnalyticsUserId : IServiceComponent
	{
		// Token: 0x0600006C RID: 108
		string GetAnalyticsUserId();
	}
}
