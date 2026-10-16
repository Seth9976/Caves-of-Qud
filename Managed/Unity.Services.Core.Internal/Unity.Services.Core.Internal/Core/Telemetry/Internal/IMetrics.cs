using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000017 RID: 23
	public interface IMetrics
	{
		// Token: 0x0600002C RID: 44
		void SendGaugeMetric(string name, double value = 0.0, IDictionary<string, string> tags = null);

		// Token: 0x0600002D RID: 45
		void SendHistogramMetric(string name, double time, IDictionary<string, string> tags = null);

		// Token: 0x0600002E RID: 46
		void SendSumMetric(string name, double value = 1.0, IDictionary<string, string> tags = null);
	}
}
