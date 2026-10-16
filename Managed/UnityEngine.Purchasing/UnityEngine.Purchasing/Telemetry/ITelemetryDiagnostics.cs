using System;

namespace UnityEngine.Purchasing.Telemetry
{
	// Token: 0x02000032 RID: 50
	internal interface ITelemetryDiagnostics
	{
		// Token: 0x06000106 RID: 262
		void SendDiagnostic(string diagnosticName, Exception e);
	}
}
