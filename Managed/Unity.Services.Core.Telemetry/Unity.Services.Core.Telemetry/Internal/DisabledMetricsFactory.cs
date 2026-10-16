using System;
using System.Collections.Generic;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000008 RID: 8
	internal class DisabledMetricsFactory : IMetricsFactory, IServiceComponent
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002137 File Offset: 0x00000337
		IReadOnlyDictionary<string, string> IMetricsFactory.CommonTags { get; } = new Dictionary<string, string>();

		// Token: 0x06000013 RID: 19 RVA: 0x0000213F File Offset: 0x0000033F
		IMetrics IMetricsFactory.Create(string packageName)
		{
			return new DisabledMetrics();
		}
	}
}
