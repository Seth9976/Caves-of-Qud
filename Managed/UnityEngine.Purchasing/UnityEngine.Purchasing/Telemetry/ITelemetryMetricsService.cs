using System;

namespace UnityEngine.Purchasing.Telemetry
{
	// Token: 0x0200003B RID: 59
	internal interface ITelemetryMetricsService
	{
		// Token: 0x06000118 RID: 280
		void ExecuteTimedAction(Action timedAction, TelemetryMetricDefinition metricDefinition);

		// Token: 0x06000119 RID: 281
		ITelemetryMetricEvent CreateAndStartMetricEvent(TelemetryMetricDefinition metricDefinition);
	}
}
