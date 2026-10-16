using System;

namespace UnityEngine.Purchasing.Telemetry
{
	// Token: 0x0200003F RID: 63
	internal struct TelemetryMetricParams
	{
		// Token: 0x06000122 RID: 290 RVA: 0x00003E2C File Offset: 0x0000202C
		internal TelemetryMetricParams(TelemetryMetricTypes metricType, string metricName, double metricTimeSeconds)
		{
			this.type = metricType;
			this.name = metricName;
			this.timeSeconds = metricTimeSeconds;
		}

		// Token: 0x0400008E RID: 142
		internal TelemetryMetricTypes type;

		// Token: 0x0400008F RID: 143
		internal string name;

		// Token: 0x04000090 RID: 144
		internal double timeSeconds;
	}
}
