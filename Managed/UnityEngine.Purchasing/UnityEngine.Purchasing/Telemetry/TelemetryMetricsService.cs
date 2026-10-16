using System;

namespace UnityEngine.Purchasing.Telemetry
{
	// Token: 0x02000042 RID: 66
	internal class TelemetryMetricsService : ITelemetryMetricsService
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00003FC1 File Offset: 0x000021C1
		public TelemetryMetricsService(ITelemetryMetricsInstanceWrapper telemetryMetricsInstanceWrapper)
		{
			this.m_TelemetryMetricsInstanceWrapper = telemetryMetricsInstanceWrapper;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00003FD0 File Offset: 0x000021D0
		public void ExecuteTimedAction(Action timedAction, TelemetryMetricDefinition metricDefinition)
		{
			ITelemetryMetricEvent telemetryMetricEvent = this.CreateAndStartMetricEvent(metricDefinition);
			timedAction();
			try
			{
				telemetryMetricEvent.StopAndSendMetric();
			}
			catch (IapTelemetryException ex)
			{
				Debug.unityLogger.LogIAPError("An exception occured while sending a metric: " + ex.Message);
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004020 File Offset: 0x00002220
		public ITelemetryMetricEvent CreateAndStartMetricEvent(TelemetryMetricDefinition metricDefinition)
		{
			ITelemetryMetricEvent telemetryMetricEvent = new TelemetryMetricEvent(this.m_TelemetryMetricsInstanceWrapper, metricDefinition.MetricType, metricDefinition.MetricName);
			try
			{
				telemetryMetricEvent.StartMetric();
			}
			catch (IapTelemetryException ex)
			{
				Debug.unityLogger.LogIAPError("An exception occured while starting a metric: " + ex.Message);
			}
			return telemetryMetricEvent;
		}

		// Token: 0x04000099 RID: 153
		private readonly ITelemetryMetricsInstanceWrapper m_TelemetryMetricsInstanceWrapper;
	}
}
