using System;
using Uniject;
using Unity.Services.Core.Telemetry.Internal;

namespace UnityEngine.Purchasing.Telemetry
{
	// Token: 0x02000041 RID: 65
	internal class TelemetryMetricsInstanceWrapper : ITelemetryMetricsInstanceWrapper
	{
		// Token: 0x06000123 RID: 291 RVA: 0x00003E43 File Offset: 0x00002043
		public TelemetryMetricsInstanceWrapper(ILogger logger, IUtil util)
		{
			this.m_Logger = logger;
			this.m_Util = util;
			this.m_Queue = new TelemetryQueue<TelemetryMetricParams>(new Action<TelemetryMetricParams>(this.SendMetricOnMainThread));
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00003E70 File Offset: 0x00002070
		public void SetMetricsInstance(IMetrics metricsInstance)
		{
			this.m_Instance = metricsInstance;
			if (this.m_Instance != null)
			{
				this.m_Queue.SendQueuedEvents();
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00003E8C File Offset: 0x0000208C
		public void SendMetric(TelemetryMetricTypes metricType, string metricName, double metricTimeSeconds)
		{
			TelemetryMetricParams telemetryMetricParams = new TelemetryMetricParams(metricType, metricName, metricTimeSeconds);
			if (this.m_Instance != null)
			{
				this.SendMetricOnMainThread(telemetryMetricParams);
				return;
			}
			this.m_Queue.QueueEvent(telemetryMetricParams);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00003EC0 File Offset: 0x000020C0
		private void SendMetricOnMainThread(TelemetryMetricParams metricParams)
		{
			this.m_Util.RunOnMainThread(delegate
			{
				this.SendMetricByTypeAndCatchExceptions(metricParams);
			});
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00003EF8 File Offset: 0x000020F8
		private void SendMetricByTypeAndCatchExceptions(TelemetryMetricParams metricParams)
		{
			try
			{
				this.SendMetricByType(metricParams);
			}
			catch (Exception ex)
			{
				this.m_Logger.LogIAPError(string.Format("An exception occurred when sending metric {0} of type {1}. Message: {2}", metricParams.name, metricParams.type, ex.Message));
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00003F50 File Offset: 0x00002150
		private void SendMetricByType(TelemetryMetricParams metricParams)
		{
			switch (metricParams.type)
			{
			case TelemetryMetricTypes.Gauge:
				this.m_Instance.SendGaugeMetric(metricParams.name, metricParams.timeSeconds, null);
				return;
			case TelemetryMetricTypes.Sum:
				this.m_Instance.SendSumMetric(metricParams.name, metricParams.timeSeconds, null);
				return;
			case TelemetryMetricTypes.Histogram:
				this.m_Instance.SendHistogramMetric(metricParams.name, metricParams.timeSeconds, null);
				return;
			default:
				return;
			}
		}

		// Token: 0x04000095 RID: 149
		private IMetrics m_Instance;

		// Token: 0x04000096 RID: 150
		private ILogger m_Logger;

		// Token: 0x04000097 RID: 151
		private IUtil m_Util;

		// Token: 0x04000098 RID: 152
		private readonly TelemetryQueue<TelemetryMetricParams> m_Queue;
	}
}
