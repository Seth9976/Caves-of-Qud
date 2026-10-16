using System;
using System.Collections.Generic;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x0200000A RID: 10
	internal class MetricsFactory : IMetricsFactory, IServiceComponent
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000217A File Offset: 0x0000037A
		public IReadOnlyDictionary<string, string> CommonTags { get; } = new Dictionary<string, string>();

		// Token: 0x0600001B RID: 27 RVA: 0x00002182 File Offset: 0x00000382
		public IMetrics Create(string packageName)
		{
			return new Metrics();
		}
	}
}
