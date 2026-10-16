using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000009 RID: 9
	internal class Metrics : IMetrics
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002159 File Offset: 0x00000359
		internal IDictionary<string, string> PackageTags { get; } = new Dictionary<string, string>();

		// Token: 0x06000016 RID: 22 RVA: 0x00002161 File Offset: 0x00000361
		void IMetrics.SendGaugeMetric(string name, double value, IDictionary<string, string> tags)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002163 File Offset: 0x00000363
		void IMetrics.SendHistogramMetric(string name, double time, IDictionary<string, string> tags)
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002165 File Offset: 0x00000365
		void IMetrics.SendSumMetric(string name, double value, IDictionary<string, string> tags)
		{
		}
	}
}
