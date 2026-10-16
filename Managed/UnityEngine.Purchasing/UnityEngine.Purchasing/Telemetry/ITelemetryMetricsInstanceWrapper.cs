using System;
using Unity.Services.Core.Telemetry.Internal;

namespace UnityEngine.Purchasing.Telemetry
{
	// Token: 0x0200003A RID: 58
	internal interface ITelemetryMetricsInstanceWrapper
	{
		// Token: 0x06000116 RID: 278
		void SetMetricsInstance(IMetrics metricsInstance);

		// Token: 0x06000117 RID: 279
		void SendMetric(TelemetryMetricTypes telemetryMetricTypes, string metricName, double metricTimeSeconds);
	}
}
