using System;

namespace UnityEngine.Purchasing.Telemetry
{
	// Token: 0x02000035 RID: 53
	internal struct TelemetryDiagnosticParams
	{
		// Token: 0x06000109 RID: 265 RVA: 0x00003ABF File Offset: 0x00001CBF
		internal TelemetryDiagnosticParams(string diagnosticName, string diagnosticException)
		{
			this.name = diagnosticName;
			this.exception = diagnosticException;
		}

		// Token: 0x04000072 RID: 114
		internal string name;

		// Token: 0x04000073 RID: 115
		internal string exception;
	}
}
