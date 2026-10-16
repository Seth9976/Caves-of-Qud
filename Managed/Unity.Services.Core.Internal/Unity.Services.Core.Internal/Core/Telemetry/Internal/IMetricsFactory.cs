using System;
using System.Collections.Generic;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000018 RID: 24
	public interface IMetricsFactory : IServiceComponent
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002F RID: 47
		IReadOnlyDictionary<string, string> CommonTags { get; }

		// Token: 0x06000030 RID: 48
		IMetrics Create(string packageName);
	}
}
