using System;
using System.Collections.Generic;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x02000007 RID: 7
	internal class DisabledMetrics : IMetrics
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002129 File Offset: 0x00000329
		void IMetrics.SendGaugeMetric(string name, double value, IDictionary<string, string> tags)
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000212B File Offset: 0x0000032B
		void IMetrics.SendHistogramMetric(string name, double time, IDictionary<string, string> tags)
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000212D File Offset: 0x0000032D
		void IMetrics.SendSumMetric(string name, double value, IDictionary<string, string> tags)
		{
		}
	}
}
