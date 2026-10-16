using System;
using Unity.Services.Core.Telemetry.Internal;

namespace UnityEngine.Purchasing.Telemetry
{
	// Token: 0x02000033 RID: 51
	internal interface ITelemetryDiagnosticsInstanceWrapper
	{
		// Token: 0x06000107 RID: 263
		void SetDiagnosticsInstance(IDiagnostics diagnosticsInstance);

		// Token: 0x06000108 RID: 264
		void SendDiagnostic(string diagnosticName, string diagnosticException);
	}
}
