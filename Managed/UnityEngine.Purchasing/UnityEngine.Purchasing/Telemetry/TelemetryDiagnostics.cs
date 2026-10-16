using System;

namespace UnityEngine.Purchasing.Telemetry
{
	// Token: 0x02000036 RID: 54
	internal class TelemetryDiagnostics : ITelemetryDiagnostics
	{
		// Token: 0x0600010A RID: 266 RVA: 0x00003ACF File Offset: 0x00001CCF
		public TelemetryDiagnostics(ITelemetryDiagnosticsInstanceWrapper telemetryDiagnosticsInstanceWrapper)
		{
			this.m_TelemetryDiagnosticsInstanceWrapper = telemetryDiagnosticsInstanceWrapper;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00003AE0 File Offset: 0x00001CE0
		public void SendDiagnostic(string diagnosticName, Exception e)
		{
			try
			{
				this.m_TelemetryDiagnosticsInstanceWrapper.SendDiagnostic(diagnosticName, e.ToString());
			}
			catch (IapTelemetryException ex)
			{
				Debug.unityLogger.LogIAPError("An exception occured while sending a diagnostic: " + ex.Message);
			}
		}

		// Token: 0x04000074 RID: 116
		private readonly ITelemetryDiagnosticsInstanceWrapper m_TelemetryDiagnosticsInstanceWrapper;
	}
}
