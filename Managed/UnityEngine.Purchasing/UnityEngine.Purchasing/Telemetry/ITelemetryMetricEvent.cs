using System;

namespace UnityEngine.Purchasing.Telemetry
{
	// Token: 0x02000039 RID: 57
	internal interface ITelemetryMetricEvent
	{
		// Token: 0x06000114 RID: 276
		void StartMetric();

		// Token: 0x06000115 RID: 277
		void StopAndSendMetric();
	}
}
