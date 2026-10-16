using System;

namespace UnityEngine.Purchasing.Telemetry
{
	// Token: 0x0200003C RID: 60
	internal struct TelemetryMetricDefinition
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00003C5B File Offset: 0x00001E5B
		public readonly TelemetryMetricTypes MetricType { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00003C63 File Offset: 0x00001E63
		public readonly string MetricName { get; }

		// Token: 0x0600011C RID: 284 RVA: 0x00003C6B File Offset: 0x00001E6B
		public TelemetryMetricDefinition(string metricName, TelemetryMetricTypes metricType = TelemetryMetricTypes.Histogram)
		{
			this.MetricName = metricName;
			this.MetricType = metricType;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00003C7B File Offset: 0x00001E7B
		public static implicit operator TelemetryMetricDefinition(string name)
		{
			return new TelemetryMetricDefinition(name, TelemetryMetricTypes.Histogram);
		}
	}
}
