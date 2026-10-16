using System;
using System.Diagnostics;

namespace UnityEngine.Purchasing.Telemetry
{
	// Token: 0x0200003E RID: 62
	internal class TelemetryMetricEvent : ITelemetryMetricEvent
	{
		// Token: 0x0600011F RID: 287 RVA: 0x00003D72 File Offset: 0x00001F72
		internal TelemetryMetricEvent(ITelemetryMetricsInstanceWrapper telemetryMetricsInstanceWrapper, TelemetryMetricTypes metricType, string metricName)
		{
			this.m_TelemetryMetricsInstanceWrapper = telemetryMetricsInstanceWrapper;
			this.m_MetricType = metricType;
			this.m_MetricName = metricName;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00003D9A File Offset: 0x00001F9A
		public void StartMetric()
		{
			if (this.m_Stopwatch == null)
			{
				throw new IapTelemetryException("Metric was already sent.");
			}
			if (!this.m_Stopwatch.IsRunning)
			{
				this.m_Stopwatch.Start();
				return;
			}
			throw new IapTelemetryException("Metric was already started.");
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00003DD4 File Offset: 0x00001FD4
		public void StopAndSendMetric()
		{
			if (this.m_Stopwatch != null)
			{
				ITelemetryMetricsInstanceWrapper telemetryMetricsInstanceWrapper = this.m_TelemetryMetricsInstanceWrapper;
				if (telemetryMetricsInstanceWrapper != null)
				{
					telemetryMetricsInstanceWrapper.SendMetric(this.m_MetricType, this.m_MetricName, (double)this.m_Stopwatch.Elapsed.Seconds);
				}
				this.m_Stopwatch = null;
				return;
			}
			throw new IapTelemetryException("Metric was already sent.");
		}

		// Token: 0x0400008A RID: 138
		private readonly ITelemetryMetricsInstanceWrapper m_TelemetryMetricsInstanceWrapper;

		// Token: 0x0400008B RID: 139
		private readonly TelemetryMetricTypes m_MetricType;

		// Token: 0x0400008C RID: 140
		private readonly string m_MetricName;

		// Token: 0x0400008D RID: 141
		private Stopwatch m_Stopwatch = new Stopwatch();
	}
}
